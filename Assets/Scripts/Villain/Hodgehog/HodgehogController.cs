using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HodgehogController : GraffitiController
{
    // Bullet Types Pointers
    public GameObject bulletType;
    // Speed
    public float bulletVelocity;
    private Vector3 offset;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetMaxHealth(120);
        // Attack
        speed = 0f; // Frozen
        damage = 1;
        // Detect Player
        radius = 15f;
    }

    // Update per frame
    protected override void Update()
    {
        // Heat/Cooling Health Damage
        if (curHeat >= upperHeatBound * heatDamageBound) // 150 * 0.8 ~ 150
        {
            ContinousDamage(heatingDamage);
        }
    }

    // Method 1. Get Damage
    public override void Damage(float decreaseValue)
    {
        curHealth -= 3 * decreaseValue;
        healthBar.value = curHealth;
        fill.color = healthBarGradient.Evaluate(healthBar.normalizedValue);
        myHealthBar.enabled = true;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    // Method 2. Attack player
    protected void Attack(Vector3 shootDirection)
    {
        if (target)
        {
            // Generate bullet
            Vector3 bulletPosition = new Vector3(transform.position.x + shootDirection.x + offset.x,
                transform.position.y + shootDirection.y + offset.y, 0);
            GameObject bullet = Instantiate(bulletType, bulletPosition, Quaternion.Euler(Vector3.zero));

            // Shoot to the mouse direction
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bulletVelocity,
                                                                      shootDirection.y * bulletVelocity);

            PlayerHeat pH = this.GetComponent<PlayerHeat>();
            BulletController bH = bullet.GetComponent<BulletController>();
            bH.bulletHeat = pH.curHeat /20;
            pH.ShootHeat(bH.bulletHeat);
        }
    }
}
