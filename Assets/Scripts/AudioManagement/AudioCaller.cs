using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioCaller : MonoBehaviour
{
    public AudioClip MainTitle_slow;
    public AudioClip MainTitle_fast;
    public AudioClip Intro;
    public AudioClip Breakdown;
    public AudioClip Outro;
    public AudioClip SafePlane;
    public AudioClip EntireSong;
    public AudioManager audioManager;
    protected GameObject[] villains;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        villains = GameObject.FindGameObjectsWithTag("Villain");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "HomeMenu")
            audioManager.changeBGM(EntireSong);
        else if (SceneManager.GetActiveScene().name == "Level 1")
            audioManager.changeBGM(Intro);
        else if (SceneManager.GetActiveScene().name == "Level 2")
            audioManager.changeBGM(Breakdown);
        else if (SceneManager.GetActiveScene().name == "Level 3")
            audioManager.changeBGM(Intro);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            audioManager.SwapTrack(MainTitle_fast);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            audioManager.SwapTrack(Outro);
        }

        // Safe Platform has the highest priority
        if (audioManager.isPlaying() &&
           audioManager.curBGM == SafePlane)
        {
            return;
        } 

        // Change BGM according to time scale during battle
        else if (InBattle())
        {
            BattleBGM();
        }
        // Not in Battle
        else
        {

        }
    }

    protected bool InBattle()
    {
        PlayerController pC = FindObjectOfType<PlayerController>();
        if(pC == null) return false;
        return pC.inBattle > 0;
    }

    protected void BattleBGM()
    {
        if (Time.timeScale < 0.5)
        {
            audioManager.SwapTrack(MainTitle_slow);
        }
        else
        {
            audioManager.SwapTrack(MainTitle_fast);
        }
    }
}
