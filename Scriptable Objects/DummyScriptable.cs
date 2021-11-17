using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class DummyScriptable : ScriptableObject
{
    [SerializeField] int health;
    [SerializeField, ReadOnly] int initialValue;

    private void OnValidate()
    {
        if (!Application.isPlaying)
            Initialize();
    }
    private void OnEnable()
    {
        Initialize();
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    private void Initialize()
    {
        initialValue = health;
    }

    private void ModeChanged(PlayModeStateChange obj)
    {
        if (obj == PlayModeStateChange.ExitingPlayMode)
            health = initialValue;

    }
}