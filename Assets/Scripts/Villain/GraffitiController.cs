using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiController : MonoBehaviour
{
    // Component pointers
    private GameObject collideObj;
    private Transform playerTransform;

    // Health System
    public float curHealth;
    public float maxHealth;

    // Heating System
    public float heatingDamage;
    public float upperHeatBound;
    public float lowerHeatBound;
    public float curHeat;
    public float boundHeat;

    // Attack
    public float damage;
    public float speed;
    public float radius;
    private float distance;

    // Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient gradient;

    // Falling
    public int fallingBound;

    // Moving
    private Vector3[] moveRanges = new Vector3[2];
    private int moveIndex = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Health 
        maxHealth = 10;
        curHealth = maxHealth;
        // Pointer
        render = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Heat
        heatingDamage = maxHealth/20;
        upperHeatBound = 0.8f;
        lowerHeatBound = 0.2f;
        // Falling
        fallingBound = -20;
        // Attack
        speed = 1f;
        radius = 5f;
        damage = 1;
        // First Lerp
        ColorLerp(curHeat, boundHeat);
    }

    // Update per frame
    protected virtual void Update()
    {
        // Heat/Cooling Health Damage
        if (curHeat >= boundHeat * upperHeatBound
            && curHeat <= boundHeat * lowerHeatBound)
        {
            ContinousDamage(heatingDamage);
        }

        Move();

        // Die Conditions
        if (transform.position.y < fallingBound)
        {
            Die();
        }
    }

    // Method 1. Collision
    public void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;

        // 1.1 Touch Bullet
        if (collideObj.layer == 8) // 8 is layer of bullets
        {
            float otherHeat = collideObj.GetComponent<BulletController>().curHeat;
            float bulletDamage = collideObj.GetComponent<BulletController>().damage;

            if (otherHeat != curHeat)
            {
                HeatGain(otherHeat);
            }

            // Direct Health Damage
            Damage(bulletDamage);
        }

        // 1.2 Touhch Platforms
        else if (collideObj.layer == 9) // 9 is layer of platforms
        {
            GetMoveRange(collideObj);
            if (collideObj.tag != "SafePlane")
            {
                float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
                if (otherHeat != curHeat)
                {
                    HeatTransfer(otherHeat);
                }
            }
        }

        // 1.3 Touhch Villains
        else if (collideObj.layer == 7) // 7 is layer of villains
        {
            float otherHeat = collideObj.GetComponent<GraffitiController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.4 Touch Player
        else if (collideObj.layer == 3) // 3 is layer of player
        {
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // Change color of planes and villains
        ColorLerp(curHeat, boundHeat);
    }

    // Method 2. Damage Health
    void Damage(float decreaseValue)
    {
        curHealth -= decreaseValue;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    // Method 3. Damage Health Continously
    void ContinousDamage(float decreaseValue)
    {
        curHealth -= decreaseValue * Time.deltaTime;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    // Method 4. Die
    void Die()
    {
        Destroy(this.gameObject);
    }

    // Method 5. Heat Transfer
    void HeatTransfer(float otherHeat)
    {
        curHeat = (curHeat + otherHeat) / 2;
    }

    // Method 6. Heat Gain
    void HeatGain(float otherHeat)
    {
        curHeat += otherHeat;
    }

    // Method 7. Color Change
    public void ColorLerp(float curHeat, float boundHeat)
    {
        render.color = gradient.Evaluate(curHeat / boundHeat);
    }

    // Method 8. Move Around
    protected virtual void Move()
    {
        if (playerTransform != null)
        {
            distance = (transform.position - playerTransform.position).sqrMagnitude;

            // Catch player
            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                                                         playerTransform.position,
                                                         speed * Time.deltaTime);
            }
            // Move on the plane 
            else if(moveRanges != null)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                                                         moveRanges[moveIndex],
                                                         speed * Time.deltaTime);
                if(transform.position.x == moveRanges[moveIndex].x)
                {
                    if(moveIndex == 1)
                    {
                        moveIndex--;
                    }
                    else
                    {
                        moveIndex++;
                    }
                }
            }
        }
    }

    // Method 9. Move Range
    public void GetMoveRange(GameObject obj)
    {
        Vector3 position = obj.GetComponent<Transform>().position;
        float width = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = obj.GetComponent<SpriteRenderer>().bounds.size.y + GetComponent<SpriteRenderer>().bounds.size.y;


        moveRanges[0] = new Vector3(position.x - width/2 + 0.2f, position.y + height/2, 0);
        moveRanges[1] = new Vector3(position.x + width/2 - 0.2f, position.y + height/2, 0);

        Debug.Log(moveRanges[0]);
        Debug.Log(moveRanges[1]);

        moveIndex = 0;
    }
}
