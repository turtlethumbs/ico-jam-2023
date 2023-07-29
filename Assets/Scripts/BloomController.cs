using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class BloomController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public float bloomMinValue = 0.5f;
    public float bloomMaxValue = 3.0f;
    public float lerpDuration = 2.0f;

    private Bloom bloom;
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
        postProcessVolume.profile.TryGetSettings(out bloom);
        if (bloom == null)
        {
            Debug.LogError("Bloom settings not found in the PostProcessVolume. Make sure Bloom is enabled.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        // Start the initial lerp
        StartLerp(bloomMinValue, bloomMaxValue);
    }
    private void Update()
    {
        if (isLerping)
        {
            float timeSinceLerpStarted = Time.time - lerpStartTime;
            float percentageComplete = Mathf.PingPong(timeSinceLerpStarted / lerpDuration, 1.0f);
            float currentBloomValue = Mathf.Lerp(bloomMinValue, bloomMaxValue, percentageComplete);

            bloom.intensity.Override(currentBloomValue);

            if (percentageComplete >= 0.99f) // Slightly below 1.0 to avoid abrupt change
            {
                // Reverse the lerp direction by swapping min and max values
                float temp = bloomMinValue;
                bloomMinValue = bloomMaxValue;
                bloomMaxValue = temp;

                // Restart the lerp
                StartLerp(bloomMinValue, bloomMaxValue);
            }
        }
    }

    public void StartLerp(float minValue, float maxValue)
    {
        bloomMinValue = minValue;
        bloomMaxValue = maxValue;
        lerpStartTime = Time.time;
        isLerping = true;
    }
}
