using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangerTrigger : MonoBehaviour
{
    [SerializeField] CameraTypes changeTo = CameraTypes.BackCamera;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.GetComponent<Player>() == null)
            return;
            
        var camChanger = FindObjectOfType<CameraChanger>();
        if (camChanger == null)
        {
            
            Debug.LogWarning($"Can't Find {nameof(CameraChanger)}");
            return;
        }

        camChanger.ChangeToSelected(changeTo);
    }
}
