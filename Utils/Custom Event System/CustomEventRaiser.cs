using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class CustomEventRaiser : MonoBehaviour
{
    [SerializeField] CustomEvent Event;


    [NaughtyAttributes.Button("Raise")]
    public void RaiseCustomEvent()
    {
        Event?.Raise();
    }
}