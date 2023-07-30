using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public float minIntensityValue = 0f;
    public float maxIntensityValue = 1f;
    public float lerpDuration = 2.0f;
    public bool reverse;

    private Vignette vignette;
    private bool isLerping = false;
    private float lerpStartTime;

    private void Awake()
    {
        if (postProcessVolume == null)
        {
            Debug.LogError("PostProcessVolume reference is missing. Please assign it in the Inspector.");
            enabled = false;
            return;
        }
        postProcessVolume.profile.TryGetSettings(out vignette);
        if (vignette == null)
        {
            Debug.LogError("Vignette settings not found in the PostProcessVolume. Make sure Vignette is enabled.");
            enabled = false;
            return;
        }
        if (reverse)
            vignette.intensity.Override(maxIntensityValue);
        else vignette.intensity.Override(minIntensityValue);
    }

    private void OnEnable()
    {
        StartLerp(minIntensityValue, maxIntensityValue);
    }

    void Update()
    {
        if (isLerping)
        {
            float timeSinceLerpStarted = Time.time - lerpStartTime;
            float percentageComplete = Mathf.PingPong(timeSinceLerpStarted / lerpDuration, 1.0f);
            float currentIntensityValue;
            if (reverse)
                currentIntensityValue = Mathf.Lerp(maxIntensityValue, minIntensityValue, percentageComplete);
            else
                currentIntensityValue = Mathf.Lerp(minIntensityValue, maxIntensityValue, percentageComplete);
            vignette.intensity.Override(currentIntensityValue);
            if (percentageComplete > 0.99f)
            {
                isLerping = false;
            }
        }
    }

    public void StartLerp(float minValue, float maxValue)
    {
        minIntensityValue = minValue;
        maxIntensityValue = maxValue;
        lerpStartTime = Time.time;
        isLerping = true;
    }
}
