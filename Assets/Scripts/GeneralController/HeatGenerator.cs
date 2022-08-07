using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGenerator : MonoBehaviour {
    private GameObject[] hotterPlanes;
    private GameObject[] colderPlanes;
    private GameObject[] villains;
    private GameObject player;

    public float maxPlaneHeat = 150f;
    public float minPlaneHeat = -150f;

    public float maxPlayerHeat = 100f;
    public float minPlayerHeat = -100f;

    public float maxEnemyHeat = 80f;
    public float minEnemyHeat = -80f;

    // Start is called before the first frame update
    void Awake() {
        // Get Plane Lists
        hotterPlanes = GameObject.FindGameObjectsWithTag("HotterPlane");
        colderPlanes = GameObject.FindGameObjectsWithTag("ColderPlane");
        villains = GameObject.FindGameObjectsWithTag("Villain");
        player = GameObject.FindGameObjectWithTag("Player");

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in hotterPlanes) {
            plane.GetComponent<PlaneController>().maxHeat = maxPlaneHeat;
            plane.GetComponent<PlaneController>().minHeat = minPlaneHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(0, maxPlaneHeat);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in colderPlanes) {
            plane.GetComponent<PlaneController>().maxHeat = maxPlaneHeat;
            plane.GetComponent<PlaneController>().minHeat = minPlaneHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(minPlaneHeat, 0f);
        }

        // Villains Heat Initialise
        foreach (GameObject villain in villains) {
            villain.GetComponent<GraffitiController>().upperHeatBound = maxEnemyHeat;
            villain.GetComponent<GraffitiController>().lowerHeatBound = minEnemyHeat;
            villain.GetComponent<GraffitiController>().curHeat = Random.Range(minEnemyHeat, maxEnemyHeat);

            // Player Initialise
            player.GetComponent<PlayerHeat>().InitalizePlayerHeat(maxPlayerHeat, minPlayerHeat);
        }
    }
}



public static class HeatOp {

    public static float mapBoundary(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    // Returns value from 0 to 1, Clamp if curHeat exceed boundary
    public static float HeatCoeff(float curHeat, float maxHeat, float minHeat) {
        return Mathf.Clamp((curHeat - minHeat)/(maxHeat - minHeat), 0, 1);    
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
    public static Color ColorLerp(Gradient grad, float HeatCoeff) {
        return grad.Evaluate(HeatCoeff);
    }
}