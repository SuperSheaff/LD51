using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected int yInput;
    
    protected bool isAbilityDone;
    private bool isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        yInput = player.InputHandler.NormInputY;

        if (isAbilityDone) {
            if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) {
                stateMachine.ChangeState(player.IdleState);
            } else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
