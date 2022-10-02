using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Camera MainCamera;
    public GameObject RainObjects;
    public GameObject RainSplatterObjects;
    public ParticleSystem[] RainParticleSystems;
    public ParticleSystem[] RainSplatterParticleSystems;

    public GameObject Logo;
    public GameObject PlayButton;
    public GameObject TextMessages;

    public GameObject YouWonMenu;
    public GameObject StartMenu;

    private float timerStartTime;
    private float finishingTime;
    private bool stopTimer;
    private bool TimerEnable;

    private float rainStartTime;
    private float rainWaitTime;
    private float rainFXWaitTime;
    private bool isPlayingRainFX;

    public Text DeathCountText;
    public Text FinishTimeText;

    public bool IsRaining;
    private bool isIntro;

    private int deathCount;
    private int countingMsg = 1;

    public Transform StartingSpawn;
    public Transform Checkpoint;

    private GeneralAudioManager audioManager;

    public Player Player;

    private void Start() 
    {
        ResetTimer();
        stopTimer = false;
        TimerEnable = true;
        audioManager = GetComponent<GeneralAudioManager>();
        audioManager.PlaySound("music");

        deathCount = 0;

        rainStartTime   = Time.time;
        IsRaining       = false;
        isPlayingRainFX = false;

        StopAllRainParticles();
        StopAllRainSplatterParticles();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (isIntro) 
        {
            if (Input.GetKeyDown("space"))
            {
                TextMessages.transform.GetChild(countingMsg).gameObject.SetActive(true);
                countingMsg++;
            }

            if (countingMsg >= 11)
            {
                StartGame();
            }
        } else {

            if (IsRaining)
            {
                rainWaitTime = 5f;
                rainFXWaitTime  = 9f;
            } else {
                rainWaitTime = 10f;
                rainFXWaitTime  = 9f;
            }

            if (Time.time >= rainStartTime + rainFXWaitTime)
            {
                if (!IsRaining) {
                    if (!isPlayingRainFX) {
                        audioManager.PlaySound("rain");
                        isPlayingRainFX = true;
                        StartAllRainParticles();
                    }
                }
            }

            if (Time.time >= rainStartTime + rainWaitTime)
            {
                if (IsRaining) 
                {
                    IsRaining = false;
                    isPlayingRainFX = false;
                    StopAllRainParticles();
                    StopAllRainSplatterParticles();
                } else {
                    IsRaining = true;
                    StartAllRainSplatterParticles();
                }

                rainStartTime = Time.time;
            }

            if (!stopTimer)
            {
                float time = Time.time - timerStartTime;
                string minutes = ((int) time / 60).ToString();
                string seconds = (time % 60).ToString("f2");
            }
        }
    }

    public void WinGame() {
        audioManager.PlaySound("Win1");
        YouWonMenu.SetActive(true);
        Time.timeScale = 0f;
        DeathCountText.text = "You died " + deathCount.ToString() + " times";
        
        float time = Time.time - timerStartTime;
        string minutes = ((int) time / 60).ToString();
        string seconds = (time % 60).ToString("f2");
        FinishTimeText.text = minutes + " : " + seconds;
    }

    public void RemoveAllScreens() {
        YouWonMenu.SetActive(false);
    }

    public void HardResetGame() {
        SetCheckpoint(StartingSpawn.transform);
        Player.ResetGame();
        RemoveAllScreens();
        ResetTimer();
    }

    // public void LoadMenu() 
    // {
    //     SceneManager.LoadScene("StartMenu");
    // }

    // public void LoadGame() 
    // {
    //     SceneManager.LoadScene("Game");
    // }

    // public void QuitGame() 
    // {
    //     Debug.Log("Quitting game...");
    //     Application.Quit();
    // }

    public void SetCheckpoint(Transform newCheckpoint) 
    {
        Checkpoint.position = newCheckpoint.position;
    }

    public Transform GetCheckpoint() 
    {
        return Checkpoint;
    }

    public void ResetTimer()
    {
        StartTimer();
        timerStartTime = Time.time;
    }

    public void StopTimer()
    {
        stopTimer = true;
    }

    public void StartTimer()
    {
        stopTimer = false;
    }

    public void IncreaseDeathCount()
    {
        deathCount += 1;
    }

    public void ResetDeathCount()
    {
        deathCount = 0;
    }

    public void StopAllRainParticles() 
    {
        RainParticleSystems = RainObjects.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in RainParticleSystems) {
            particleSystem.Stop();
        }
    }

    public void StartAllRainParticles() 
    {
        RainParticleSystems = RainObjects.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in RainParticleSystems) {
            particleSystem.Play();
        }
    }

    public void StopAllRainSplatterParticles() 
    {
        RainSplatterParticleSystems = RainSplatterObjects.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in RainSplatterParticleSystems) {
            particleSystem.Stop();
        }
    }

    public void StartAllRainSplatterParticles() 
    {
        RainSplatterParticleSystems = RainSplatterObjects.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in RainSplatterParticleSystems) {
            particleSystem.Play();
        }
    }

    public void StartIntro() 
    {
        PlayButton.SetActive(false);
        Logo.SetActive(false);
        TextMessages.SetActive(true);
        TextMessages.transform.GetChild(0).gameObject.SetActive(true);
        isIntro = true;
    }

    public void StartGame() 
    {
        isIntro = false;
        StartMenu.SetActive(false);
        rainStartTime   = Time.time;
        Player.IsPlayable = true;
    }
}
