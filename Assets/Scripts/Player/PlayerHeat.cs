using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{
    // Heat Bar
    [SerializeField]
    public float curHeat; /*{ get; set; }*/
    public float minHeat; /*{ get; private set; }*/
    public float maxHeat; /*{ get; private set; }*/
    public Slider heatBar;
    // Layers
    public LayerMask villainLayer;
    public LayerMask planeLayer;
    // Heat Bar Color Change
    public Gradient gradient;
    public Image fill;
    // Player Color Change
    [SerializeField] 
    private SpriteRenderer render;
    public Gradient renderGradient;

    // Start is called before the first frame update
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        SetPlayerColor();
    }

    public void InitalizePlayerHeat(float upperBoundHeat, float lowerBoundHeat)
    {
        // Heat value
        this.maxHeat = upperBoundHeat;
        this.minHeat = lowerBoundHeat;
        curHeat = (upperBoundHeat - lowerBoundHeat) / 2 + lowerBoundHeat;
    }

    // Method 2. Heat Change
    public void HeatTransfer(float otherHeat)
    {
        float endHeat = (curHeat + otherHeat) / 2;
        curHeat += (endHeat - curHeat) * Time.deltaTime;
        SetPlayerColor();
    }

    // Method 3. Shoot Bullet
    public void ShootHeat(float bulletHeat)
    {
        curHeat -= bulletHeat;
        SetPlayerColor();
    }

    // Method 1. Set Player Color
    public void SetPlayerColor() {
        float heatCoeff = HeatOp.HeatCoeff(curHeat, maxHeat, minHeat);
        HeatOp.ColorLerp(ref render, renderGradient, heatCoeff);
    }
}
