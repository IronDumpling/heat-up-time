using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    public float curHeat;
    public float boundHeat;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        // Get Color Pointer
        render = GetComponent<SpriteRenderer>();

        // First Lurp
        curHeat = GetComponent<PlaneVillainHeat>().curHeat;
        boundHeat = GetComponent<PlaneVillainHeat>().boundHeat;
        ColorLerp(curHeat, boundHeat);
    }

    // Method 1. Color Change
    public void ColorLerp(float curHeat, float boundHeat)
    {
        render.color = gradient.Evaluate(curHeat/boundHeat);
    }
}
