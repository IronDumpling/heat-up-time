using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlane : MonoBehaviour
{
    [SerializeField] GameObject MainCameraObj;
    Camera mainCamera;
    public float cameraSizeSpeed;
    float cameraInitialSize;
    public float cameraFinalSize = 8.8f;

    bool trigger = false;

    //collide objs counter
    private List<GameObject> collideObjs;

    void Awake(){
        mainCamera = MainCameraObj.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraInitialSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            trigger = (!trigger);
        }
    }


}
