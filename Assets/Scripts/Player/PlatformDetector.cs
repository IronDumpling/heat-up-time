using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformDetector : MonoBehaviour
{

    //PlayerController playercontroller;
    [SerializeField] Transform player;
    [SerializeField] GameObject detailedPlayer;

    public bool isGrounded = false;

    void Awake(){
        //playercontroller = detailedPlayer.GetComponent<PlayerController>();
    }

    //
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject target = collision.gameObject;
        
        //do nothing when the player is already binded
        //might be a problem when touching two platform at the same time
        if (!isGrounded){
            Debug.Log(target.tag);
            switch(target.tag){
                case "MovingHotterPlane":
                    bindToObject(target);
                    break;
                case "MovingColderPlane":
                    bindToObject(target);
                    break;
                case "SafePlane":
                    SendMessageToSafePlane(target, true);
                    break;
                default:
                    break;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision) {
        GameObject target = collision.gameObject;

        if (isGrounded){
            switch(target.tag){
                case "MovingHotterPlane":
                    unbind();
                    break;
                case "MovingColderPlane":
                    unbind();
                    break;
                case "SafePlane":
                    SendMessageToSafePlane(target, false);
                    break;
                default:
                    break;
            }
        }
    }

    void bindToObject(GameObject target){
        
        Transform targetTransform = target.GetComponent<Transform>();
        
        //player must be on top of the platform, might change when encounter tilting platform
        if (transform.position.y >= targetTransform.position.y){
            player.SetParent(target.GetComponent<Transform>());
            isGrounded = true;
        }
        
    }

    void unbind(){
        player.SetParent(null);
        isGrounded = false;
    }

    //tell the safe plane to start to do its thing
    //this will be trigger more than once when player enter the plane
    void SendMessageToSafePlane(GameObject target, bool trigger){
        SafePlane sp = target.GetComponent<SafePlane>();

        if (trigger){
            sp.playerTrigger();
            isGrounded = true;
        }else{
            sp.playerUnTrigger();
            isGrounded = false;
        }   
    }


}
