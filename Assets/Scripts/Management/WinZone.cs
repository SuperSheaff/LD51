using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{

    public GameController gameController;
    public BoxCollider2D winZone;

    // Start is called before the first frame update
    void Start()
    {
        gameController  = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        winZone         = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameController.WinGame();
        }
    }
}
