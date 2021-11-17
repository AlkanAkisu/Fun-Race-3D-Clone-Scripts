using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFinishResponse : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField, NaughtyAttributes.Required] TransformEvent onActorPassedFinish;
    [SerializeField] DanceMoves _danceMove = DanceMoves.Flair;
    [SerializeField] Animator _anim;

    #endregion

    #region Public Properties

    public Animator Anim => _anim;

    public DanceMoves DanceMove { get => _danceMove; set => _danceMove = value; }
    public bool IsFinished { get; private set; }
    public bool IsLost { get; private set; }
    
    #endregion 
    
    #region Dependencies
    
    
    #endregion 



    private void Awake()
    {
        IsFinished = false;
        IsLost = false;
    }
    public virtual void Finished(bool isLost)
    {

        if (!CanFinish())
            return;
        IsLost = isLost;
        IsFinished = true;
        GetComponent<MoveController>().Finished();

    }
    protected void Dance()
    {
        var dance = new ActorDance(Anim);

        dance.Play(DanceMove);
    }
    private void LostDanceMove()
    {
        DanceMove = DanceMoves.Lost;
    }
    protected void LostDance()
    {
        LostDanceMove();
        Dance();
    }
    protected void RandomDance()
    {
        RandomDanceMove();
        Dance();
    }

    private void RandomDanceMove()
    {
        Array values = Enum.GetValues(typeof(DanceMoves));
        System.Random random = new System.Random();
        DanceMove = (DanceMoves)values.GetValue(random.Next(values.Length - 1));
    }

    protected bool CanFinish() => !IsFinished;



    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {

    }



}

