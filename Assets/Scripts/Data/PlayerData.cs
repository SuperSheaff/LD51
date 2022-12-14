using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementSpeed = 150f;
    public float movementSmoothing = 0.01f;

    [Header("Jump State")]
    public float jumpForce = 305f;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 10f;

    [Header("In Air State")]
    public float coyoteTime = 0.1f;

    [Header("Death State")]
    public float deathTime      = 2f;
    public int startingHealth   = 3;

}
