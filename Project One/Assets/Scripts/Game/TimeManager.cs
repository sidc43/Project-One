using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int noon;
    [SerializeField] private int midnight;

    private static float minIntensity = 0.1F;
    private static float maxIntensity = 1.0F;

    private float elapsedTime = 0F;

    [SerializeField] private Light2D globalLight;

    public enum TimeOfDay
    {
        Dawn,
        Morning,
        Afternoon,
        Night
    };

    private TimeOfDay timeOfDay;

    // Start is called before the first frame update
    void Start()
    {
        globalLight.intensity = minIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        DayNightCycle();

    }

    private void DayNightCycle()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 0F && elapsedTime < noon)
        {   
            float percentageComplete = elapsedTime / noon;

            globalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, percentageComplete);
        } else if (elapsedTime >= noon)
        {   
            float percentageComplete = (elapsedTime - noon) / midnight;

            if (percentageComplete > 1F) {
                elapsedTime = 0F;
            } else {
                globalLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, percentageComplete);
            } 
        } 
    }

    private void SetTimeOfDay()
    {
        if (elapsedTime >= 0 && elapsedTime < noon / 2) {
            timeOfDay = TimeOfDay.Dawn;
        } else if (elapsedTime >= noon / 2 && elapsedTime < noon) {
            timeOfDay = TimeOfDay.Morning;
        } else if (elapsedTime >= noon && elapsedTime < (midnight - noon) / 2) {
            timeOfDay = TimeOfDay.Afternoon;
        } else {
            timeOfDay = TimeOfDay.Night;
        }
    }

    public TimeOfDay GetTimeOfDay() => timeOfDay;
}
