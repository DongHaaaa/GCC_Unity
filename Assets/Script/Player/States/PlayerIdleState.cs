using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsJumping", false);
        player.Animator.SetBool("OnGround", true);
    }

    public override void Update()
    {
        base.Update();

        if (player.AttackPressed)
        {
            stateMachine.ChangeState(player.AttackState);
            return;
        }

        if (player.JumpPressed && player.Movement.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (Mathf.Abs(player.MoveInput.x) >= 0.01f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}
