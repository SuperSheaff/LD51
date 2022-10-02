using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCheckpoint : MonoBehaviour
{

    public Player               player;
    public BoxCollider2D        softCheckpointZone;
    private bool                isFirstEntry;

    // Start is called before the first frame update
    void Start()
    {
        player              = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        softCheckpointZone  = GetComponent<BoxCollider2D>();
        isFirstEntry        = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isFirstEntry)
            {
                player.SetSpawnPoint(this.transform.position);
            }
        }
    }
}
