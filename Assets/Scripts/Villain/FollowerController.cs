using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : GraffitiController
{
    public float speed;
    public float radius;
    private float distance;
    private Transform playerTransform;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damageBound = 1f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (playerTransform != null)
        {
            distance = (transform.position - playerTransform.position).sqrMagnitude;

            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                                                         playerTransform.position,
                                                         speed * Time.deltaTime);
            }
        }
    }
}
