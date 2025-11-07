using Unity.Collections;
using UnityEngine;

public class MovementState : MonoBehaviour
{

    public MovementBaseState currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();
    public CoverState Cover = new CoverState();

    [HideInInspector] public Animator anim;

    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float hzInput, vtInput;
    //[HideInInspector] public CharacterController controller;
    public CharacterController controller; //sonradan eklendi olmazsa silicennek
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundLayer;
    Vector3 spherePos;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vtInput", vtInput);
        // Eğer "cover" tuşuna basılmışsa ve yakınında siper varsa
    if (Input.GetKeyDown(KeyCode.Q))
    {
        if (Physics.Raycast(transform.position + Vector3.up * 1f, transform.forward, 1f, LayerMask.GetMask("Cover")))
        {
            SwitchState(Cover);
            return;
        }
    }
        currentState.UpdateState(this);
        
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vtInput = Input.GetAxis("Vertical");

        moveDirection = transform.forward * vtInput + transform.right * hzInput;
        controller.Move(currentMoveSpeed * Time.deltaTime * moveDirection.normalized);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
