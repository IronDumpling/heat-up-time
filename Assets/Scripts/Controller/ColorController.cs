using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    //[SerializeField] private Component[] rendererList;
    [SerializeField] private SpriteRenderer renderer;
    //[SerializeField] [Range(0f, 1f)] float lerpTime;
    public float curHeat;
    public float boundHeat;
    private Color heatFloor;
    private Color heatCeiling;


    // Start is called before the first frame update
    void Start()
    {
        // Get Color Pointer
        //myRenderer = GetComponents(typeof(SpriteRenderer));
        renderer = GetComponent<SpriteRenderer>();

        // Color Bound
        heatFloor = new Color(0, 93, 255, 255);
        heatCeiling = new Color(255, 0, 0, 255);

        // First Lurp
        colorLerp();
    }

    // Method 1. On Collision

    // Method 2. Color Change
    public void colorLerp()
    {
        // Get Heat
        curHeat = GetComponent<PlaneVillainHeat>().curHeat;
        boundHeat = GetComponent<PlaneVillainHeat>().boundHeat;

        //renderer.color = Color.Lerp(heatFloor, heatCeiling, curHeat/boundHeat);
        //renderer.color = Color.Lerp(heatFloor, heatCeiling, lerpTime);
    }
}
