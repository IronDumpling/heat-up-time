using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{

    public Image img;
    public HeatInfo heatInfo;
    private TimeScaleEditor TSE;

    public bool disableBalance = false;

    void Awake(){
        heatInfo = GetComponent<HeatInfo>();
        TSE = GameObject.FindObjectOfType<TimeScaleEditor>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        SetPlayerColor();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (disableBalance) return;

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

    public void forceMaxHeat() {
        heatInfo.curHeat = heatInfo.maxHeat;
        SetPlayerColor();
    }
    public void forceMinHeat() {
        heatInfo.curHeat = heatInfo.minHeat;
        SetPlayerColor();
    }
    public void SetPlayerColor() {
        TSE.UpdateTimescaleByPlayerHeat(heatInfo);
        heatInfo.DebugLogInfo("setPly");
        img.color = heatInfo.CalculateColor();
    }
}