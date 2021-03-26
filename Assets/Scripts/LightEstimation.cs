using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    ARCameraManager cameraManager;


    public ARCameraManager CameraManager
    {
        get { return cameraManager; }
        set
        {
            if (cameraManager == value)
                return;

            if (cameraManager != null)
                cameraManager.frameReceived -= FrameChanged;

            cameraManager = value;

            if (cameraManager != null & enabled)
                cameraManager.frameReceived += FrameChanged;
        }
    }


    public float? brightness { get; private set; }


    public float? colorTemperature { get; private set; }


    public Color? colorCorrection { get; private set; }


    public Vector3? mainLightDirection { get; private set; }


    public Color? mainLightColor { get; private set; }


    public float? mainLightIntensityLumens { get; private set; }


    public SphericalHarmonicsL2? sphericalHarmonics { get; private set; }

    void Awake()
    {
        light = GetComponent<Light>();
    }

    void OnEnable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived += FrameChanged;
        }

    }

    void OnDisable()
    {

        if (cameraManager != null)
        {
            cameraManager.frameReceived -= FrameChanged;
        }
    }



    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            brightness = args.lightEstimation.averageBrightness.Value;
            light.intensity = brightness.Value;
        }
        else
        {
            brightness = null;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            light.colorTemperature = colorTemperature.Value;
        }
        else
        {
            colorTemperature = null;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            colorCorrection = args.lightEstimation.colorCorrection.Value;
            light.color = colorCorrection.Value;
        }
        else
        {
            colorCorrection = null;
        }

        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            mainLightDirection = args.lightEstimation.mainLightDirection;
            light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
        }

        if (args.lightEstimation.mainLightColor.HasValue)
        {
            mainLightColor = args.lightEstimation.mainLightColor;
            light.color = mainLightColor.Value;
        }
        else
        {
            mainLightColor = null;
        }

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
        {
            mainLightIntensityLumens = args.lightEstimation.mainLightIntensityLumens;
            light.intensity = args.lightEstimation.averageMainLightBrightness.Value;
        }
        else
        {
            mainLightIntensityLumens = null;
        }

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = sphericalHarmonics.Value;
        }
        else
        {
            sphericalHarmonics = null;
        }
    }

    Light light;
}
