using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
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

        // 1.1 Touch Bullets
        if (collideObj.layer == 8) // 8 is layer of bullets
        {
            float otherHeat = collideObj.GetComponent<BulletController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatGain(otherHeat);
            }
        }

        // 1.2 Touhch Platforms
        else if (collideObj.layer == 9) // 9 is layer of platforms
        {
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.3 Touhch Villains
        else if (collideObj.layer == 7) // 7 is layer of villains
        {
            float otherHeat = collideObj.GetComponent<VillainController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.4 Touch Player
        else if (collideObj.layer == 3) // 3 is layer of player
        {
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat;
            if (otherHeat != curHeat)
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

