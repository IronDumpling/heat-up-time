using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class WorldSpeed : MonoBehaviour
{
    [Range(0.5f, 2)]    
    public float modifyScale;

    public Rigidbody2D plyCtrl;

    private void Start() {
        modifyScale = 1f;
    }

    public bool on;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = modifyScale;

        //Vector2 veloc = plyCtrl.velocity;
        //if (on) {
        //    plyCtrl.velocity = new Vector2(veloc.x / modifyScale, veloc.y);
        //    plyCtrl.gravityScale = 1 / modifyScale;
        //}
    }
}