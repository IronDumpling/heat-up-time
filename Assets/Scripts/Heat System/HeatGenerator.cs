using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGenerator : MonoBehaviour
{
    private GameObject[] hotterPlanes;
    private GameObject[] colderPlanes;
    private GameObject[] villains;
    public float maxHeat = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        // Get Plane Lists
        hotterPlanes = GameObject.FindGameObjectsWithTag("HotterPlane");
        colderPlanes = GameObject.FindGameObjectsWithTag("ColderPlane");
        villains = GameObject.FindGameObjectsWithTag("Villain");

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in hotterPlanes)
        {
            plane.GetComponent<PlaneVillainHeat>().boundHeat = maxHeat;
            plane.GetComponent<PlaneVillainHeat>().curHeat = Random.Range(maxHeat / 2, maxHeat);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in colderPlanes)
        {
            plane.GetComponent<PlaneVillainHeat>().boundHeat = maxHeat;
            plane.GetComponent<PlaneVillainHeat>().curHeat = Random.Range(0f, maxHeat / 2);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject villain in villains)
        {
            villain.GetComponent<PlaneVillainHeat>().boundHeat = Random.Range(maxHeat / 2, maxHeat);
            villain.GetComponent<PlaneVillainHeat>().curHeat = Random.Range(0f, villain.GetComponent<PlaneVillainHeat>().boundHeat);
        }
    }
}
