using UnityEngine;
using System;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu]
public class CustomEvent : ScriptableObject
{
    protected List<Action> callbacks = new List<Action>();

    [Button]
    public void Raise()
    {
        for (int i = callbacks.Count - 1; i >= 0; i--)
            callbacks[i]();
    }

    public void RegisterListener(Action callback)
    {
        if (!callbacks.Contains(callback))
            callbacks.Add(callback);
    }

    public void UnregisterListener(Action callback)
    {
        if (callbacks.Contains(callback))
            callbacks.Remove(callback);
    }

    public static CustomEvent operator +(CustomEvent _event, Action action)
    {
        _event.RegisterListener(action);
        return _event;
    }

    public static CustomEvent operator -(CustomEvent _event, Action action)
    {
        _event.UnregisterListener(action);
        return _event;
    }

    internal void Register(object onEvent)
    {
        throw new NotImplementedException();
    }
}
