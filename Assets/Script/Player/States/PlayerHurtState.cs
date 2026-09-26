using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Movement.StopHorizontal();
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsAttacking", false);
        player.Animator.SetBool("IsHurt", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool("IsHurt", false);
    }

    public void FinishHurt()
    {
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
}
