using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{
    // Heat Bar
    public float curHeat { get; set; }
    public float lowerBoundHeat { get; private set; }
    public float upperBoundHeat { get; private set; }
    public Slider heatBar;
    // Layers
    public LayerMask villainLayer;
    public LayerMask planeLayer;
    // Heat Bar Color Change
    public Gradient gradient;
    public Image fill;
    // Player Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient renderGradient;

    // Start is called before the first frame update
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        ColorLerp(curHeat, upperBoundHeat, lowerBoundHeat);
    }

    // Method 0. Set Bound Heat
    public void SetBoundHeat(float upperBoundHeat, float lowerBoundHeat)
    {
        // Heat value
        this.upperBoundHeat = upperBoundHeat;
        this.lowerBoundHeat = lowerBoundHeat;
        curHeat = (upperBoundHeat - lowerBoundHeat) / 2;
        // Heat Bar
        heatBar.value = curHeat;
        heatBar.maxValue = upperBoundHeat;
        // Set Color
        fill.color = gradient.Evaluate(1f);
    }

    // Method 1. Set Current Heat
    public void SetCurHeat(float heat)
    {
        heatBar.value = heat;
        fill.color = gradient.Evaluate(heatBar.normalizedValue);
        ColorLerp(heat, upperBoundHeat, lowerBoundHeat);
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

    // Method 4. Color Change
    public void ColorLerp(float curHeat, float upperBoundHeat, float lowerBoundHeat)
    {
        render.color = renderGradient.Evaluate(curHeat - lowerBoundHeat / upperBoundHeat - lowerBoundHeat);
    }
}
