using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class AimStateManager : MonoBehaviour
{

    public AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();


    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSensitivity = 1f;

    float xAxis, yAxis;
    private PlayerInput playerInput;
    private InputAction lookAction;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;

    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    private void Start()
    {
        vCam = GetComponentInChildren<CinemachineCamera>();
        hipFov = vCam.Lens.FieldOfView;
        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }

    private void Update()
    {
        xAxis += Input.GetAxis("Mouse X") * mouseSensitivity;
        yAxis -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -45f, 45f);
        currentState.UpdateState(this);

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
        
    }

    private void LateUpdate()
    {
        if (camFollowPos != null)
        {
            // Apply rotations
            camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
        }
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);

    }
}
