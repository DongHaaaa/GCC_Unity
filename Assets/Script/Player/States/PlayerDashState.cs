using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float timer;
    private float direction;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timer = player.DashDuration;

        float moveX = player.MoveInput.x;
        direction = Mathf.Abs(moveX) >= 0.01f
            ? Mathf.Sign(moveX)
            : player.Movement.FacingDirection;

        player.Movement.Flip(direction);
        player.Movement.BeginDash(direction);
        player.StartDashCooldown();
    }

    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;

        if (timer > 0f)
            return;

        if (!player.Movement.IsGrounded)
        {
            stateMachine.ChangeState(player.FallState);
            return;
        }

        if (Mathf.Abs(player.MoveInput.x) >= 0.01f)
            stateMachine.ChangeState(player.MoveState);
        else
            stateMachine.ChangeState(player.IdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.ContinueDash(direction);
    }

    public override void Exit()
    {
        base.Exit();
        player.Movement.EndDash();
    }
}
