using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(SplineFollower))]
public abstract class MoveController : MonoBehaviour
{
    #region Name

    #region Serialize Fields

    [SerializeField] private float _maxSpeed = 15f;
    [SerializeField] private float _slowDownDuration = 0.1f;
    [SerializeField] protected CustomEvent onRaceStart;

    #endregion

    #region Private Fields
    RunninAnim runningAnim;

    #endregion

    #region Public Properties
    public float MaxSpeed => _maxSpeed;
    public float Speed => Follower.followSpeed;

    #endregion

    #region Dependencies
    public SplineFollower Follower { get; set; }
    public bool FinishedLine { get; private set; }
    public bool CanMove { get; protected set; }
    public RunninAnim RunningAnim => runningAnim;


    #endregion




    protected virtual void Awake()
    {

        Follower = GetComponent<SplineFollower>();
        ResetSpeed();
        FinishedLine = false;
        DisableMovement();
        runningAnim = new RunninAnim(this, GetComponent<Animator>());

    }
    #endregion

    // Slow Down - Speed Up
    [NaughtyAttributes.Button]
    public void SlowDown()
    {
        if (!FinishedLine && CanMove)
            DOTween.To(GetSpeed, UpdateSpeed, 0, _slowDownDuration);
    }
    [NaughtyAttributes.Button]
    public void SpeedUp()
    {
        if (!FinishedLine && CanMove)
            DOTween.To(GetSpeed, UpdateSpeed, _maxSpeed, _slowDownDuration);
    }

    protected virtual void Update()
    {
        RunningAnim.Tick();
    }


    // Utils
    private float GetSpeed() => Follower.followSpeed;
    private void UpdateSpeed(float speed) => Follower.followSpeed = Mathf.Clamp(speed, 0, _maxSpeed);
    protected float ResetSpeed() => Follower.followSpeed = 0f;

    public void Finished()
    {
        SlowDown();
        FinishedLine = true;
    }

    public virtual void DisableMovement()
    {
        CanMove = false;
    }
    public virtual void EnableMovement()
    {
        CanMove = true;
    }
    virtual protected void OnEnable()
    {
        onRaceStart += EnableMovement;
    }
    virtual protected void OnDisable()
    {
        onRaceStart -= EnableMovement;
    }

    public virtual void ActorBackTrack()
    {
        EnableMovement();
    }


}
