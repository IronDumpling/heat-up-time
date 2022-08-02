using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainController : MonoBehaviour
{
    // Damage
    public float damage;

    // Pointers
    private Collider2D coll;
    public LayerMask bulletLayer;

    // Health System
    public float curHealth;
    public float maxHealth;

    // Heating System
    private float heatingDamage;
    public float damageBound;

    // Falling
    private int lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        // Health 
        curHealth = maxHealth;
        // Pointer
        coll = GetComponent<Collider2D>();
        // Heat
        heatingDamage = maxHealth / 20;
        damageBound = 0.9f;
        // Falling
        lowerBound = -20;
    }

    private void Update()
    {
        // Heat Health Damage
        if (GetComponent<PlaneVillainHeat>().curHeat >= GetComponent<PlaneVillainHeat>().boundHeat * damageBound)
        {
            ContinousDamage(heatingDamage);
        }

        if (transform.position.y < lowerBound)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Decrease Health by Bullet
        if (coll.IsTouchingLayers(bulletLayer))
        {
            // Direct Health Damage
            float bulletDamage = collision.gameObject.GetComponent<BulletController>().damage;
            Damage(bulletDamage);

            if (curHealth <= 0)
            {
                Die();
            }
        }
    }

    // Method 1. Damage Health
    void Damage(float decreaseValue)
    {
        curHealth -= decreaseValue;
    }

    // Method 2. Die
    void Die()
    {
        Destroy(this.gameObject);
    }

    // Method 3. Damage Health Continously
    void ContinousDamage(float decreaseValue)
    {
        curHealth -= decreaseValue * Time.deltaTime;
    }
}
