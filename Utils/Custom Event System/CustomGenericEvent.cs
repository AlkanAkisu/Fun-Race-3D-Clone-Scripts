using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class CustomEvent<T> : ScriptableObject
{
    protected List<Action<T>> callbacks = new List<Action<T>>();

    [SerializeField] T debugValue;

    public void Raise(T val)
    {
        for (int i = callbacks.Count - 1; i >= 0; i--)
            callbacks[i](val);
    }

    [NaughtyAttributes.Button]

    private void Raise() => Raise(debugValue);

    public void RegisterListener(Action<T> callback)
    {
        if (!callbacks.Contains(callback))
            callbacks.Add(callback);
            
    }

    public void UnregisterListener(Action<T> callback)
    {
        if (callbacks.Contains(callback))
            callbacks.Remove(callback);

    }

    public static CustomEvent<T> operator +(CustomEvent<T> _event, Action<T> action)
    {
        _event.RegisterListener(action);
        return _event;
    }
    public static CustomEvent<T> operator -(CustomEvent<T> _event, Action<T> action)
    {
        _event.UnregisterListener(action);
        return _event;
    }

}

public abstract class CustomEvent<T1, T2> : ScriptableObject
{
    protected List<Action<T1, T2>> callbacks = new List<Action<T1, T2>>();

    [SerializeField] T1 debugValue1;
    [SerializeField] T2 debugValue2;

    [NaughtyAttributes.Button]
    private void Raise() => Raise(debugValue1, debugValue2);

    public void Raise(T1 val, T2 val2)
    {
        for (int i = callbacks.Count - 1; i >= 0; i--)
            callbacks[i](val, val2);
    }

    public void RegisterListener(Action<T1, T2> callback)
    {
        if (!callbacks.Contains(callback))
            callbacks.Add(callback);
    }

    public void UnregisterListener(Action<T1, T2> callback)
    {
        if (callbacks.Contains(callback))
            callbacks.Remove(callback);
    }

    public static CustomEvent<T1, T2> operator +(CustomEvent<T1, T2> _event, Action<T1, T2> action)
    {
        _event.RegisterListener(action);
        return _event;
    }
    public static CustomEvent<T1, T2> operator -(CustomEvent<T1, T2> _event, Action<T1, T2> action)
    {
        _event.UnregisterListener(action);
        return _event;
    }

}