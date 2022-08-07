using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlane : MonoBehaviour
{
    //[SerializeField] GameObject MainCameraObj;
    Camera mainCamera;
    PlayerHealth health;
    AudioSource music;

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
    

    bool trigger = false;
    bool isOn = false;
    bool cameraIsFinished = true;
    bool maxHealthIsFinished = false;
    bool musicIsFinished = false;

    void Awake(){

        //assume there is only one camera
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        GameObject MainCameraObj = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        mainCamera = MainCameraObj.GetComponent<Camera>();
        health = player.GetComponent<PlayerHealth>();
        music = GetComponent<AudioSource>();
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
        Debug.Log("Player on safe plane");
    }

    public void playerUnTrigger(){
        isOn = false;
        Debug.Log("Player leave safe plane");
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
        if (isOn){
            if (!musicIsPlaying){
                music.Play();
                musicIsPlaying = true;
                delayCount = 0f;
            }else if (music.volume < 1f){
                music.volume += (1f/delayPlay) * Time.fixedDeltaTime;
                if (music.volume > 1f)music.volume = 1f; 
            }
     
        }else if (!isOn && musicIsPlaying){
            
            delayCount += Time.fixedDeltaTime;

            //gradually reduce the volumn
            music.volume -= (1f/delayPaused) * Time.fixedDeltaTime;
            if (music.volume < 0f) music.volume = 0f;

            if (delayCount >= delayPaused || music.volume <= 0f){
                music.Pause();
                musicIsPlaying = false;
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
