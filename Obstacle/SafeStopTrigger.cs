using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SafeStopTrigger : MonoBehaviour
{

    [SerializeField] private ObstacleSafeTrigger _obstacleSafeTrigger;

    public ObstacleSafeTrigger ObstacleSafeTrigger => _obstacleSafeTrigger;

    private void OnTriggerEnter(Collider other)
    {

        var ai = other.GetComponent<AIMoveController>();
        ai?.EnteredSafeStop(ObstacleSafeTrigger);
    }
    private void OnTriggerExit(Collider other)
    {
        var ai = other.GetComponent<AIMoveController>();
        ai?.LeftSafeStop(ObstacleSafeTrigger);

    }
}
