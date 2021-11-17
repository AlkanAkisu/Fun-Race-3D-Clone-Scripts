using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(Animator))]
public class PlayerMoveController : MoveController
{
    #region Serialize Fields


    [SerializeField] Transform _middlePoint;
    [SerializeField] CustomEvent onPlayerDied, onPlayerBackTrack;
    [SerializeField, NaughtyAttributes.Required] CustomEvent onPlayerPassedFinish;
    [SerializeField, NaughtyAttributes.Required] CustomEvent onPlayerLost;

    [SerializeField] bool isDebug;
    #endregion

    #region Private Fields
    private bool isHapticsSupported;

    private bool vibrateAtStart = false;// DO NOT Change


    #endregion

    #region Public Properties


    #endregion

    #region Dependencies


    #endregion

    private void Start()
    {
        InitVibrate();
        if (isDebug)
        {
            onRaceStart?.Raise();
        }
        isHapticsSupported = MMVibrationManager.HapticsSupported();



    }

    private void InitVibrate()
    {
        if (vibrateAtStart)
            Handheld.Vibrate();
    }



    // Middle Point
    private void UpdateMiddle() => _middlePoint.position = GetMiddlePoint();
    public Vector3 GetMiddlePoint()
    {
        if (Follower.enabled)
            return transform.position - transform.right * Follower.motion.offset.x;

        return transform.position;
    }

    private void PlayerDied()
    {
        base.ResetSpeed();
        base.DisableMovement();

        VibrateHaptic(HapticTypes.MediumImpact);

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        onPlayerDied += PlayerDied;
        onPlayerBackTrack += ActorBackTrack;
        onPlayerPassedFinish.RegisterListener(Finished);
        onPlayerLost.RegisterListener(PlayerLost);
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        onPlayerDied -= PlayerDied;
        onPlayerBackTrack -= ActorBackTrack;
        onPlayerPassedFinish.UnregisterListener(Finished);
        onPlayerLost.UnregisterListener(PlayerLost);
    }
    private void PlayerLost()
    {
        SlowDown();
        DisableMovement();

        VibrateHaptic(HapticTypes.Failure);
    }

    private void VibrateHaptic(HapticTypes haptic)
    {
        // Debug.Log($"Haptic entered");
        // MMVibrationManager.Vibrate();
        // return;
        if (isHapticsSupported)
        {
            MMVibrationManager.Haptic(haptic);
        }
        else
        {
            MMVibrationManager.Vibrate();
        }
    }
}
