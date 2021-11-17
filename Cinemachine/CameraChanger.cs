using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CameraDictionary cams;
    [SerializeField] CameraTypes currentType = CameraTypes.BackCamera;
    void OnValidate()
    {
        ChangeToSelectedCam(currentType);
    }

    private void ChangeAllToDefault()
    {
        foreach (var cam in cams)
        {
            cam.Value.Priority = 10;
        }
    }
    private void ChangeToSelectedCam(CameraTypes cameraType)
    {
        currentType = cameraType;
        ChangeAllToDefault();
        cams[cameraType].Priority = 20;
    }
    public void ChangeToFront() => ChangeToSelectedCam(CameraTypes.FrontCamera);
    public void ChangeToBack() => ChangeToSelectedCam(CameraTypes.BackCamera);

    public void ChangeToSelected(CameraTypes cameraType)
    {
        ChangeToSelectedCam(cameraType);
    }
    public CinemachineVirtualCamera GetCurrentCinemachine() => cams[currentType];
}

public enum CameraTypes
{
    SideCamera,
    BackCamera,
    FrontCamera,
    UpCamera,
    ZoomOutUpCamera,
    UICamera,
    BackSideCamera


}

[System.Serializable]
public class CameraDictionary : SerializableDictionary<CameraTypes, CinemachineVirtualCamera> { }

[System.Serializable]
public class CamEvent : UnityEvent<CameraTypes>
{
}