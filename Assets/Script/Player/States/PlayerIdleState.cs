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
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.MoveInput.x) >= 0.01f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }
}
