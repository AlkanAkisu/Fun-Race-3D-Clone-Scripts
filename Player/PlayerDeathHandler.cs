using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Dreamteck.Splines;
using NaughtyAttributes;

[RequireComponent(typeof(MoveController))]
public class PlayerDeathHandler : DeathHandler
{
    #region Serialize Fields

    [SerializeField] CustomEvent onBackOnTrack;
    [SerializeField] CustomEvent onPlayerDied;

    #endregion





    public override void GoToCheckpoint()
    {
        base.GoToCheckpoint();
        var rotation = new Vector3(0f, 90f, 0f);
        var tween = transform.DOLocalMove(_checkpoint.position, secondsForTween);
        transform.DOLocalRotate(rotation, secondsForTween);
        tween.onComplete += CheckPointArrived;

    }

    protected override void CheckPointArrived()
    {
        base.CheckPointArrived();
        onBackOnTrack?.Raise();

    }



    public override void OnEnable()
    {
        base.OnEnable();
        onPlayerDied += ActorDied;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        onPlayerDied -= ActorDied;
    }
}
