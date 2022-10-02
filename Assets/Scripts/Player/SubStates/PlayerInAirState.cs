using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{

    private int xInput;
    private bool isGrounded;
    private bool jumpInput;
    private bool JumpInputHold;
    private bool coyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded      = core.CollisionSenses.Ground;
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

        CheckCoyoteTime();

        if (player.playerRigidBody.velocity.y < 0) {
            player.playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (playerData.fallMultiplier - 1) * Time.deltaTime;
        } else if (player.playerRigidBody.velocity.y > 0 && !JumpInputHold) {
            player.playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (playerData.lowJumpMultiplier - 1) * Time.deltaTime;
        }

        xInput          = player.InputHandler.NormInputX;
        jumpInput       = player.InputHandler.JumpInput;
        JumpInputHold   = player.InputHandler.JumpInputHold;

        if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) 
        {
            stateMachine.ChangeState(player.LandState);
        } 
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.SetIsPoweredUp(false);
            player.playerAnimator.SetBool("isPowered", false);
            stateMachine.ChangeState(player.JumpState);
        } 
        else {
            core.Movement.checkIfShouldFlip(xInput);
            core.Movement.SmoothDampVelocityX(playerData.movementSpeed * xInput, playerData.movementSmoothing);

            player.playerAnimator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.playerAnimator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        }

        if (player.playerRigidBody.velocity.y < 0) {
            player.playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (playerData.fallMultiplier - 1) * Time.deltaTime;
        } else if (player.playerRigidBody.velocity.y > 0 && !JumpInputHold) {
            player.playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (playerData.lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime() {
        if (coyoteTime && (Time.time > startTime + playerData.coyoteTime)) {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

}
