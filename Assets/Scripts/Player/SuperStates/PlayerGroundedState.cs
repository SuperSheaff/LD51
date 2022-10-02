using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    protected Vector2 input;
    protected int xInput;
    protected int yInput;

    private bool jumpInput;
    private bool chargeInput;
    private bool isGrounded;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        xInput          = player.InputHandler.NormInputX;
        yInput          = player.InputHandler.NormInputY;
        jumpInput       = player.InputHandler.JumpInput;
        chargeInput     = player.InputHandler.ChargeInput;

        core.Movement.checkIfShouldFlip(xInput);

        if (jumpInput && player.JumpState.CanJump()) {
            stateMachine.ChangeState(player.JumpState);
        } else if (!isGrounded) {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }

        if (chargeInput && !player.GetIsPoweredUp() && player.GetCanPowerUp()) {
            stateMachine.ChangeState(player.ChargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
