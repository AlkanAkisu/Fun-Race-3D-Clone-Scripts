using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceManager : MonoBehaviour
{
    public DanceMoves CurrentDance { get; private set; }
    [SerializeField] DanceMovesEvent onDanceMoveEquipped;
    [SerializeField] DanceStorage danceStorage;

    private void Awake()
    {
        CurrentDance = danceStorage.CurrentDanceMove;

    }

    private void Start()
    {
        onDanceMoveEquipped.Raise(CurrentDance);
    }


    private void DanceMoveEquipped(DanceMoves dance)
    {
        CurrentDance = dance;
        danceStorage.CurrentDanceMove = CurrentDance;
        danceStorage.Save();

    }


    private void OnEnable()
    {
        onDanceMoveEquipped.RegisterListener(DanceMoveEquipped);
    }
    private void OnDisable()
    {
        onDanceMoveEquipped.UnregisterListener(DanceMoveEquipped);

    }
}
