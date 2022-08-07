using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{

    public Image img;
    public HeatInfo heatInfo;

    void Awake(){
        heatInfo = GetComponent<HeatInfo>();
    }
    
    public void InitalizePlayerHeat (float upperBoundHeat, float lowerBoundHeat, float transferSpeed)
    {
        // Heat value
        heatInfo.maxHeat = upperBoundHeat;
        heatInfo.minHeat = lowerBoundHeat;
        heatInfo.curHeat = (upperBoundHeat - lowerBoundHeat) / 2 + lowerBoundHeat;
        heatInfo.heatTransferSpeed = transferSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        HeatInfo hI = collision.gameObject.GetComponent<HeatInfo>();
        if (!hI)return;
        bool isBalanced = HeatOp.HeatBalance(ref heatInfo.curHeat, ref hI.curHeat, heatInfo.heatTransferSpeed);
        
        if (!isBalanced){
            heatInfo.DebugLogInfo("PlyBalance");
            SetPlayerColor();
        }
        
        //trigger the target function to change the color, maybe not :)
    }

    // Shoot Bullet
    public void ShootHeat(float bulletHeat)
    {
        heatInfo.curHeat -= bulletHeat;
        heatInfo.DebugLogInfo("shot");
        SetPlayerColor();
    }

    public void SetPlayerColor() {
        heatInfo.DebugLogInfo("setPly");
        img.color = heatInfo.CalculateColor();
    }
}