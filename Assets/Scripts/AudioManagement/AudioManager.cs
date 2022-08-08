using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainTrack, sideTrack;
    private AudioSource curTrack;

    public float timeToFade = 1.25f;

    public AudioClip curBGM;
    public float defaultVolume;
    float delay = 0f;
    public static AudioManager instance;
    bool first = false;
    void Awake(){
        if (GameObject.FindGameObjectsWithTag("AudioManager").Length > 1){
            if (!first) Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        first = true;
        mainTrack.clip = curBGM;
        curTrack = mainTrack;
    }

    public void SwapTrack(AudioClip newClip) {
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));
    }

    private IEnumerator FadeTrack(AudioClip newClip) {
        float timeElapsed = 0f;
        AudioSource nextTrack;
        if (curTrack == mainTrack) nextTrack = sideTrack; 
        else nextTrack = mainTrack;

        nextTrack.clip = newClip;
        nextTrack.Play();

        while (timeElapsed < timeToFade) {
            curTrack.volume = Mathf.Lerp(defaultVolume, 0, timeElapsed / timeToFade);
            nextTrack.volume = Mathf.Lerp(0, defaultVolume, timeElapsed / timeToFade);
            timeElapsed+=Time.deltaTime;
            yield return null;
        }

        curTrack.Stop();
        curTrack = nextTrack;
    }



    // Start is called before the first frame update
    void Start()
    {
        if (curBGM != null){
            curTrack.Play();
        }
    }

    //Stop the current BGM and swap to another BGM
    //note that it won't play it, use play() to play
    public void changeBGM(AudioClip newBGM){
        if (curTrack.isPlaying){
            curTrack.Stop();
        }
        curTrack.clip = newBGM;
        curBGM = newBGM;
        curTrack.volume = defaultVolume;
    }

    public AudioClip getCurBGM(){
        return curBGM;
    }

    public float getDefaultVolume(){
        return defaultVolume;
    }
    public void changeVolume(float newVolume){
        curTrack.volume = Mathf.Clamp(newVolume, 0f, 1f);
    }

    public void changeDefaultVolume(float newVolume){
        defaultVolume = Mathf.Clamp(newVolume, 0f, 1f);
        curTrack.volume = defaultVolume;
    }
    public float getVolume(){
        return curTrack.volume;
    }

    public void play(){
        curTrack.Play();
    }

    public void playWithDelay(float newDelay){
        if (delay <= 0) return;
        delay = newDelay;
        //trigger
    }

    public void pause(){
        curTrack.Pause();
    }
    
    public void restart(){
        curTrack.Stop();
        curTrack.Play();
    }
    public bool isPlaying()
    {
        return curTrack.isPlaying;
    }


}
