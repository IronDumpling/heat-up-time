using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainController : MonoBehaviour
{
    // Damage
    public float damage; 

    // Component pointers
    private GameObject collideObj;
    private Collider2D coll;

    // Layers
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    public LayerMask playerLayer;
    public LayerMask bulletLayer;

    // Health System
    public float curHealth;
    public float maxHealth;

    // Heating System
    public float heatingDamage;
    public float damageBound;
    public float curHeat;
    public float boundHeat;

    // Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient gradient;

    // Falling
    public int lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        // Health 
        curHealth = maxHealth;
        // Pointer
        coll = GetComponent<Collider2D>();
        render = GetComponent<SpriteRenderer>();
        // Heat
        heatingDamage = maxHealth/20;
        damageBound = 0.9f;
        // Falling
        lowerBound = -20;
        // First Lerp
        ColorLerp(curHeat, boundHeat);
    }

    // Update per frame
    private void Update()
    {
        // Heat Health Damage
        if (curHeat >= boundHeat * damageBound)
        {
            ContinousDamage(heatingDamage);
        }

        // Die Conditions
        if (transform.position.y < lowerBound)
        {
            Die();
        }
    }

    // Method 1. Collision
    private void OnCollisionEnter2D(Collision2D collision)
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
        else if (collideObj.layer == 9 && collideObj.tag != "SafePlane") // 9 is layer of platforms
        {
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.3 Touhch Villains
        else if (collideObj.layer == 7) // 7 is layer of villains
        {
            float otherHeat = collideObj.GetComponent<VillainController>().curHeat;
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
}
