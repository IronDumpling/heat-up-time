using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public const int PLAYER = 3;
    public const int VILLAINS = 7;
    public const int BULLETS = 8;
    public const int PLATFORMS = 9;
    
    // Component pointers
    private GameObject collideObj;
    
    private List<GameObject> collideObjs;
    // Heat Value
    public float curHeat;
    public float boundHeat;
    public float ratePerSec;
    // Color Change
    [SerializeField] private SpriteRenderer render;
    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        // Pointers
        render = GetComponent<SpriteRenderer>();
        collideObjs = new List<GameObject>();

        // First Lerp
        ColorLerp(curHeat, boundHeat);
    }

    void FixedUpdate()
    {
        HeatTransferHandler();
        ColorLerp(curHeat, boundHeat);
    }

    // Method 1. Get colliding object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collideObj = collision.gameObject;

        collideObjs.Add(collideObj);
        // 1.1 Touch Bullets
        if (collideObj.layer == 8) // 8 is layer of bullets
        {
            float otherHeat = collideObj.GetComponent<BulletController>().curHeat;
            HeatGain(otherHeat);

        }

        // 1.2 Touhch Platforms
        else if (collideObj.layer == 9 && collideObj.tag != "SafePlane") // 9 is layer of platforms
        {
            float otherHeat = collideObj.GetComponent<PlaneController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.3 Touhch Villains
        else if (collideObj.layer == 7) // 7 is layer of villains
        {
            float otherHeat = collideObj.GetComponent<GraffitiController>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // 1.4 Touch Player
        else if (collideObj.layer == 3) // 3 is layer of player
        {
            float otherHeat = collideObj.GetComponent<PlayerHeat>().curHeat;
            if (otherHeat != curHeat)
            {
                HeatTransfer(otherHeat);
            }
        }

        // Change color of planes and villains
        ColorLerp(curHeat, boundHeat);
    }

    private void OnCollisionExit2D(Collision2D collision){
        collideObjs.Remove(collision.gameObject);
    }

    // Method 2. Heat Transfer
    void HeatTransfer(float otherHeat)
    {
        float endHeat = (curHeat + otherHeat) / 2;
        curHeat += (endHeat - curHeat) * Time.deltaTime;
    }

    // Method 3. Heat Gain
    void HeatGain(float otherHeat)
    {
        curHeat += otherHeat;
    }

    // Method 4. Color Change
    public void ColorLerp(float curHeat, float boundHeat)
    {
        render.color = gradient.Evaluate(curHeat / boundHeat);
    }

    void HeatTransferHandler(){

        foreach (GameObject collider in collideObjs){
            float otherHeat;
            
            switch(collider.layer){
                case PLAYER:
                    otherHeat = collider.GetComponent<PlayerHeat>().curHeat;
                    break;

                case VILLAINS:
                    otherHeat = collider.GetComponent<GraffitiController>().curHeat;
                    break;

                case BULLETS:
                    otherHeat = collider.GetComponent<BulletController>().curHeat;
                    break;

                case PLATFORMS:
                    if (collider.tag != "SafePlane"){
                        otherHeat = collider.GetComponent<PlaneController>().curHeat;
                    }else{
                        otherHeat = curHeat;
                    }
                    break;

                default:
                    otherHeat = curHeat;
                    break;
                
            }
            HeatTransfer(otherHeat);
        }
    }
}

