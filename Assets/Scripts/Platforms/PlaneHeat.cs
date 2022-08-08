using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHeat : MonoBehaviour
{
    public const int PLAYER = 3;
    public const int VILLAINS = 7;
    public const int BULLETS = 8;
    public const int PLATFORMS = 9;
    
    public HeatInfo heatInfo;

    // Color Change
    [SerializeField] private SpriteRenderer render;

    void Awake(){
        render = GetComponent<SpriteRenderer>();
        heatInfo = GetComponent<HeatInfo>();
    }
    // Start is called before the first frame update
   private void Start()
    {
        if (heatInfo) {
            SetPlaneColor();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        SetPlaneColor();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!heatInfo) return;

        HeatInfo hI = collision.gameObject.GetComponent<HeatInfo>();
        if (!hI)return;
        if (Mathf.Abs(heatInfo.curHeat-hI.curHeat) < 0.1f){
            SetPlaneColor();
        }
        //trigger the target function to change the color, maybe not :)
    }

    private void SetPlaneColor(){
        heatInfo.DebugLogInfo("Plane onCollSty");
        render.color = heatInfo.CalculateColor();
    }
}

