using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;

public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager arCameraManager;
    [SerializeField]
    private TextMeshProUGUI brightnessValue;
    [SerializeField]
    private TextMeshProUGUI tempvalue;
    [SerializeField]
    private TextMeshProUGUI colorCorrectionValue;
    private Light currentLight;
    private void Awake()
    {
        currentLight = GetComponent<Light>();
    }
    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
    }
    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
    }
    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if(args.lightEstimation.averageBrightness.HasValue)
        {
            brightnessValue.text = $"B: {args.lightEstimation.averageBrightness.Value}";
            currentLight.intensity = args.lightEstimation.averageBrightness.Value;
        }
        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            tempvalue.text = $"TV: {args.lightEstimation.averageColorTemperature.Value}";
            currentLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }
        if (args.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrectionValue.text = $"CC: {args.lightEstimation.colorCorrection.Value}";
            currentLight.color = args.lightEstimation.colorCorrection.Value;
        }
    }
}
