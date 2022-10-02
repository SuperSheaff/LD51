using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : PlayerState
{
    public PlayerRespawnState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        // stateMachine.ChangeState(player.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerRigidBody.isKinematic = false;
        player.transform.position = player.RespawnPoint.position;
        core.Movement.checkIfShouldFlip(1);
        player.HealthPoints = 100f;
        player.SetIsPoweredUp(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.IsPlayable) 
        {
            stateMachine.ChangeState(player.IdleState);
        }
        core.Movement.SetVelocityZero();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
