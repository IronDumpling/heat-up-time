using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeat : MonoBehaviour
{
    public SpriteRenderer render;
    public HeatInfo heatInfo;
    private PlayerController plyCtrl;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
        heatInfo = GetComponent<HeatInfo>();

        plyCtrl = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        SetEnemyColor();

        //Player
        if(collision.gameObject.layer == 3){
            plyCtrl.Hurt(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        HeatInfo hI = collision.gameObject.GetComponent<HeatInfo>();
        if (!hI) return;
        bool isBalanced = HeatOp.HeatBalance(ref heatInfo.curHeat, ref hI.curHeat, heatInfo.heatTransferSpeed);

        if (!isBalanced) {
            SetEnemyColor();
        }

        //Player
        if(collision.gameObject.layer == 3){
            plyCtrl.Hurt(this.gameObject);
        }
        //trigger the target function to change the color, maybe not :)
    }

    public void SetEnemyColor() {
        render.color = heatInfo.CalculateColor();
    }
}
