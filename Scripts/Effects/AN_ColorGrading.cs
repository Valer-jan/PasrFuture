using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AN_ColorGrading : MonoBehaviour
{
    public PostProcessVolume PostProc;
    public int Speed = 2;
    public float setTemp = 0, currentTemp = 0;
    ColorGrading clrGrad = null;

    private void Start()
    {
        PostProc.profile.TryGetSettings(out clrGrad);
    }

    private void Update()
    {
        if (setTemp - currentTemp > 1) currentTemp += Time.deltaTime * Speed;
        else if (setTemp - currentTemp < -1) currentTemp -= Time.deltaTime * Speed;
        clrGrad.temperature.value = currentTemp;
    }
}
