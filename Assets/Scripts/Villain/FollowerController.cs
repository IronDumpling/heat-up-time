using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : GraffitiController
{
    private Rigidbody2D rigBody;
    private float jumpForce;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Redefine
        speed = 5f; // Fast
        radius = 15f;
        maxHealth = 20;
        upperDamageHeatBound = 0.9f;
        // Jump variables
        jumpForce = 9f; // Higher than player's jump force
        rigBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Method 8. Move Around
    public override void Move()
    {
        if (playerTransform != null)
        {
            distance = (transform.position - playerTransform.position).sqrMagnitude;

            // Catch player
            if (distance < radius)
            {
                // Jump to the player
                if (notJumped && playerTransform.position.y > transform.position.y)
                {
                    rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
                    notJumped = false;
                }
                else if(transform.position.y == moveRanges[0].y && playerTransform.position.y < transform.position.y)
                {
                    Vector3 tempPosition = new Vector3(playerTransform.position.x, transform.position.y, 0);
                    transform.position = Vector2.MoveTowards(transform.position, tempPosition, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        playerTransform.position, speed * Time.deltaTime);
                }
            }
            // Move on the plane 
            else if (moveRanges != null)
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
        }
    }
}
