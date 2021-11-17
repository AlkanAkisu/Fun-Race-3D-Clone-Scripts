using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


[RequireComponent(typeof(CameraChanger))]
public class DeathCamera : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] FloatReference deathCamSecond;

    #endregion

    #region Private Fields
    CameraChanger _camChanger;

    #endregion

    #region Dependencies
    public CameraChanger CamChanger => _camChanger ?? (_camChanger = GetComponent<CameraChanger>());


    #endregion

    public void PlayerDied()
    {
        var cam = CamChanger.GetCurrentCinemachine();
        var lookat = cam.LookAt;
        var folllow = cam.Follow;
        cam.LookAt = null;
        cam.Follow = null;
        StartCoroutine(ChangeToPlayer(lookat, folllow));
    }

    IEnumerator ChangeToPlayer(Transform lookat, Transform folllow)
    {
        yield return new WaitForSeconds(deathCamSecond);
        var cam = CamChanger.GetCurrentCinemachine();
        var player = FindObjectOfType<Player>().transform;
        cam.LookAt = lookat;
        cam.Follow = folllow;
    }
}
