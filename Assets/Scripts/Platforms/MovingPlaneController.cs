using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingPlaneController : MonoBehaviour
{
    
    //constant
    const float PRECISION = 0.0001f;
    public float speed = 1f;
    int target;
    
    public List<Transform> waypoints;
    /*
    protected override void Start(){
        base.Start();
        checkpoints.Insert(0, transform.position);
        Debug.Log(checkpoints.Count);
        currCheckpoint = 0;
        direction = 1;
    }*/
    void Update(){
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, speed * Time.deltaTime);
    }

    public void FixedUpdate()
    {
        MovingHandler();
    }

    void MovingHandler(){
        if (transform.position == waypoints[target].position){
            if (target == waypoints.Count - 1){
                target = 0;
            }else{
                target += 1;
            }
        }

        /*
        //always assume the correct direction is already determined
        nextCheckpoint = currCheckpoint + direction;

        if (loop && direction == 1 && nextCheckpoint >= checkpoints.Count){
            nextCheckpoint = 0;
        }else if (loop && direction == -1 && nextCheckpoint >= 0){
            nextCheckpoint = checkpoints.Count - 1;
        }

        if (nextCheckpoint >= checkpoints.Count || nextCheckpoint < 0) Debug.Log("Next Checkpoint out of index");

        Vector3 currPos = Vector3.MoveTowards(transform.position, checkpoints[nextCheckpoint], speed * Time.fixedDeltaTime);
        transform.position = currPos;
        
        //check if platform has reached the checkpoint
        if ((currPos - checkpoints[currCheckpoint]).magnitude >= (checkpoints[nextCheckpoint] - checkpoints[currCheckpoint]).magnitude){
            //move the platform back to the checkpoint if platform pass the checkpoint
            transform.position = checkpoints[nextCheckpoint];

            currCheckpoint = nextCheckpoint;
            
            //change direction if reach the end
            if (currCheckpoint == checkpoints.Count - 1 || currCheckpoint == 0){
                if (!loop){
                    direction *= -1;
                }
            }
        }*/
        

    }
}
