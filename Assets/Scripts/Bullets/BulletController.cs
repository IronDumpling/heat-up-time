using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    public float bulletHeat;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Method 1. Destroy out of bound
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //Platform: Heat Transfer, no dmg
        //Enemy: Heat Transfer, dmg applied

        PlaneController pC = collision.gameObject.GetComponent<PlaneController>();
        GraffitiController gC = collision.gameObject.GetComponent<GraffitiController>();

        if (pC != null) {
            HeatOp.HeatTransfer(ref pC.curHeat, bulletHeat);
        }
        if (gC != null) {
            HeatOp.HeatTransfer(ref gC.curHeat, bulletHeat);
            gC.Damage(damage);
        }
        Destroy(gameObject);
    }
}
