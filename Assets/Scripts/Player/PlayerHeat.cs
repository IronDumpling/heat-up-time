using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : HeatInfo
{
    public void InitalizePlayerHeat (float upperBoundHeat, float lowerBoundHeat)
    {
        // Heat value
        this.maxHeat = upperBoundHeat;
        this.minHeat = lowerBoundHeat;
        curHeat = (upperBoundHeat - lowerBoundHeat) / 2 + lowerBoundHeat;
    }

    
    private void OnCollisionEnter2D(Collision2D collision){
        isHeatBalance = false;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!isHeatBalance) {
            GameObject target = collision.gameObject;
            // HeatOp.HeatBalance(curHeat, )
        }
    }


    //// Method 2. Heat Change
    //public void HeatTransfer(float otherHeat)
    //{
    //    float endHeat = (curHeat + otherHeat) / 2;
    //    curHeat += (endHeat - curHeat) * Time.deltaTime;
    //    SetPlayerColor();
    //}

    // Shoot Bullet
    public void ShootHeat(float bulletHeat)
    {
        curHeat -= bulletHeat;
        SetPlayerColor();
    }

}