using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using NaughtyAttributes;


public class SplineStorage : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField, ReadOnly] SplineComputer[] _splines;

    public int SplineNumber => _splines.Length;



    #endregion

    #region Private Fields


    #endregion

    #region Public Properties


    #endregion

    #region Dependencies


    #endregion



    public void AddSpline(SplineComputer spline, int i)
    {
        if (i >= _splines.Length)
            Debug.LogError("Index is larger than length");
        else
            _splines[i] = spline;

        if (i == _splines.Length - 1)
            SplinesFinished();
    }

    public SplineComputer GetSpline(int i)
    {
        if (i >= _splines.Length)
        {
            return null;
        }

        return _splines[i];
    }

    private void SplinesFinished()
    {

    }
    public int GetIndex(SplineComputer spline)
    {
        foreach (var (i, sp) in _splines.Enumerate())
        {
            if (sp == spline)
                return i;
        }
        return -1;
    }


    public void InitSplines(int N) => _splines = new SplineComputer[N];
}
