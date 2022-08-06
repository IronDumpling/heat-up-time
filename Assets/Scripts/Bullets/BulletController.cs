using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Collider2D coll;
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    public float bulletHeat;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // FixedUpdate is called once per physics frame
    //void FixedUpdate()
    //{
    //    Collide();
    //}

    // Method 1. Destroy out of bound
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlaneController pC = collision.gameObject.GetComponent<PlaneController>();
        GraffitiController gC = collision.gameObject.GetComponent<GraffitiController>();

        if (pC != null) {
            HeatOp.HeatTransfer(ref pC.curHeat, bulletHeat);
        }
        if (gC != null) {
            HeatOp.HeatTransfer(ref gC.curHeat, bulletHeat);
        }
        Destroy(gameObject);
    }
}
