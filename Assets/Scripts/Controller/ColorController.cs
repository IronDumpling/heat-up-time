using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer myRenderer;
    public float curHeat;
    public float boundHeat;
    private Color heatFloor;
    private Color heatCeiling;

    // Start is called before the first frame update
    void Start()
    {
        // Get Color Pointer
        myRenderer = GetComponent<SpriteRenderer>();
        // Color Bound
        heatFloor = new Color(0, 93, 255, 255);
        heatCeiling = new Color(255, 0, 0, 255);

        // First Lurp
        colorLurp();
    }

    // On Collision


    // Method 1. Color Change
    public void colorLurp()
    {
        // Get Heat
        curHeat = GetComponent<PlaneVillainHeat>().curHeat;
        boundHeat = GetComponent<PlaneVillainHeat>().boundHeat;

        myRenderer.color = Color.Lerp(heatFloor, heatCeiling, curHeat/boundHeat);
    }
}
