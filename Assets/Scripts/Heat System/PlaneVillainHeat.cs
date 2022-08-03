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
    // Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        // Pointers
        coll = GetComponent<Collider2D>();
        render = GetComponent<SpriteRenderer>();

        // First Lerp
        ColorLerp(curHeat, boundHeat);
    }

    // Method 1. Get colliding object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;

        if (coll.IsTouchingLayers(playerLayer))
        {
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat; // TODO
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
        ColorLerp(curHeat, boundHeat);
    }

    // Method 2. Heat Transfer
    void HeatTransfer(float otherHeat)
    {
        curHeat = (curHeat + otherHeat) / 2;
    }

    // Method 3. Heat Gain
    void HeatGain(float otherHeat)
    {
        curHeat += otherHeat;
    }

    // Method 4. Color Change
    public void ColorLerp(float curHeat, float boundHeat)
    {
        render.color = gradient.Evaluate(curHeat / boundHeat);
    }
}
