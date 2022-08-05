using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components pointer
    private Rigidbody2D rigBody;
    private GameObject collideObj;
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    // Movement facters
    public float velocity;
    public float xInput;
    public float jumpForce;
    public float bulletVelocity;
    // Flags
    public bool isOnPlane;
    public int jumpCount = 2;
    public bool pressJump = false;
    private Transform PlayerGnd;
    // Falling Variables
    private int lowerBound;
    public float fallingDamage;
    private Vector3 lastPlanePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get this Components
        rigBody = GetComponent<Rigidbody2D>();

        // Falling Variables
        lowerBound = -20;
        fallingDamage = GetComponent<PlayerHealth>().maxHealth/10;
        lastPlanePosition = new Vector3(0, -2.5f, 0);

        // Movement Variables
        velocity = 2f;
        jumpForce = 7f;
        jumpCount = 2;
        PlayerGnd = getChildGameObject(this.gameObject, "PlayerGnd").transform;
    }

    // Update is called once per frame
    void Update()
    {
        TriggerJump();

        // Decrease Health by Falling
        if (transform.position.y < lowerBound) 
        {
            GetComponent<PlayerHealth>().Damage(fallingDamage);

            if (GetComponent<PlayerHealth>().curHealth > 0)
            {
                BackToPlane();
            }
        }
    }

    void TriggerJump() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            pressJump = true;
        }
    }

    // FixedUpdate for physics events 
    void FixedUpdate()
    {
        HorizontalMove();
        OnPlaneCheck();
        JumpHandler();
    }

    // Method 1. Movement
    void HorizontalMove()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rigBody.velocity = new Vector2(xInput * velocity, rigBody.velocity.y);
    }

    // Method 2. Plane collision check
    void OnPlaneCheck()
    {
        if(Physics2D.OverlapCircle(PlayerGnd.position, 0.1f, planeLayer.value)) {
            isOnPlane = true;
            jumpCount = 2;
        }
        else {
            isOnPlane=false;
        }
    }

    // Method 3. Jump
    void JumpHandler()
    {
        if (pressJump) {
            if (jumpCount > 0) {
                rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
                jumpCount--;
            }
            pressJump = false;
        }
    }

    // Method 4. Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;

        if (collideObj.layer == 7) // 7 is layer of villains
        {
            // Heat Change if collide villains
            float otherHeat = collideObj.GetComponent<VillainController>().curHeat; 
            if (otherHeat != GetComponent<PlayerHeat>().curHeat)
            {
                GetComponent<PlayerHeat>().HeatTransfer(otherHeat);
            }

            isOnPlane = true;

            // Health Change if collide villains
            float damage = collideObj.GetComponent<VillainController>().damage; 
            GetComponent<PlayerHealth>().Damage(damage);
        }

        
        else if (collideObj.layer == 9 && collideObj.tag != "SafePlane") // 9 is layer of platforms and not safe plane
        {
            // Heat Change if collide platforms
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            if (otherHeat != GetComponent<PlayerHeat>().curHeat)
            {
                GetComponent<PlayerHeat>().HeatTransfer(otherHeat);
            }

            isOnPlane = true;

            // Record the last landing place if collide platform
            lastPlanePosition = collideObj.transform.position;
            lastPlanePosition.y += 0.5f;
        }    
    }

    // Method 5. Back to the last collision plane
    void BackToPlane()
    {
        transform.position = lastPlanePosition;
        rigBody.velocity = Vector2.zero;
    }


    static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}

