using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private Vector3             referenceVelocity;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerAudioManager.PlaySound("steps");
        referenceVelocity = Vector3.zero;
        // player.playerRunParticles.Play();
    }

    public override void Exit()
    {
        base.Exit();
        player.playerAudioManager.StopSound("steps");
        // player.playerRunParticles.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SmoothDampVelocityX(playerData.movementSpeed * xInput, playerData.movementSmoothing);

        if (!isExitingState) {
            if (xInput == 0) {
                stateMachine.ChangeState(player.IdleState);
            } 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
