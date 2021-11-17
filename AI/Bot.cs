using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Actor
{

    private void OnEnable()
    {
        GenerateRandomName();
    }
    [NaughtyAttributes.Button]
    private void GenerateRandomName()
    {
        ActorName = NameGenerator.GetRandomName();
    }
}
