using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

[RequireComponent(typeof(SplineFollower))]
public class SplineChanger : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] FloatEvent percent;


    #endregion

    #region Private Fields
    SplineFollower _follower;
    SplineStorage _splineStorage;
    int i = 1;

    #endregion

    #region Dependencies

    public SplineFollower Follower => (_follower = GetComponent<SplineFollower>());
    public SplineStorage SplineStorage => (_splineStorage = FindObjectOfType<SplineStorage>());

    #endregion




    private void Start()
    {
        if (Follower.spline == null)
            StartFromFirstSpline();

    }

    [NaughtyAttributes.Button]
    public void StartFromFirstSpline()
    {

        Follower.spline = SplineStorage.GetSpline(0);
    }

    public float GetTotalPercent()
    {
        float unscaled = (i - 1) + (float)Follower.result.percent;
        float scaled = unscaled / SplineStorage.SplineNumber;
        return scaled;
    }

    private void LateUpdate()
    {
        percent.Raise(GetTotalPercent());
    }


    private void endReached(double percent) => StartCoroutine(endSequence());

    IEnumerator endSequence()
    {
        if (SplineStorage == null)
            yield return null;

        Follower.spline = SplineStorage.GetSpline(i);
        if (Follower.spline != null)
        {
            i++;
            yield return new WaitForEndOfFrame();
            Follower.SetPercent(0);
        }
        else
        {
            // End of the path logic
        }
    }

    private void OnEnable() => Follower.onEndReached += endReached;

    private void OnDisable() => Follower.onEndReached -= endReached;


}
