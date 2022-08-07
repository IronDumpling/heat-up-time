using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformDetector : MonoBehaviour
{
    public bool isGrounded;
    public bool check;

    PlayerController playercontroller;
    [SerializeField] Transform player;
    [SerializeField] GameObject detailedPlayer;

    void Awake(){
        playercontroller = detailedPlayer.GetComponent<PlayerController>();
    }    
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = (playercontroller.collideObjs.Count > 0);
        
        if (!isGrounded){
            check = false;
            if (Input.GetAxisRaw("Horizontal") > .25f || Input.GetAxisRaw("Horizontal") < -0.25f){
                player.SetParent(null);
            }
        }

        if (!check){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .1f);
            if (hit.collider != null){
                
                if (hit.collider.CompareTag("MovingHotterPlane")){
                    
                    player.SetParent(hit.transform);
                }else{
                    player.SetParent(null);
                }
                
                check = true;
            }
        }
    }
}
