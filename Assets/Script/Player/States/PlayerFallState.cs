using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Animator.SetBool("IsJumping", false);
        player.Animator.SetBool("OnGround", false);
    }

    public override void Update()
    {
        base.Update();

        float moveX = player.MoveInput.x;

        player.Movement.Flip(moveX);
        player.Animator.SetFloat("yVelocity", player.VerticalVelocity);

        if (player.Movement.IsGrounded)
        {
            if (Mathf.Abs(moveX) >= 0.01f)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.Move(player.MoveInput.x);
    }
}
