using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class RaceStarter : MonoBehaviour
{
    [SerializeField] CustomEvent onRaceStarted;
    public bool IsRaceStarted { get; private set; }

    private void Awake()
    {
        IsRaceStarted = false;

    }
    private void Update()
    {
        if (KeyCode.Space.Up())
            StartRace(null);
    }

    private void StartRace(LeanFinger finger)
    {
        if (IsRaceStarted) return;
        if (finger == null)
        {
            onRaceStarted.Raise();
            IsRaceStarted = true;
        }
        else if (!finger.IsOverGui)
        {
            onRaceStarted.Raise();
            IsRaceStarted = true;
        }


    }
    private void OnEnable() => LeanTouch.OnFingerDown += StartRace;
    private void OnDisable() => LeanTouch.OnFingerDown -= StartRace;
}
