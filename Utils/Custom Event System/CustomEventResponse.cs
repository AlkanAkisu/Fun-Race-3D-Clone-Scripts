using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class CustomEventResponse : MonoBehaviour
{
    [SerializeField, NaughtyAttributes.Expandable] CustomEvent Event;

    [SerializeField] UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(OnEventRaised);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}