using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneCheck : MonoBehaviour
{

    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "SafeZone") 
        {
            Player.SetIsSafe(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.tag == "SafeZone") 
        {
            Player.SetIsSafe(false);
        }
    }
}
