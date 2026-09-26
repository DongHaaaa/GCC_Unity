using UnityEngine;

public class PlayerHurtState : PlayerState
{
    private float timer;

    public PlayerHurtState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timer = player.HurtDuration;
        player.Movement.StopHorizontal();
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsAttacking", false);
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
}
