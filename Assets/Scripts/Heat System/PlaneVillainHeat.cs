using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneVillainHeat : MonoBehaviour
{
    // Component pointers
    private Collider2D coll;
    private GameObject collideObj;
    // Heat Value
    public float curHeat;
    public float boundHeat;
    // Layers
    public LayerMask bulletLayer;
    public LayerMask planeLayer;
    public LayerMask villainLayer;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();

        curHeat = Random.Range(0f, 100f);
    }

    // Method 1. Get colliding object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;

        if (coll.IsTouchingLayers(playerLayer))
        {
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        else if (coll.IsTouchingLayers(bulletLayer))
        {
            float otherHeat = collideObj.GetComponent<BulletController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatGain(otherHeat);
            }
        }

        else if (coll.IsTouchingLayers(planeLayer) ||
            coll.IsTouchingLayers(villainLayer))
        {
            float otherHeat = collideObj.GetComponent<PlaneVillainHeat>().curHeat;
            {
                HeatTransfer(otherHeat);
            }
        }

        // Change color of planes and villains
        GetComponent<ColorController>().colorLerp();
    }

    // Method 2. Heat Transfer
    void HeatTransfer(float otherHeat)
    {
        curHeat = (curHeat + otherHeat) / 2;
    }

    // Method 2. Heat Gain
    void HeatGain(float otherHeat)
    {
        curHeat += otherHeat;
    }
}
