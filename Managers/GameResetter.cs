using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameResetter : MonoBehaviour
{
    const string resetPPName = "isFirstTime";
    [SerializeField] UnityEvent resetEvent;
    [SerializeField, ReadOnly] bool isFirstTime;
    private void Awake()
    {
        isFirstTime = PlayerPrefsX.GetBool(resetPPName, defaultValue: true);
        if (isFirstTime)
        {
            resetEvent?.Invoke();
            PlayerPrefsX.SetBool(resetPPName, false);

        }
    }

    [NaughtyAttributes.Button]
    private void Reset()
    {
        isFirstTime = true;
        PlayerPrefsX.SetBool(resetPPName, isFirstTime);
    }
}
