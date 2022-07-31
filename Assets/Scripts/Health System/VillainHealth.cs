using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainHealth : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;
    private Collider2D coll;
    public LayerMask bulletLayer;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease Health by Bullet
        if (coll.IsTouchingLayers(bulletLayer))
        {
            Damage(1);
            if (curHealth <= 0)
            {
                Die();
            }
        }

        // Decrease Health by Heating
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
}
