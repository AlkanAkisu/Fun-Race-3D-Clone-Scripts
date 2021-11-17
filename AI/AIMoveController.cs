using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AIMoveController : MoveController
{
    #region Name


    ObstacleSafeTrigger _currentSafeTrigger;
    [SerializeField, Expandable] AIIntelligence intelligence;
    PlatformSO _currentPlatform;

    protected override void Awake()
    {
        base.Awake();
        _currentSafeTrigger = null;
    }
    public void NonDangerZone()
    {
        SpeedUp();
    }
    #endregion
    public void EnteredSafeStop(ObstacleSafeTrigger obsTrigger)
    {
        if (intelligence.DoesStopAtSafeStop())
        {
            SlowDown();
            obsTrigger.ListenTrigger(TriggerListener);
            _currentSafeTrigger = obsTrigger;
        }
    }


    private void TriggerListener()
    {
        if (intelligence.IsSuccessful(_currentPlatform))
        {
            SpeedUp();
        }
    }

    public override void EnableMovement()
    {
        base.EnableMovement();
        SpeedUp();
    }

    public void LeftSafeStop(ObstacleSafeTrigger obsTrigger)
    {
        obsTrigger.UnListenTrigger(TriggerListener);

    }

    public void InjectIntelligence(AIIntelligence intelligence)
    {
        this.intelligence = intelligence;
    }
    public void InjectPlatformSO(PlatformSO platformSO)
    {
        this._currentPlatform = platformSO;
    }




}
