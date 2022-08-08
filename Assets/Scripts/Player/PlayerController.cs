using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const int PLAYER = 3;
    public const int VILLAINS = 7;
    public const int BULLETS = 8;
    public const int PLATFORMS = 9;
    // Components pointer
    private Rigidbody2D rigBody;
    public Vector2 GetrigBodyVeloc() { return rigBody.velocity; }
    public void SetrigBodyVeloc(Vector2 veloc) { rigBody.velocity = veloc; }

    [Header("Collision Attribute")]
    public LayerMask planeLayer;
    public LayerMask villainLayer;

    [Header("Movement Attribute")]
    public float velocity;
    public float xInput;
    public float jumpForce;
    public float bulletVelocity;

    // Flags
    private int jumpCount = 2;
    private bool pressJump = false;
    public bool isGrounded { get; private set; }

    private Transform PlayerGnd;
    // Falling Variables
    private int lowerBound;
    private float fallingDamage;
    private Vector3 lastPlanePosition;

    // detect if Player in Battle
    [SerializeField] public int inBattle{get; set;}

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
        jumpForce = 8f;
        jumpCount = 2;
        PlayerGnd = getChildGameObject(this.gameObject, "PlayerGnd").transform;
    }

    // Update is called once per frame
    void Update()
    {
        TriggerJump();
        changeOrientation();
        fallOutOfWorld();
    }


    void TriggerJump() {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            pressJump = true;
        }
    }
    //adjust character avatar facing 
    void changeOrientation() {
        // Flip the renderer
        if (rigBody.velocity.x >= 0.001f) {
            transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
        else if (rigBody.velocity.x <= -0.001f) {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1f);
        }
    }
    // Back to the last collision plane
    void BackToPlane()
    {
        transform.position = lastPlanePosition;
        rigBody.velocity = Vector2.zero;
    }
    //check if player fall out of mapid
    void fallOutOfWorld() {
        // Decrease Health by Falling
        if (transform.position.y < lowerBound) {
            GetComponent<PlayerHealth>().Damage(fallingDamage);

            if (GetComponent<PlayerHealth>().curHealth > 0) {
                BackToPlane();
            }
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
        if (Physics2D.OverlapCircle(PlayerGnd.position, 0.1f, planeLayer.value)) {
            jumpCount = 2;
            isGrounded = true;
        }
        if (Physics2D.OverlapCircle(PlayerGnd.position, 0.1f, villainLayer.value)) {
            jumpCount = 1;
            isGrounded = false;
        }
        else isGrounded = false;
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

    [Header("Enemy Collide with Player")]
    public float recoilCoef = 5f;
    public float EnemyColliDmg = 1f;

    // Method 7. Recoil if collide villains
    public void Hurt(GameObject obj)
    {
        this.GetComponent<PlayerHealth>().Damage(EnemyColliDmg);

        if (obj.transform.position.x <= transform.position.x && obj.transform.position.y < transform.position.y)
        {
            rigBody.velocity = new Vector2(recoilCoef, rigBody.velocity.y);
        }
        else if (obj.transform.position.x > transform.position.x && obj.transform.position.y < transform.position.y)
        {
            rigBody.velocity = new Vector2(-recoilCoef, rigBody.velocity.y);
        }
    }

    ////////////////////// Helper Functions STARTING////////////////////////////
    static public GameObject getChildGameObject(GameObject fromGameObject, string withName) {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }


    
}

