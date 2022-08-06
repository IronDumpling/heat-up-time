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
            villain.GetComponent<GraffitiController>().boundHeat = Random.Range(maxPlaneHeat / 2, maxPlaneHeat);
            villain.GetComponent<GraffitiController>().curHeat = Random.Range(0f, villain.GetComponent<GraffitiController>().boundHeat/2);
        }

        // Player Initialise
        player.GetComponent<PlayerHeat>().SetBoundHeat(maxPlayerHeat, minPlayerHeat);
    }
}
public static class HeatTransfer {

    public static float mappingCoeff(float currHeat, float maxHeat, float minHeat) {
        return (currHeat - minHeat)/(maxHeat - minHeat);    
    }

    // Method 2. Heat Change
    public static void HeatTransferLerp(ref float currentHeat, ref float otherHeat) {
        //curHeat = (curHeat + otherHeat) / 2;
        //SetCurHeat(curHeat);
    }

    // Method 3. Shoot Bullet
    public static void ShootHeat(int bulletHeat) {
        curHeat -= bulletHeat;
        SetCurHeat(curHeat);
    }

    // Method 4. Color Change
    public static void ColorLerp(float curHeat, float upperBoundHeat, float lowerBoundHeat) {
        render.color = renderGradient.Evaluate((curHeat - lowerBoundHeat) / (upperBoundHeat - lowerBoundHeat));
    }
}