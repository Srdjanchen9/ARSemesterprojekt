using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager _cameraManager;

    private Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    void OnEnable()
    {
        if (_cameraManager != null)
            _cameraManager.frameReceived += CameraFrameChanged;
    }

    void OnDisable()
    {
        if (_cameraManager != null)
            _cameraManager.frameReceived -= CameraFrameChanged;
    }

    private void CameraFrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            _light.intensity = args.lightEstimation.averageBrightness.Value;
            Debug.Log($"Brightness: {_light.intensity}");
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            _light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            Debug.Log($"Color Temperature: {_light.colorTemperature}");
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            _light.color = args.lightEstimation.colorCorrection.Value;
            Debug.Log($"Color Correction: {_light.color}");
        }

        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            var mainLightDirection = args.lightEstimation.mainLightDirection;
            _light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
            Debug.Log($"Main Light Direction: {mainLightDirection.Value}");
        }

        if (args.lightEstimation.mainLightColor.HasValue)
        {
            _light.color = args.lightEstimation.mainLightColor.Value;
            Debug.Log($"Main Light Color: {_light.color}");
        }

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            _light.intensity = args.lightEstimation.mainLightIntensityLumens.Value;
            Debug.Log($"Main Light Intensity Lumens: {_light.intensity}");
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
            Debug.Log("Ambient Spherical Harmonics updated.");
        }
    }
}
