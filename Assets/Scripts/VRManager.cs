using System.Collections;
using System.Collections.Generic;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.UI;

/// <summary>
/// This class switches to VR mode if VR is selected in the title screen.
/// </summary>
public class VRManager : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        // Configures the app to not shut down the screen and sets the brightness to maximum.
        // Brightness control is expected to work only in iOS, see:
        // https://docs.unity3d.com/ScriptReference/Screen-brightness.html.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        // Checks if the device parameters are stored and scans them if not.
        // This is only required if the XR plugin is initialized on startup,
        // otherwise these API calls can be removed and just be used when the XR
        // plugin is started.
        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }

        EnterVR();
    }

    /// <summary>
    /// Enters VR mode.
    /// </summary>
    private void EnterVR()
    {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }

    /// <summary>
    /// Initializes and starts the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    ///
    /// <returns>
    /// Returns result value of <c>InitializeLoader</c> method from the XR General Settings Manager.
    /// </returns>
    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
            MakeCanvasChildren();
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
            MakeCanvasChildren();
            //GameObject SGameObject.Find()
        }
    }

    private void MakeCanvasChildren()
    {
        string[] canvasArr = { "ScoreCanvas", "RaceCountdownCanvas", "TimeDisplayCanvas" };
        foreach (string canvasName in canvasArr)
        {
            GameObject scoreCanvasGO = GameObject.Find(canvasName);
            if (scoreCanvasGO != null)
            {
                Canvas scoreCanvas = scoreCanvasGO.GetComponent<Canvas>();
                scoreCanvas.renderMode = RenderMode.WorldSpace;
                scoreCanvas.worldCamera = Camera.main;
                scoreCanvasGO.transform.parent = Camera.main.transform;
                RectTransform scoreRect = scoreCanvasGO.GetComponent<RectTransform>();
                scoreRect.localScale *= 0.002f;
                scoreRect.localPosition = new Vector3(0, 0, 2.85f);
            }
        }
    }
}
