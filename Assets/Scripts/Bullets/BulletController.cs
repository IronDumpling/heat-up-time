using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletHeat;
    public float damage;

    public SpecialSkill SPS;

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
            //Debug.Log(hI);

            HeatOp.HeatTransfer(ref hI.curHeat, bulletHeat);
        }
        if (gC != null) {
            SPS.SPadd();
            gC.Damage(damage);
        }
        Destroy(gameObject);
    }
}
