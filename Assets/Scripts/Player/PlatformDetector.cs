using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformDetector : MonoBehaviour
{
    public bool isGrounded;
    public bool check;

    //PlayerController playercontroller;
    [SerializeField] Transform player;
    [SerializeField] GameObject detailedPlayer;

    bool isConnected = false;

    void Awake(){
        //playercontroller = detailedPlayer.GetComponent<PlayerController>();
    }

    //
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject target = collision.gameObject;
        
        //do nothing when the player is already binded
        //might be a problem when touching two platform at the same time
        if (!isConnected){
            switch(target.tag){
                case "MovingHotterPlane":
                    bindToObject(target);
                    break;
                case "MovingColderPlane":
                    bindToObject(target);
                    break;
                default:
                    break;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision) {
        GameObject target = collision.gameObject;

        if (isConnected){
            switch(target.tag){
                case "MovingHotterPlane":
                    unbind();
                    break;
                case "MovingColderPlane":
                    unbind();
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
            isConnected = true;
        }
        
    }

    void unbind(){
        player.SetParent(null);
        isConnected = false;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    isGrounded = playercontroller.isGrounded;

    //    if (!isGrounded){
    //        check = false;
    //        if (Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") < -0.25f){
    //            player.SetParent(null);
    //        }
    //    }

    //    if (!check){
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .1f);
    //        if (hit.collider != null){

    //            if (hit.collider.CompareTag("MovingHotterPlane")){

    //                player.SetParent(hit.transform);
    //            }else{
    //                player.SetParent(null);
    //            }

    //            check = true;
    //        }
    //    }
    //}


}
