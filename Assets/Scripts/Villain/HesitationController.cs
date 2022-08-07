using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HesitationController : GraffitiController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Redefine
        speed = 0.3f; // Slow
        damage = 0.5f; // Mid
        maxHealth = 60;
        heatDamageBound = 0.9f;
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    // Method 2. Follower A* Moving AI
    public override void Move()
    {

    }
}
