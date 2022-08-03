using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Components pointer
    private Rigidbody2D rigBody;
    private Collider2D coll;
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
    public bool notJumped;
    public bool pressJump;
    // Falling Variables
    private int lowerBound;
    public float fallingDamage;
    private Vector3 lastPlanePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get this Components
        rigBody = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        // Falling Variables
        lowerBound = -20;
        fallingDamage = GetComponent<PlayerHealth>().maxHealth/10;
        lastPlanePosition = new Vector3(0, -2.5f, 0);

        // Movement Variables
        velocity = 2f;
        jumpForce = 7f;
        notJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) // TODO: Need a jump buffer
            && notJumped)
        {
            pressJump = true;
        }

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

    // FixedUpdate for physics events 
    void FixedUpdate()
    {
        OnPlaneCheck();
        HorizontalMove();
        Jump();
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
        if (!coll.IsTouchingLayers(planeLayer) &&
            !coll.IsTouchingLayers(villainLayer))
        {
            isOnPlane = false;
        }
    }

    // Method 3. Jump
    void Jump()
    {
        // Case 1. Stay on the planes
        if (isOnPlane)
        {
            notJumped = true;
        }
        // Case 2. Jump on the Plane or in the Air
        if (pressJump)
        {
            rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
            notJumped = false;
            pressJump = false;
        }
    }

    // Method 4. Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;
        
        if (coll.IsTouchingLayers(villainLayer) && collideObj)
        {
            // Heat Change if collide villains
            float otherHeat = collideObj.GetComponent<VillainController>().curHeat;
            if (otherHeat != GetComponent<VillainController>().curHeat)
            {
                GetComponent<PlayerHeat>().HeatTransfer(otherHeat);
            }

            // Health Change if collide villains
            float damage = collideObj.GetComponent<VillainController>().damage; // TODO
            GetComponent<PlayerHealth>().Damage(damage);

            isOnPlane = true;
        }

        
        if (coll.IsTouchingLayers(planeLayer) && collideObj)
        {
            // Heat Change if collide platforms
            float otherHeat = collideObj.GetComponent<PlaneHeat>().curHeat;
            if (otherHeat != GetComponent<PlayerHeat>().curHeat)
            {
                GetComponent<PlayerHeat>().HeatTransfer(otherHeat);
            }

            // Record the last landing place if collide platform
            lastPlanePosition = collideObj.transform.position;
            lastPlanePosition.y += 0.5f;

            isOnPlane = true;
        }
        
    }

    // Method 5. Back to the last collision plane
    void BackToPlane()
    {
        transform.position = lastPlanePosition;
        rigBody.velocity = Vector2.zero;
    }
}
