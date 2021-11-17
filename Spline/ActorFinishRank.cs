using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Dreamteck.Splines;
using System;
using System.Linq;


public class ActorFinishRank : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField, ReadOnly] List<SplineFollower> splineFollowers;
    [SerializeField, ReadOnly] string[] actorNamesSorted;

    #endregion

    #region Private Fields
    SplineStorage _splineStorage;
    private Transform finishLine;

    #endregion

    #region Public Properties

    public Actor[] ActorsFinishRanked { get; private set; }

    #endregion

    #region Dependencies
    public SplineStorage SplineStorage => (_splineStorage = FindObjectOfType<SplineStorage>());

    #endregion



    private void Awake()
    {
        StartCoroutine(WaitUntilFinishInit());
        var actors = FindObjectsOfType<Actor>();
        splineFollowers = actors.Select(a => a.GetComponent<SplineFollower>()).ToList();

     
    }
    IEnumerator WaitUntilFinishInit()
    {
        while (true)
        {
            if (FindObjectOfType<FinishTrigger>())
                break;
            yield return null;
        }
        finishLine = FindObjectOfType<FinishTrigger>().transform;
        SortActors();
    }

    private void SortActors()
    {
        splineFollowers.Sort(Sort);

        ActorsFinishRanked = splineFollowers.Select(s => s.GetComponent<Actor>()).ToArray();

        actorNamesSorted = ActorsFinishRanked.Select(s => s.ActorName).ToArray();

        Invoke(nameof(SortActors), 0.5f);

    }

    private int Sort(SplineFollower x, SplineFollower y)
    {
        var xPercent = x.transform.GetComponent<SplineChanger>().GetTotalPercent();
        var yPercent = y.transform.GetComponent<SplineChanger>().GetTotalPercent();
        return -xPercent.CompareTo(yPercent);
    }



    private float DistanceFromFinish(Transform tr) => GetDistance(tr.position, finishLine.position);
    private float DistanceFromFinish(Vector3 vect) => GetDistance(vect, finishLine.position);
    private float GetDistance(Transform tr1, Transform tr2) => GetDistance(tr1.position, tr2.position);
    private float GetDistance(Vector3 v1, Vector3 v2) => (v1 - v2).sqrMagnitude;

}
