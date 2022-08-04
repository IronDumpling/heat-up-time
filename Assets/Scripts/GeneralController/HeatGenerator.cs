using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGenerator : MonoBehaviour
{
    private GameObject[] hotterPlanes;
    private GameObject[] colderPlanes;
    private GameObject[] villains;
    private GameObject player;

    public float maxHeat = 150f;

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
            plane.GetComponent<PlaneController>().boundHeat = maxHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(maxHeat / 2, maxHeat);
        }

        // Hotter Plane Heat Initialise
        foreach (GameObject plane in colderPlanes)
        {
            plane.GetComponent<PlaneController>().boundHeat = maxHeat;
            plane.GetComponent<PlaneController>().curHeat = Random.Range(0f, maxHeat / 2);
        }

        // Villains Heat Initialise
        foreach (GameObject villain in villains)
        {
            villain.GetComponent<VillainController>().boundHeat = Random.Range(maxHeat / 2, maxHeat);
            villain.GetComponent<VillainController>().curHeat = Random.Range(0f, villain.GetComponent<VillainController>().boundHeat/2);
        }

        // Player Initialise
        player.GetComponent<PlayerHeat>().SetBoundHeat(100f);
    }
}
