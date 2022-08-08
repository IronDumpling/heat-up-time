using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FollowerController : GraffitiController
{
    private Rigidbody2D rigBody;
    private float jumpForce;
    public float detectionDelay;

    // Path FInder with A*
    protected float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    // Scale Flip and enlarge
    private Vector3 normalScale = new Vector3(0.5f, 0.5f, 1f);
    private Vector3 largeScale = new Vector3(1f, 1f, 1f);

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Redefine
        speed = 4.5f; // Fast
        damage = 1.5f; // Mid
        SetMaxHealth(40);
        // Detection coroutine started
        radius = 7f;
        detectionDelay = 0.3f;
        StartCoroutine(DetectionCoroutine());
        // Jump variables
        jumpForce = 20f; // Higher than player's jump force
        rigBody = GetComponent<Rigidbody2D>();
        // Seeker Pointer
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Re-Start path generating per 0.5s 
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rigBody.position, GameObject.FindGameObjectWithTag("Player").transform.position, OnPathComplete);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Method 1. Detecting the player
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    // Method 2. Follower A* Moving AI
    public override void Move()
    {
        // Catch player
        if (target)
        {
            // Check Path Ending or Not 
            if (path == null) return;
            if (currentWaypoint >= path.vectorPath.Count) return;

            if (notJumped && target.transform.position.y > transform.position.y)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigBody.position).normalized;
                Vector2 force = direction * jumpForce;
                rigBody.AddForce(force);
                notJumped = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);
            }

            // Move to the next Way Point
            float distance = Vector2.Distance(rigBody.position, path.vectorPath[currentWaypoint]);
            if(distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            // Flip the renderer with larger scale
            transform.localScale = largeScale;

        }
        // Move on the plane slower 
        else if (moveRanges != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                        moveRanges[moveIndex],
                                                        speed/2 * Time.deltaTime);
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

            // Flip the renderer with normal scale
            transform.localScale = normalScale;
        }
    }

    // // Method 3. Avoid kicking the player
    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     collideObj = collision.gameObject;

    //     if (collideObj.layer == 3) // 3 is layer of player
    //     {
    //         float otherHeat = collideObj.GetComponent<HeatInfo>().curHeat;
    //         if (otherHeat != curHeat)
    //         {
    //             HeatTransfer(otherHeat);
    //         }
    //         // Collide player and move back
    //         collideObj.GetComponent<PlayerController>().CollideRecoil(this.gameObject, damage * 7);

    //     }
    // }

    // Method 4. If path completed
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
