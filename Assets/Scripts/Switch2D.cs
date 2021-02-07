using System.Collections;
using System.Collections.Generic;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// <summary>
/// Switches back to none VR once looped back to title screen.
/// </summary>
public class Switch2D : MonoBehaviour
{

    /// <summary>
    /// Gets a value indicating whether the VR mode is enabled.
    /// </summary>
    private bool _isVrModeEnabled
    {
        get
        {
            return XRGeneralSettings.Instance.Manager.isInitializationComplete;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_isVrModeEnabled)
        {
            StartCoroutine(SwitchTo2D());
            Api.UpdateScreenParams();
        }
    }   

    // Call via `StartCoroutine(SwitchTo2D())` from your code. Or, use
    // `yield SwitchTo2D()` if calling from inside another coroutine.
    IEnumerator SwitchTo2D()
    {
        // Empty string loads the "None" device.
        //XRSettings.LoadDeviceByName("");
        StopXR();
        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Not needed, since loading the None (`""`) device takes care of this.
        // XRSettings.enabled = false;

        // Restore 2D camera settings.
        ResetCameras();
    }

    // Resets camera transform and settings on all enabled eye cameras.
    void ResetCameras()
    {
        // Camera looping logic copied from GvrEditorEmulator.cs
        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {

                // Reset local position.
                // Only required if you change the camera's local position while in 2D mode.
                cam.transform.localPosition = Vector3.zero;

                // Reset local rotation.
                // Only required if you change the camera's local rotation while in 2D mode.
                cam.transform.localRotation = Quaternion.identity;

                // No longer needed, see issue github.com/googlevr/gvr-unity-sdk/issues/628.
                // cam.ResetAspect();

                // No need to reset `fieldOfView`, since it's reset automatically.
            }
        }
    }

    /// <summary>
    /// Stops and deinitializes the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");
    }
}
