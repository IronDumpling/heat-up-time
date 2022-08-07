using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HesitationController : GraffitiController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Redefine
        speed = 0.5f; // Slow
        damage = 0.5f; // Low
        SetMaxHealth(70);
        heatDamageBound = 0.5f;
        heatingDamage = maxHealth / 5;
        GetMoveRange(gameObject);
    }

    // Method 2. Follower A* Moving AI
    public override void Move()
    {         
        transform.position = Vector2.MoveTowards(transform.position,
                                                    moveRanges[moveIndex],
                                                    speed * Time.deltaTime);
        if (transform.position.x == moveRanges[moveIndex].x)
        {
            if (moveIndex == 1)
            {
                moveIndex--;
            }
            else
            {
                moveIndex++;
            }
        }
    }

    // Method 1. Move Range on the Platform
    protected override void GetMoveRange(GameObject obj)
    {
        Vector3 position = obj.GetComponent<Transform>().position;

        moveRanges[0] = new Vector3(position.x - 5f, position.y, 0);
        moveRanges[1] = new Vector3(position.x + 5f, position.y, 0);
        moveIndex = 0;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;
        collideObjs.Add(collideObj);

        // 1.2 Touch Platforms
        if (collideObj.layer == PLATFORMS && collideObj.tag != "SafePlane")
        {
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            notJumped = true;
            if (otherHeat != curHeat)
            {
                HeatOp.HeatBalance(ref curHeat, ref otherHeat, 0.5f);
            }
        }

        // 1.3 Touch Villains
        else if (collideObj.layer == VILLAINS) // 7 is layer of villains
        {
            float otherHeat = collideObj.GetComponent<GraffitiController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatOp.HeatBalance(ref curHeat, ref otherHeat, 0.5f);
            }
        }

        // 1.4 Touch Player
        else if (collideObj.layer == PLAYER) // 3 is layer of player
        {
            Debug.Log("In");
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatOp.HeatBalance(ref curHeat, ref otherHeat, 0.5f);
            }

            // Collide player and move back
            collideObj.GetComponent<PlayerController>().CollideRecoil(this.gameObject, damage * 5);
        }

        // Change color of planes and villains
        ColorLerp(curHeat);
    }

    // Method 2.
    public override void Damage(float decreaseValue)
    {
        
    }
}
