using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartVillainController : VillainController
{
    public float speed;
    public float radius;
    private float distance;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Health 
        curHealth = maxHealth;
        // Heat
        heatingDamage = maxHealth / 20;
        damageBound = 1f;
        // Falling
        lowerBound = -20;
        // First Lerp
        ColorLerp(curHeat, boundHeat);
        // Player's position
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            distance = (transform.position - playerTransform.position).sqrMagnitude;

            if(distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                                                         playerTransform.position,
                                                         speed * Time.deltaTime);
            }
        }
    }
}
