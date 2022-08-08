using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource manager;
    public AudioClip curBGM;
    public float defaultVolume;
    bool trigger = false;
    float delay = 0f;
    public static AudioManager instance;
    bool first = false;
    void Awake(){
        if (GameObject.FindGameObjectsWithTag("AudioManager").Length > 1){
            if (!first) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        first = true;
        manager = GetComponent<AudioSource>();
        manager.clip = curBGM;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (curBGM != null){
            manager.Play();
        }
    }

    //Stop the current BGM and swap to another BGM
    //note that it won't play it, use play() to play
    public void changeBGM(AudioClip newBGM){
        if (manager.isPlaying){
            manager.Stop();
        }
        manager.clip = newBGM;
        curBGM = newBGM;
        manager.volume = defaultVolume;
    }

    public AudioClip getCurBGM(){
        return curBGM;
    }

    public float getDefaultVolume(){
        return defaultVolume;
    }
    public void changeVolume(float newVolume){
        manager.volume = Mathf.Clamp(newVolume, 0f, 1f);
    }

    public void changeDefaultVolume(float newVolume){
        defaultVolume = Mathf.Clamp(newVolume, 0f, 1f);
        manager.volume = defaultVolume;
    }
    public float getVolume(){
        return manager.volume;
    }

    public void play(){
        manager.Play();
    }

    public void playWithDelay(float newDelay){
        if (delay <= 0) return;
        delay = newDelay;
        //trigger
    }

    public void pause(){
        manager.Pause();
    }
    
    public void restart(){
        manager.Stop();
        manager.Play();
    }

    public bool isPlaying()
    {
        return manager.isPlaying;
    }
}
