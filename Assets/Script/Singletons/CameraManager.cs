using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public delegate void OnCameraChange(Camera camera);
    public static event OnCameraChange OnCameraChanged;

    [SerializeField]
    private Camera[] camerasPlayerBase;

    private int currentCameraIx;
    public Camera CurrentCamera => camerasPlayerBase[currentCameraIx];
    public Camera[] CamerasPlayerBase => camerasPlayerBase;

    public void SwitchCameraTo(int playerBaseNo)
    {
        TurnOffAllCameras();

        currentCameraIx = playerBaseNo;
        CurrentCamera.enabled = true;

        OnCameraChanged?.Invoke(CurrentCamera);
    }

    public void TurnOffAllCameras()
    {
        foreach (var camera in camerasPlayerBase)
        {
            camera.enabled = false;
        }
    }

}
