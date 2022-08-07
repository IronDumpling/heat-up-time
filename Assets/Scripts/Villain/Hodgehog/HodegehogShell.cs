using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HodegehogShell : MonoBehaviour
{
    public float rotateSpeed = 120;
    private Transform center;

    // Start is called before the first frame update
    private void Start()
    {
        center = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
