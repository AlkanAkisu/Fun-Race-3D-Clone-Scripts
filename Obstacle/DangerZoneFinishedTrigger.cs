using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class DangerZoneFinishedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var ai = other.GetComponent<AIMoveController>();

        if (ai == null)
            return;

        ai.NonDangerZone();
    }
}
