using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    [HideInInspector] public Vector3 dir;
    private float hzInput, vInput;
    private CharacterController controller;

    [Header("Ground Check")]
    [SerializeField] private float groundYOffset = 0.5f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePos;

    [Header("Gravity Settings")]
    [SerializeField] private float gravity = -20f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirection();
        ApplyGravity();
        MovePlayer();
    }

    void GetDirection()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        return Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask);
    }

    void ApplyGravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2f;
    }

    void MovePlayer()
    {
        controller.Move((dir * moveSpeed + velocity) * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (controller == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
