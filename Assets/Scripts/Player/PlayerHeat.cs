using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{
    // Heat Bar
    public float curHeat;
    public float boundHeat;
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
        ColorLerp(curHeat, boundHeat);
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
        ColorLerp(heat, boundHeat);
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
    public void ColorLerp(float curHeat, float boundHeat)
    {
        render.color = renderGradient.Evaluate(curHeat / boundHeat);
    }
}
