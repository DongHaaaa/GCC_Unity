using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsAttacking", true);
    }

    public override void Update()
    {
        base.Update();

        player.Movement.Flip(player.MoveInput.x);
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool("IsAttacking", false);
    }

    public void FinishAttack()
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
