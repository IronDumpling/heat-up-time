using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : GraffitiController
{
    private Rigidbody2D rigBody;
    private float jumpForce;
    public float detectionDelay;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Redefine
        speed = 4.5f; // Fast
        maxHealth = 20;
        upperDamageHeatBound = 0.9f;
        // Detection coroutine started
        radius = 5.5f;
        detectionDelay = 1.5f;
        StartCoroutine(DetectionCoroutine());
        // Jump variables
        jumpForce = 9f; // Higher than player's jump force
        rigBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Method 1.
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    // Method 2. Move Around
    public override void Move()
    {
        // Catch player
        if (target)
        {
            // Jump to the player
            if (notJumped && target.transform.position.y > transform.position.y)
            {
                rigBody.velocity = new Vector2(rigBody.velocity.x, jumpForce);
                notJumped = false;
            }
            else if(transform.position.y == moveRanges[0].y && target.transform.position.y < transform.position.y)
            {
                Vector3 tempPosition = new Vector3(target.transform.position.x, transform.position.y, 0);
                transform.position = Vector2.MoveTowards(transform.position, tempPosition, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    target.transform.position, speed * Time.deltaTime);
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
