using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
        amountOfJumpsLeft = 1;
    }

    public override void Enter()
    {
        base.Enter();

        player.playerAudioManager.PlaySound("jump");
        // player.playerJumpParticles.Play();
        player.InputHandler.UseJumpInput();
        core.Movement.SetVelocityY(playerData.jumpForce);
        isAbilityDone = true;
        amountOfJumpsLeft--;
    }

    public bool CanJump() {
        if (amountOfJumpsLeft > 0) 
        {
            return true;
        } else {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() 
    {
        if (player.GetIsPoweredUp()) 
        {
            amountOfJumpsLeft = 2;
        } else {
            amountOfJumpsLeft = 1;
        }
    }

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
