using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameController      gameController;
    private BoxCollider2D       checkpointZone;
    private Player              player;

    public GeneralAudioManager  audioManager           { get; private set; }
    public Animator             checkpointAnimator      { get; private set; }

    private bool                isFirstEntry;


    // Start is called before the first frame update
    private void Start()
    {

        player              = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameController      = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        checkpointZone      = GetComponent<BoxCollider2D>();
        // checkpointAnimator  = GetComponent<Animator>();
        // audioManager        = GetComponent<GeneralAudioManager>();

        isFirstEntry        = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isFirstEntry)
            {
                player.SetSpawnPoint(this.transform.position);
                gameController.SetCheckpoint(this.transform);
                // audioManager.PlaySound("checkpointReached");
                // checkpointAnimator.SetBool("checkpointReached", true);
                isFirstEntry = false;
            }
        }
    }
}
