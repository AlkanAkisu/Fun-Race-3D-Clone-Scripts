using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public abstract class DeathHandler : MonoBehaviour
{
    [SerializeField] protected Transform _checkpoint;
    [SerializeField] protected float secondsForTween;
    [SerializeField] protected FloatReference deathSeconds;

    #region Private Fields
    MoveController _moveController;
    Collider _collider;
    CheckpointHandler _cpHandler;

    #endregion


    #region Dependencies
    public Collider Collider => (_collider = GetComponent<Collider>());
    public SplineFollower Follower => MoveController.Follower;
    public MoveController MoveController => (_moveController = GetComponent<MoveController>());
    public CheckpointHandler CPHandler => (_cpHandler = GetComponent<CheckpointHandler>());
    #endregion

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnEnable()
    {

    }
    public virtual void OnDisable()
    {

    }

    public virtual void ActorDied()
    {
        _checkpoint = CPHandler.CurrentCheckpoint;
        Invoke(nameof(GoToCheckpoint), deathSeconds);
    }

    public virtual void GoToCheckpoint() {
        Collider.enabled = false;
    }

    protected virtual void CheckPointArrived()
    {
        Follower.enabled = true;

        Follower.SetPercent(_checkpoint.GetComponent<SplineFollower>().startPosition);

        // Open collider
        Collider.enabled = true;

        MoveController.ActorBackTrack();
    }

}