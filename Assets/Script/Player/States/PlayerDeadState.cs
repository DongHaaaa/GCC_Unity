public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Movement.Stop();
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsJumping", false);
        player.Animator.SetBool("IsAttacking", false);
        player.Animator.SetBool("IsDashing", false);
        player.Animator.SetBool("IsHurt", false);
        player.Animator.SetBool("IsDead", true);
    }
}
