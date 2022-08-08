using UnityEngine;

public class TimeScaleEditor : MonoBehaviour
{
    [Header("Player TimeScale Attribute")]
    [SerializeField]
    private float TimeScaletoView;
    public float upperTimeScale = 2.5f;
    public float lowerTimeScale = 0.5f;
    public float slopDecay = 30f;

    private bool forceTimeScale;
    private float forceTSKeeper;

    private float timeScaleHelperFunc(float x) {
        return -(Mathf.Log(1 / x - 1, (float)System.Math.E)) / slopDecay + 0.5f;
    }

    public void UpdateTimescaleByPlayerHeat(HeatInfo plyHeat) {
        TimeScaletoView = Time.timeScale;

        if (forceTimeScale) return;

        float x = 1 - HeatOp.HeatCoeff(plyHeat.curHeat, plyHeat.maxHeat, plyHeat.minHeat);
        float y = timeScaleHelperFunc(x);

        if (x < 0.5) y = HeatOp.mapBoundary(y, 0, 0.5f, lowerTimeScale, 1);
        else y = HeatOp.mapBoundary(y, 0.5f, 1, 1, upperTimeScale);

        y = Mathf.Clamp(y, lowerTimeScale, upperTimeScale);

        Time.timeScale = y;
    }

    public void ForceSetTimeScale(float forceScale) {
        forceTimeScale = true;
        forceTSKeeper = Time.timeScale;
        Time.timeScale = forceScale;
    }
    public void ReleaseUnsetTimeScale() {
        forceTimeScale = false;
        Time.timeScale = forceTSKeeper;
    }


}
