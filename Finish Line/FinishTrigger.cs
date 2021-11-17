using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] float particleDelay = 0.8f;
    [SerializeField] GameObject[] particles;
    [SerializeField, NaughtyAttributes.Required] TransformEvent onActorPassedFinish;


    private void OnCollisionEnter(Collision other)
    {
        var actor = other.transform.GetComponent<ActorFinishResponse>();
        if (actor == null)
            return;

        onActorPassedFinish.Raise(other.transform);

        if (other.transform.GetComponent<Player>())
            Invoke(nameof(ActivateParticles), particleDelay);

    }





    private void ActivateParticles() => Array.ForEach<GameObject>(particles, (p) => p.SetActive(true));
}
