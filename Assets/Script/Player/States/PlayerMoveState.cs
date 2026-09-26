using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("IsRunning", true);
    }

    public override void Update()
    {
        base.Update();

        float moveX = player.MoveInput.x;

        player.Movement.Flip(moveX);

        if (Mathf.Abs(moveX) < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.Move(player.MoveInput.x);
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool("IsRunning", false);
    }
}
