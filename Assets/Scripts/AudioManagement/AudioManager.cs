using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource manager;
    public AudioClip curBGM;

    void Awake(){
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
    }

    public AudioClip getCurBGM(){
        return curBGM;
    }

    public void changeVolume(float newVolume){
        manager.volume = Mathf.Clamp(newVolume, 0f, 1f);
    }

    public float getVolume(){
        return manager.volume;
    }

    public void play(){
        manager.Play();
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
