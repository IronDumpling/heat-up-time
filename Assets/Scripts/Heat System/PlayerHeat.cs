using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{
    // Health Bar
    public float curHeat;
    public float boundHeat;
    public Slider heatBar;
    // Component pointers
    //private Collider2D coll;
    //private GameObject collideObj;
    private GameObject bulletType;
    // Layers
    public LayerMask villainLayer;
    public LayerMask planeLayer;
    // Change Color
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        SetBoundHeat(100);
       
        // Object Pointers
        bulletType = GetComponent<ShootBullets>().bulletType;
    }

    // Method 0. Set Bound Heat
    public void SetBoundHeat(float heat)
    {
        // Heat value
        boundHeat = heat;
        curHeat = boundHeat / 2;
        // Heat Bar
        heatBar.value = curHeat;
        heatBar.maxValue = boundHeat;
        // Set Color
        fill.color = gradient.Evaluate(1f);
    }

    // Method 1. Set Current Heat
    public void SetCurHeat(float heat)
    {
        heatBar.value = heat;
        fill.color = gradient.Evaluate(heatBar.normalizedValue);
    }

    // Method 2. Heat Change
    public void HeatTransfer(float otherHeat)
    {
        curHeat = (curHeat + otherHeat) / 2;
        SetCurHeat(curHeat);
    }

    // Method 3. Shoot Bullet
    public void ShootHeat(int bulletHeat)
    {
        curHeat -= bulletHeat;
        SetCurHeat(curHeat);
    }
}
