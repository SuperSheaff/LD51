using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuSystem : MonoBehaviour
{

    public void LoadGame() 
    {
        SceneManager.LoadScene("Stage2");
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    
}
