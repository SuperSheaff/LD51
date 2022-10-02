using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

 private GameController      gameController;
    private Player              player;
    private Collider2D safeZone;

    // Start is called before the first frame update
    void Start()
    {
        player          = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameController  = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        safeZone         = GetComponent<Collider2D>();
    }

    void Update()
    {
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         player.SetIsSafe(true);
    //     }
    // }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.SetIsSafe(true);
        } else {
            player.SetIsSafe(false);
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         player.SetIsSafe(false);
    //     }
    // }
}
