using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LoadingScreen : UIBase
{

    [SerializeField] Image loadingBar;
    [SerializeField] float secondsToFill = 1f;
    [SerializeField] Ease ease;
    [SerializeField] CustomEvent onLoadingFinished;
    private Dictionary<CustomEvent, Action> eventDict;
    private Dictionary<CustomEvent<string>, Action<string>> stringDict;

    private void Awake()
    {
        LoadingStarted();
        eventDict = Utils.EventActionDict(
            (onLoadingFinished, LoadingFinished)
        );
    }

    [NaughtyAttributes.Button]
    public void LoadingStarted()
    {
        Open();
        DOTween.To(
            () => loadingBar.fillAmount,
            (d) => loadingBar.fillAmount = d,
            1f,
            secondsToFill
            ).
        SetEase(ease);
    }

    [NaughtyAttributes.Button]
    public void LoadingFinished()
    {
        Close();
    }


    public override void Open()
    {
        _system.gameObject.SetActive(true);

    }

    public override void Close()
    {
        _system.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        eventDict.RegisterListeners();
    }
    private void OnDisable()
    {
        eventDict.UnregisterListeners();

    }


}
