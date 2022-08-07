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

    // Method 1. Destroy out of bound
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Platform: Heat Transfer, no dmg
        //Enemy: Heat Transfer, dmg applied

        HeatInfo hI = collision.gameObject.GetComponent<HeatInfo>();
        GraffitiController gC = collision.gameObject.GetComponent<GraffitiController>();

        if (hI != null) {
            Debug.Log(hI);

            HeatOp.HeatTransfer(ref hI.curHeat, bulletHeat);
        }
        if (gC != null) {
            Debug.Log(gC);


            gC.Damage(damage);
        }
        Destroy(gameObject);
    }
}
