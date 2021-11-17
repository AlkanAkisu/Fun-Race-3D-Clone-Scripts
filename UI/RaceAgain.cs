using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class RaceAgain : MonoBehaviour
{

    [SerializeField, NaughtyAttributes.Required] CustomEvent onNextLevel;
    [SerializeField, NaughtyAttributes.Required] CustomEvent onReloadLevel;
    [SerializeField, NaughtyAttributes.Required] CustomEvent onPlayerLost;
    [SerializeField] Button RaceAgainButton;
    bool isNextRace;

    private void Awake()
    {

        RaceAgainButton.onClick.AddListener(RaceAgainPressed);
        isNextRace = true;
    }
    public void RaceAgainPressed()
    {
        var _event = isNextRace ? onNextLevel : onReloadLevel;
        _event.Raise();

    }
    private void OnEnable()
    {
        onPlayerLost.RegisterListener(PlayerLost);
    }

    private void OnDisable()
    {
        onPlayerLost.UnregisterListener(PlayerLost);
    }
    private void PlayerLost()
    {
        isNextRace = false;
    }
}