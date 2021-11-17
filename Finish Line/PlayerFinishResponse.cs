using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinishResponse : ActorFinishResponse
{
    [SerializeField] CustomEvent onPlayerLost;
    [SerializeField] float _waitBeforeDance = 0.3f;
    [SerializeField] DanceMovesEvent onDanceMoveEquipped;
    CurrencyManager _currencyManager;

    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    private void PlayerLost()
    {
        _currencyManager.PlayerFinished(false);
        LostDance();
    }

    public override void Finished(bool isLost)
    {
        if (IsFinished)
            return;


        base.Finished(isLost);

        StartCoroutine(IEFinished());
        _currencyManager.PlayerFinished(true);

    }
    IEnumerator IEFinished()
    {
        FindObjectOfType<CameraChanger>().ChangeToFront();
        yield return new WaitForSeconds(_waitBeforeDance);
        Dance();
        yield return new WaitForEndOfFrame();
    }



    protected override void OnEnable()
    {
        base.OnEnable();
        onPlayerLost.RegisterListener(PlayerLost);
        onDanceMoveEquipped.RegisterListener(SetDance);

    }


    protected override void OnDisable()
    {
        base.OnDisable();
        onPlayerLost.UnregisterListener(PlayerLost);
        onDanceMoveEquipped.UnregisterListener(SetDance);
    }
    private void SetDance(DanceMoves dance)
    {
        Debug.Log($"Dance Move set to {dance}");
        DanceMove = dance;
    }


}
