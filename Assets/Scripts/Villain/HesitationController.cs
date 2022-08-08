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
}
