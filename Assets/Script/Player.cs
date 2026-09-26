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
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public PlayerMovement Movement { get; private set; }
    public Animator Animator => anim;
    public Vector2 MoveInput => MoveAction.ReadValue<Vector2>();
    public bool JumpPressed => JumpAction.WasPressedThisFrame();
    public bool AttackPressed => AttackAction.WasPressedThisFrame();
    public float VerticalVelocity => rb.linearVelocityY;

    void Awake()
    {
        MoveAction = InputSystem.actions.FindAction("MoveAction");
        JumpAction = InputSystem.actions.FindAction("JumpAction");
        AttackAction = InputSystem.actions.FindAction("AttackAction");

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
        AttackState = new PlayerAttackState(this, StateMachine);
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
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    public void OnAttackEnd()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            AttackState.FinishAttack();
        }
    }
}
