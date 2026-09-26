using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;

    [SerializeField] private Animator anim;

    private Rigidbody2D rb;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerMovement Movement { get; private set; }
    public Animator Animator => anim;
    public Vector2 MoveInput => MoveAction.ReadValue<Vector2>();

    void Awake()
    {
        MoveAction = InputSystem.actions.FindAction("MoveAction");
        JumpAction = InputSystem.actions.FindAction("JumpAction");
        AttackAction = InputSystem.actions.FindAction("AttackAction");

        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement = GetComponent<PlayerMovement>();

        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.Update();

        if (JumpAction.WasPressedThisFrame())
        {
            Debug.Log("Jump Pressed");
        }

        if (JumpAction.IsPressed())
        {
            Debug.Log("Jump Hold");
        }

        if (JumpAction.WasReleasedThisFrame())
        {
            Debug.Log("Jump Release");
        }

        if (AttackAction.IsPressed())
        {
            anim.SetBool("IsAttacking", true);
            Debug.Log("Attack Pressed");
        }

        float Vy = rb.linearVelocityY;

        if (Vy < 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("OnGround", false);
            anim.SetFloat("yVelocity", Vy);
        }
        else if (Vy > 0)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("OnGround", false);
            anim.SetFloat("yVelocity", Vy);
        }
        else
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("OnGround", true);
        }
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void OnAttackEnd()
    {
        anim.SetBool("IsAttacking", false);
    }
}
