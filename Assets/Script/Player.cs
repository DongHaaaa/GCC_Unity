using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;
    InputAction DashAction;

    [SerializeField] private Animator anim;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    [Header("Dash Settings")]
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;

    private Rigidbody2D rb;
    private float nextDashTime;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerHurtState HurtState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    public PlayerMovement Movement { get; private set; }
    public Animator Animator => anim;
    public Vector2 MoveInput => MoveAction.ReadValue<Vector2>();
    public bool JumpPressed => JumpAction.WasPressedThisFrame();
    public bool AttackPressed => AttackAction.WasPressedThisFrame();
    public bool DashPressed => DashAction.WasPressedThisFrame();
    public float VerticalVelocity => rb.linearVelocityY;
    public int CurrentHealth { get; private set; }
    public float DashDuration => dashDuration;
    public bool CanDash => Time.time >= nextDashTime;

    void Awake()
    {
        MoveAction = InputSystem.actions.FindAction("MoveAction");
        JumpAction = InputSystem.actions.FindAction("JumpAction");
        AttackAction = InputSystem.actions.FindAction("AttackAction");
        DashAction = InputSystem.actions.FindAction("DashAction");

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
        AttackState = new PlayerAttackState(this, StateMachine);
        DashState = new PlayerDashState(this, StateMachine);
        HurtState = new PlayerHurtState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);

        CurrentHealth = maxHealth;
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

    public void StartDashCooldown()
    {
        nextDashTime = Time.time + dashCooldown;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        if (StateMachine.CurrentState == DeadState ||
            StateMachine.CurrentState == HurtState)
            return;

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        if (CurrentHealth == 0)
        {
            StateMachine.ChangeState(DeadState);
            return;
        }

        StateMachine.ChangeState(HurtState);
    }

    public void OnAttackEnd()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            AttackState.FinishAttack();
        }
    }

    public void OnHurtEnd()
    {
        if (StateMachine.CurrentState == HurtState)
        {
            HurtState.FinishHurt();
        }
    }
}
