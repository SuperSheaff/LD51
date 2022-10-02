using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeState : PlayerState
{

    private bool chargeInput;
    private bool lockedIntoCharge;

    public PlayerChargeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        player.SetIsPoweredUp(true);
        player.playerAnimator.SetBool("isPowered", true);
        player.JumpState.ResetAmountOfJumpsLeft();
        stateMachine.ChangeState(player.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        lockedIntoCharge = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.playerAudioManager.PlaySound("charge");
        lockedIntoCharge = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();
        chargeInput     = player.InputHandler.ChargeInput;

        // need to add && in charge zone
        if (!chargeInput && !lockedIntoCharge) 
        {
            player.playerAudioManager.StopSound("charge");
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
