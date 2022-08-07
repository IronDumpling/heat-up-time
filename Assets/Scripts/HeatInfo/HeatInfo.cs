using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatInfo : MonoBehaviour
{
    // Heat Bar
    [SerializeField]
    public float curHeat; /*{ get; set; }*/
    public float minHeat; /*{ get; private set; }*/
    public float maxHeat; /*{ get; private set; }*/
    public Gradient renderGradient;

    protected bool isHeatBalance = true;

    

    // Set Player Color
    public Color CalculateColor() {
        float heatCoeff = HeatOp.HeatCoeff(curHeat, maxHeat, minHeat);
        return HeatOp.ColorLerp(renderGradient, heatCoeff);
    }

}
