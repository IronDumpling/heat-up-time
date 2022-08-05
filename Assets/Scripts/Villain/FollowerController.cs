using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : GraffitiController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed = 2f;
        radius = 15f;
        maxHealth = 20;
        upperHeatBound = 0.9f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Method 8. Move Around
    //protected override void Move()
    //{
    //    if (playerTransform != null)
    //    {
    //        distance = (transform.position - playerTransform.position).sqrMagnitude;

    //        // Catch player
    //        if (distance < radius)
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position,
    //                                                     playerTransform.position,
    //                                                     speed * Time.deltaTime);
    //        }
    //        // Move on the plane 
    //        else if (moveRanges != null)
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position,
    //                                                     moveRanges[moveIndex],
    //                                                     speed * Time.deltaTime);
    //            if (transform.position.x == moveRanges[moveIndex].x)
    //            {
    //                if (moveIndex == 1)
    //                {
    //                    moveIndex--;
    //                }
    //                else
    //                {
    //                    moveIndex++;
    //                }
    //            }
    //        }
    //    }
    //}
}
