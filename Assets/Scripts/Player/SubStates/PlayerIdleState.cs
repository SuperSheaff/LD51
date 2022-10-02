using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        core.Movement.SmoothDampVelocityX(playerData.movementSpeed * xInput, playerData.movementSmoothing);

        if (!isExitingState) 
        {
            if (xInput != 0) 
            {
                stateMachine.ChangeState(player.MoveState);
            } 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
