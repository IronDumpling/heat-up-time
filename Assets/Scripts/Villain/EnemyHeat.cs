using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeat : MonoBehaviour
{
    public SpriteRenderer render;
    public HeatInfo heatInfo;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
        heatInfo = GetComponent<HeatInfo>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        SetEnemyColor();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        HeatInfo hI = collision.gameObject.GetComponent<HeatInfo>();
        if (!hI) return;
        bool isBalanced = HeatOp.HeatBalance(ref heatInfo.curHeat, ref hI.curHeat, heatInfo.heatTransferSpeed);

        if (!isBalanced) {
            SetEnemyColor();
        }

        //trigger the target function to change the color, maybe not :)
    }

    public void SetEnemyColor() {
        render.color = heatInfo.CalculateColor();
    }
}
