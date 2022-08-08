using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlane : MonoBehaviour
{
    //[SerializeField] GameObject MainCameraObj;
    Camera mainCamera;
    PlayerHealth health;
    AudioManager audioManager;
    AudioCaller audCaller;
    public AudioClip bgm;

    //camera
    public float cameraEnlargeSpeed = 1f;
    public float cameraResumeSpeed = -10f;
    float cameraInitialSize;
    public float cameraFinalSize = 8.8f;
    
    //health
    public float healthRestoreSpeed = 1f;
    public float maxHealthIncreased = 2;


    //music
    bool musicIsPlaying = false;
    public float delayPaused = 0.5f;
    public float delayPlay = 0.5f;
    public float delayCount = 0f;
    public float defaultVolume = 1f;
    

    bool trigger = false;
    bool isOn = false;
    bool cameraIsFinished = true;
    bool maxHealthIsFinished = false;
    bool musicIsFinished = false;

    void Awake(){

        //assume there is only one camera
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        GameObject MainCameraObj = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        GameObject audioManagerObj = GameObject.FindGameObjectWithTag("AudioManager");

        mainCamera = MainCameraObj.GetComponent<Camera>();
        health = player.GetComponent<PlayerHealth>();
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        audCaller = FindObjectOfType<AudioCaller>();
        //if (bgm == null) Debug.LogAssertion("The safe plane has no bgm");
    }

    // Start is called before the first frame update
    void Start()
    {   
        cameraEnlargeSpeed = cameraEnlargeSpeed > 0? cameraEnlargeSpeed: -1 * cameraEnlargeSpeed;
        cameraResumeSpeed = cameraResumeSpeed < 0? cameraResumeSpeed: -1 * cameraResumeSpeed;
        cameraInitialSize = mainCamera.orthographicSize;
    }

    //{NOTE} do not add more things to these two functions
    //those function might got trigger more than once
    //do not add stuff that do different thing when call 
    //this function second times
    public void playerTrigger(){
        isOn = true; 
    }

    public void playerUnTrigger(){
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {   
        // do nothing is player is not on the plane
        
        // if (Input.GetKeyDown(KeyCode.R)){
        //     trigger = (!trigger);
        //     Debug.Log(trigger);
        // }
    }

    void FixedUpdate(){
        if (isOn){
            trigger = true;
            cameraIsFinished = false;
            musicIsFinished = false;
        }

        if (!trigger){
            return;
        }
        changeCamera();
        RestorePlayerHealth();
        adjustMusic();
        finalCheck();
    }
    
    void changeCamera(){
        float camearSpeed = isOn? cameraEnlargeSpeed: cameraResumeSpeed;
        mainCamera.orthographicSize += camearSpeed * Time.fixedDeltaTime;

        if (mainCamera.orthographicSize >= cameraFinalSize){
            mainCamera.orthographicSize = cameraFinalSize;
        }else if (mainCamera.orthographicSize <= cameraInitialSize) {
            mainCamera.orthographicSize = cameraInitialSize;
            cameraIsFinished = true;
        }   
    }

    void RestorePlayerHealth(){

        if (!maxHealthIsFinished){
            health.SetMaxHealth(health.maxHealth + maxHealthIncreased);
            maxHealthIsFinished = true;
        }
        health.Recover(healthRestoreSpeed * Time.fixedDeltaTime);
    }

    void adjustMusic(){
        if (bgm == null)
        {
            return;
        }
        float curVolume = audioManager.getVolume();
        if (isOn){
            if (!musicIsPlaying){
                
                //check if curBGM is our bgm
                if (audioManager.getCurBGM() != bgm){
                    audioManager.changeBGM(bgm);
                }

                audioManager.play();
                musicIsPlaying = true;
                audCaller.curAudStat = AudioCaller.AUDSTAT.SAFEPLANE;
                delayCount = 0f;

            }else if (curVolume < audioManager.getDefaultVolume()){
                audioManager.changeVolume(curVolume +(1f/delayPlay) * Time.fixedDeltaTime);
            }
     
        }else if (!isOn && musicIsPlaying){
            
            delayCount += Time.fixedDeltaTime;

            float newVolume = curVolume - (1f/delayPaused) * Time.fixedDeltaTime;
            //gradually reduce the volumn
            audioManager.changeVolume(newVolume);

            if (delayCount >= delayPaused || newVolume <= 0f){
                audioManager.pause();
                musicIsPlaying = false;
                audCaller.curAudStat = AudioCaller.AUDSTAT.ENTSONG;
                musicIsFinished = true;
                delayCount = 0f;
            }   
        }
    }

    void finalCheck(){
        if (cameraIsFinished && musicIsFinished){
            trigger = false;
        }
    }
}
