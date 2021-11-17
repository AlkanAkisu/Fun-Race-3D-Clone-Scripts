using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : UIBase
{
    [SerializeField] CustomEvent onSettingsOpened;
    [SerializeField] CustomEvent onSettingsClosed;
    [SerializeField] CustomEvent onCountdownBegin;
    [SerializeField] GameObject SettingsButton;
    private Dictionary<CustomEvent, Action> eventDict;

    private void Awake()
    {
        eventDict = Utils.EventActionDict(
            (onSettingsOpened, Close),
            (onSettingsClosed, Open),
            (onCountdownBegin, HideSettingsButton)
        );


    }
    private void OnEnable()
    {
        eventDict.RegisterListeners();
    }
    private void OnDisable()
    {
        eventDict.UnregisterListeners();

    }

    [NaughtyAttributes.Button]
    public override void Open()
    {
        _system.gameObject.SetActive(true);

    }
    [NaughtyAttributes.Button]
    public override void Close()
    {
        _system.gameObject.SetActive(false);
    }

    private void HideSettingsButton()
    {
        SettingsButton.SetActive(false);
    }





}
