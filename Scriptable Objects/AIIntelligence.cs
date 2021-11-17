using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AIIntelligence : ScriptableObject
{
    [SerializeField] AIDictionary overridePlatfromSuccessRate;

    [SerializeField] float stopAtSafeStopRate = 1f;// default val
    [SerializeField] float defaultSuccessRate = 1f;// default val

    private static System.Random Rand = new System.Random();

    public bool IsSuccessful(PlatformSO platform)
    {
        return ((float)Rand.NextDouble()) < GetSuccess(platform);
    }
    public float GetSuccess(PlatformSO platform)
    {
        return overridePlatfromSuccessRate.ContainsKey(platform)
        ? overridePlatfromSuccessRate[platform]
        : defaultSuccessRate;
    }
    public bool DoesStopAtSafeStop()
    {
        return ((float)Rand.NextDouble()) < stopAtSafeStopRate;
    }
}


[System.Serializable]
public class AIDictionary : SerializableDictionary<PlatformSO, float> { }
