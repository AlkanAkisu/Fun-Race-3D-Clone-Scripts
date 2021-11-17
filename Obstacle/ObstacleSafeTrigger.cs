using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class ObstacleSafeTrigger : MonoBehaviour
{
    Action onWhenAreaSafe;


    private void OnTriggerExit(Collider other)
    {
        var obstacle = other.GetComponentInChildren<Obstacle>();

        if (obstacle != null)
            Raise();
    }

    [NaughtyAttributes.Button]
    private void Raise()
    {

        onWhenAreaSafe?.Invoke();
    }
    public void ListenTrigger(Action callback)
    {
        onWhenAreaSafe += callback;
    }
    public void UnListenTrigger(Action callback)
    {
        onWhenAreaSafe -= callback;
    }
}
