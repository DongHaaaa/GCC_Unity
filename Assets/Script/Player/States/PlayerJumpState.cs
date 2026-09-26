public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("OnGround", false);
        player.Animator.SetBool("IsJumping", true);

        player.Movement.Jump();
    }

    public override void Update()
    {
        base.Update();

        float moveX = player.MoveInput.x;

        player.Movement.Flip(moveX);
        player.Animator.SetFloat("yVelocity", player.VerticalVelocity);

        if (player.DashPressed && player.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
            return;
        }

        if (player.VerticalVelocity < 0f)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.Move(player.MoveInput.x);
    }
}
