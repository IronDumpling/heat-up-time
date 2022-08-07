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

    // Method 11. Move Range on the Platform
    //protected override void GetMoveRange(GameObject obj)
    //{
    //    //Vector3 position = obj.GetComponent<Transform>().position;

    //    //moveRanges[0] = new Vector3(position.x, position.y, 0);
    //    //moveRanges[1] = new Vector3(position.x, position.y, 0);
    //    //moveIndex = 0;
    //}

    // Method 4. Avoid kicking the player
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;
        collideObjs.Add(collideObj);

        // 1.1 Touch Bullet
        if (collideObj.layer == BULLETS)
        {
            float otherHeat = collideObj.GetComponent<BulletController>().bulletHeat;
            HeatGain(otherHeat);
        }

        // 1.2 Touhch Platforms
        else if (collideObj.layer == PLATFORMS && collideObj.tag != "SafePlane")
        {
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.3 Touhch Villains
        else if (collideObj.layer == VILLAINS)
        {
            float otherHeat = collideObj.GetComponent<GraffitiController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.4 Touch Player
        else if (collideObj.layer == PLAYER)
        {
            float otherHeat = collideObj.GetComponent<HeatInfo>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }

            // Collide player and move back
            collideObj.GetComponent<PlayerController>().CollideRecoil(this.gameObject, damage * 7);
        }

        // Change color of planes and villains
        ColorLerp(curHeat);
    }
}
