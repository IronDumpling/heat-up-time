using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Collider2D coll;
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    public int curHeat;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Collide();
    }

    // Method 1. Destroy out of bound
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Method 2. Destroy if collision
    void Collide()
    {
        if (coll.IsTouchingLayers(planeLayer) ||
            coll.IsTouchingLayers(villainLayer))
        {
            Destroy(this.gameObject);
        }
    }
}
