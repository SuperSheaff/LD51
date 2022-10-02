using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // player.playerAudioManager.PlaySound("PlayerLand");
        // player.playerLandParticles.Play();
    }

    public override void Exit()
    {
        base.Exit();
        // player.playerLandParticles.Stop();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState) {
            if (xInput != 0) {
                stateMachine.ChangeState(player.MoveState);
            // } else if (isAnimationFinished) {
            //     stateMachine.ChangeState(player.IdleState);
            // }
            } else {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
