using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGenerator : MonoBehaviour
{
    private GameObject[] hotterPlanes;
    private GameObject[] colderPlanes;
    private GameObject[] villains;
    private GameObject player;

    public float maxPlaneHeat = 150f;
    public float minPlaneHeat = -150f;

    public float maxPlayerHeat = 100f;
    public float minPlayerHeat = -100f;

    // Start is called before the first frame update
    void Awake()
    {
        // Get Plane Lists
        hotterPlanes = GameObject.FindGameObjectsWithTag("HotterPlane");
        colderPlanes = GameObject.FindGameObjectsWithTag("ColderPlane");
        villains = GameObject.FindGameObjectsWithTag("Villain");
        player = GameObject.FindGameObjectWithTag("Player");

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in hotterPlanes)
        {
            plane.GetComponent<PlaneController>().maxHeat = maxPlaneHeat;
            plane.GetComponent<PlaneController>().minHeat = minPlaneHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(0, maxPlaneHeat);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in colderPlanes)
        {
            plane.GetComponent<PlaneController>().maxHeat = maxPlaneHeat;
            plane.GetComponent<PlaneController>().minHeat = minPlaneHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(minPlaneHeat, 0f);
        }

        // Villains Heat Initialise
        foreach (GameObject villain in villains)
        {
            villain.GetComponent<GraffitiController>().upperHeatBound  = Random.Range(maxPlaneHeat / 2, maxPlaneHeat);
            villain.GetComponent<GraffitiController>().curHeat = Random.Range(0f, villain.GetComponent<GraffitiController>().upperHeatBound /2);
        }

        // Player Initialise
        player.GetComponent<PlayerHeat>().InitalizePlayerHeat(maxPlayerHeat, minPlayerHeat);
    }
}
public static class HeatOp {

    public static float HeatCoeff(float curHeat, float maxHeat, float minHeat) {
        return (curHeat - minHeat)/(maxHeat - minHeat);    
    }

    // Method 2. Heat Balance
    public static void HeatBalance(ref float srcHeat, ref float targetHeat, float speed) {
        float tempSrc = Mathf.Lerp(srcHeat, targetHeat, 0.5f * speed * Time.deltaTime);
        targetHeat = targetHeat - (tempSrc - srcHeat);
        srcHeat = tempSrc;
    }

    // Method 3. Heat Transfer
    public static void HeatTransfer(ref float currentHeat, float gainAmount) {
        currentHeat += gainAmount;
    }

    // Method 4. Color Change
    public static void ColorLerp(ref SpriteRenderer rend, Gradient grad, float HeatCoeff) {
        rend.color = grad.Evaluate(HeatCoeff);
    }
}