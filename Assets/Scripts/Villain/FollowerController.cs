using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : GraffitiController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        upperHeatBound = 0.9f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
