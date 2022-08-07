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

    public float heatTransferSpeed = 15f;

    // Start is called before the first frame update
    void Awake() {
        // Get Plane Lists
        hotterPlanes = GameObject.FindGameObjectsWithTag("HotterPlane");
        colderPlanes = GameObject.FindGameObjectsWithTag("ColderPlane");
        villains = GameObject.FindGameObjectsWithTag("Villain");
        player = GameObject.FindGameObjectWithTag("Player");

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in hotterPlanes) {
            HeatInfo hI = plane.GetComponent<HeatInfo>();
            hI.setVal(Random.Range(0, maxPlaneHeat), maxPlaneHeat, minPlaneHeat, heatTransferSpeed);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in colderPlanes) {
            HeatInfo hI = plane.GetComponent<HeatInfo>();
            hI.setVal(Random.Range(minPlaneHeat, 0f), maxPlaneHeat, minPlaneHeat, heatTransferSpeed);
        }

        // Villains Heat Initialise
        foreach (GameObject villain in villains) {
            HeatInfo hI = villain.GetComponent<HeatInfo>();
            hI.setVal(Random.Range(minEnemyHeat, maxEnemyHeat), maxEnemyHeat, minEnemyHeat, heatTransferSpeed);
        }

        HeatInfo plyH = player.GetComponent<HeatInfo>();
        plyH.setVal((maxPlayerHeat - minPlayerHeat)/2+minPlayerHeat, maxPlaneHeat, minPlaneHeat, heatTransferSpeed);
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

    // Method 2. Heat Balance, return if is balanced
    public static bool HeatBalance(ref float srcHeat, ref float targetHeat, float speed) {
        if(Mathf.Abs(srcHeat - targetHeat) < 0.1f) return true;
        float tempSrc = Mathf.Lerp(srcHeat, targetHeat, 0.5f * speed * Time.deltaTime);
        targetHeat = targetHeat - (tempSrc - srcHeat);
        srcHeat = tempSrc;
        return false;
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