using UnityEngine;

public class HeatInfo : MonoBehaviour
{
    bool HEATDEBUG = true;

    public Gradient gradient;

    // Heat Bar
    [HideInInspector] public float curHeat; /*{ get; set; }*/
    [HideInInspector] public float minHeat; /*{ get; private set; }*/
    [HideInInspector] public float maxHeat; /*{ get; private set; }*/
    [HideInInspector] public float heatTransferSpeed = 1f;


    // Set Color
    public Color CalculateColor() {
        float heatCoeff = HeatOp.HeatCoeff(curHeat, maxHeat, minHeat);
        return HeatOp.ColorLerp(gradient, heatCoeff);
    }

    public void DebugLogInfo(string position) {
        if(HEATDEBUG)
            Debug.Log(position + ": CurHeat: " + curHeat + " MinHeat: " + minHeat + " MaxHeat: " + maxHeat);
    }

    public void setVal(float curHeat, float minHeat, float maxHeat, float speed){
        this.curHeat = curHeat;
        this.maxHeat = maxHeat;
        this.minHeat = minHeat;
        this.heatTransferSpeed = speed;
    }
}
