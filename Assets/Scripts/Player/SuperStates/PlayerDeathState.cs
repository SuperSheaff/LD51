using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{

    private bool deathTime;
    public GameController gameController;

    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (!deathTime) {
            StartDeathTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        player.playerAudioManager.PlaySound("death");
        player.playerRigidBody.isKinematic = true;
        player.CameraShake();
        core.Movement.SetVelocityZero();

        gameController  = player.gameController;
        gameController.IncreaseDeathCount();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckDeathTime();

        if (!deathTime) {
            stateMachine.ChangeState(player.RespawnState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckDeathTime() {
        if (deathTime && (Time.time > startTime + playerData.deathTime)) {
            deathTime = false;
        }
    }

    public void StartDeathTime() => deathTime = true;

}
