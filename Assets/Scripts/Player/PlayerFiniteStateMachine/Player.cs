using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variables

    public PlayerStateMachine       StateMachine        { get; private set; }

    public PlayerRespawnState       RespawnState        { get; private set; }
    public PlayerDeathState         DeathState          { get; private set; }
    public PlayerIdleState          IdleState           { get; private set; }
    public PlayerMoveState          MoveState           { get; private set; }
    public PlayerJumpState          JumpState           { get; private set; }
    public PlayerInAirState         InAirState          { get; private set; }
    public PlayerLandState          LandState           { get; private set; }
    public PlayerChargeState        ChargeState         { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    public GameController gameController;

    [SerializeField]
    public CameraController CameraController;

    #endregion

    #region Components

    [SerializeField]

    public Core                 Core                { get; private set; }
    public Animator             playerAnimator      { get; private set; }
    public PlayerInputHandler   InputHandler        { get; private set; }
    public Rigidbody2D          playerRigidBody     { get; private set; }
    public BoxCollider2D        playerBoxCollider   { get; private set; }
    public PlayerAudioManager   playerAudioManager  { get; private set; }
    public ParticleSystem       playerRunParticles;
    public ParticleSystem       playerLandParticles;
    public ParticleSystem       playerWallLandParticles;
    public ParticleSystem       playerJumpParticles;
    public ParticleSystem       playerGroundSlideParticles;

    public float                HealthPoints;
    public GameObject           HealthBar;

    #endregion

    #region Check Transforms

    [SerializeField]
    public Transform StartingSpawn;

    [SerializeField]
    public Transform RespawnPoint;

    #endregion

    #region Other Variables

    private Vector2             workspace;
    private Vector2             referenceVelocity;

    private bool                canPowerUp;
    private bool                isPoweredUp;
    private bool                isSafe;
    private bool                isSizzling;

    public bool                IsPlayable;

    #endregion

    #region Unity Callback Functions

        private void Awake() {

            Core = GetComponentInChildren<Core>();

            StateMachine        = new PlayerStateMachine();

            DeathState          = new PlayerDeathState(this, StateMachine, playerData, "death");
            RespawnState        = new PlayerRespawnState(this, StateMachine, playerData, "respawn");
            IdleState           = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState           = new PlayerMoveState(this, StateMachine, playerData, "move");
            JumpState           = new PlayerJumpState(this, StateMachine, playerData, "inAir");
            InAirState          = new PlayerInAirState(this, StateMachine, playerData, "inAir");
            LandState           = new PlayerLandState(this, StateMachine, playerData, "land");
            ChargeState         = new PlayerChargeState(this, StateMachine, playerData, "charge");

        }

        private void Start() {
            playerAnimator      = GetComponent<Animator>();
            InputHandler        = GetComponent<PlayerInputHandler>();
            playerRigidBody     = GetComponent<Rigidbody2D>();
            playerBoxCollider   = GetComponent<BoxCollider2D>();
            playerAudioManager  = GetComponent<PlayerAudioManager>();

            referenceVelocity       = Vector2.zero;
            RespawnPoint.position   = StartingSpawn.position;
            canPowerUp              = false;
            isPoweredUp             = false;
            isSizzling              = false;
            
            IsPlayable              = false;

            HealthPoints            = 100f;

            StateMachine.Initialize(RespawnState);
        }

        private void Update() {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();   
        }

        private void FixedUpdate() {

            // Debug.Log("Safe: " + isSafe);
            // Debug.Log("CanCharge: " + canPowerUp);


            if (isSafe || !gameController.IsRaining) 
            {
                playerAudioManager.StopSound("sizzle");
                isSizzling = false;

                if (HealthPoints < 100f) 
                {
                    HealthPoints = HealthPoints + 1.5f;
                }
            }

            if (!isSafe && gameController.IsRaining) 
            {
                
                if (!isSizzling && StateMachine.CurrentState != DeathState) {
                    playerAudioManager.PlaySound("sizzle");
                    isSizzling = true;
                }

                if (HealthPoints > 0f) 
                {
                    HealthPoints = HealthPoints - 1.5f;
                }
            }

            if (HealthPoints <= 0f) 
            {
                if (StateMachine.CurrentState != DeathState) {
                    playerAudioManager.StopSound("sizzle");
                    isSizzling = false;
                    StateMachine.ChangeState(DeathState);
                }

                HealthBar.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            } else {
                HealthBar.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                HealthBar.transform.localScale = new Vector3(16 * (HealthPoints / 100), 1, 1);

                if (HealthPoints >= 100f) {
                    HealthBar.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0f);
                }
            }

            StateMachine.CurrentState.PhysicsUpdate();    
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log(collision.collider.name);
            // if ((collision.collider.tag == "Obstacle" || (collision.collider.tag == "EnemyObstacle" && !isGroundSliding)) && StateMachine.CurrentState != DeathState) 
            // {
            //     LoseHealth(1);
            //     Core.Combat.setCurrentHealth(currentHealth);
            //     StateMachine.ChangeState(DeathState);   
            // }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.tag == "SafeZone") 
            {
                isSafe = true;
            }

            if (collision.tag == "ChargeZone") 
            {
                canPowerUp = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (collision.tag == "SafeZone") 
            {
                isSafe = true;
            }

            if (collision.tag == "ChargeZone") 
            {
                canPowerUp = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.tag == "SafeZone") 
            {
                isSafe = false;
            }

            if (collision.tag == "ChargeZone") 
            {
                canPowerUp = false;
            }
        }

    #endregion

    #region Set Functions

        public void SetColliderHeight(float height, float offset) 
        {
            Vector2 center = playerBoxCollider.offset;
            workspace.Set(playerBoxCollider.size.x, height);
            
            center.y += offset;

            playerBoxCollider.size = workspace;
            playerBoxCollider.offset = center;
        }

        public void SetSpawnPoint(Vector3 newPosition) 
        {
            RespawnPoint.position   = newPosition;
        }

        public void SetIsSafe(bool value) 
        {
            isSafe = value;
        }

        public void SetIsPoweredUp(bool value) 
        {
            isPoweredUp = value;

            if (value)
            {
                playerAnimator.SetBool("isPowered", true);
            } else {
                playerAnimator.SetBool("isPowered", false);
            }
        }

    #endregion

    #region Get Functions

        public bool GetIsPoweredUp() 
        {
            return isPoweredUp;
        }

        public bool GetCanPowerUp() 
        {
            return canPowerUp;
        }

    #endregion

    #region Trigger Functions

        private void AnimationTrigger()                 => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishedTrigger()         => StateMachine.CurrentState.AnimationFinishedTrigger();
        private void AnimationStartMovementTrigger()    => StateMachine.CurrentState.AnimationStartMovementTrigger();
        private void AnimationStopMovementTrigger()     => StateMachine.CurrentState.AnimationStopMovementTrigger();
        private void AnimationTurnOffFlip()             => StateMachine.CurrentState.AnimationTurnOffFlip();
        private void AnimationTurnOnFlip()              => StateMachine.CurrentState.AnimationTurnOnFlip();
        private void AnimationActionTrigger()           => StateMachine.CurrentState.AnimationActionTrigger();

    #endregion
    
    
    #region Other Functions

    public void ResetGame() {
        Core.Movement.checkIfShouldFlip(1);
        SetSpawnPoint(gameController.GetCheckpoint().position);
        StateMachine.ChangeState(RespawnState);
    }

    public void CameraShake() {
        StartCoroutine(CameraController.ShakeTheCamera(1f, 0.5f));
    }

    #endregion

}
