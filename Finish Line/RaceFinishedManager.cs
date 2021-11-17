using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RaceFinishedManager : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] TransformEvent onActorPassedFinish;
    [SerializeField] CustomEvent onPlayerLost, onPlayerPassedFinish;
    [SerializeField] StringArrayEvent onWinnersAnnounced;
    [SerializeField] FloatReference secondsBeforeAfterMenu;
    [SerializeField] FloatReference secondsBeforeAfterMenuLost;



    #endregion

    #region Private Fields
    List<Actor> actorsPassed;
    List<Actor> actors;
    ActorFinishRank _actorFinishRank;
    int _numberOfActorsPassed = 0;
    private Dictionary<CustomEvent, Action> eventDict;


    private Actor _player;

    #endregion

    #region Public Properties    
    public bool IsPlayerPassed => actorsPassed.Contains(Player);


    #endregion

    #region Dependencies 
    public Actor Player => (_player = FindObjectOfType<Player>());
    public ActorFinishRank ActorFinish => (_actorFinishRank = GetComponent<ActorFinishRank>());
    #endregion



    private void Awake()
    {
        eventDict = Utils.EventActionDict(
          (onPlayerPassedFinish, OpenAfterGameMenu),
          (onPlayerLost, OpenAfterGameMenu)
        );


    }
    private void OnEnable()
    {
        onActorPassedFinish.RegisterListener(PassedFinishLine);
        eventDict.RegisterListeners();
    }
    private void OnDisable()
    {
        onActorPassedFinish.UnregisterListener(PassedFinishLine);
        eventDict.UnregisterListeners();
    }


    private void Start()
    {
        actors = new List<Actor>(FindObjectsOfType<Actor>());
        actorsPassed = new List<Actor>(actors.Capacity);

        _numberOfActorsPassed = 0;

    }


    public void PassedFinishLine(Transform tr)
    {
        Actor actor = tr.GetComponent<Actor>();

        if (!actorsPassed.Contains(actor))
        {

            bool isLost = AddActorList(actor);
            tr.GetComponent<ActorFinishResponse>().Finished(isLost);

            if (tr.GetComponent<Player>() != null && !isLost)
                onPlayerPassedFinish?.Raise();

        }

    }

    private string[] GiveNames()
    {
        // TODO give names based on distance reached
        // ;
        var actorNames = actorsPassed.Select(a => a.ActorName).ToList();

        foreach (var actor in ActorFinish.ActorsFinishRanked)
        {
            if (!actorsPassed.Contains(actor))
            {
                actorNames.Add(actor.ActorName);
            }
        }

        return actorNames.ToArray();
    }

    private bool AddActorList(Actor actor)
    {
        _numberOfActorsPassed++;
        actorsPassed.Add(actor);

        if (actor == Player)
            return false;

        bool isBotLost = _numberOfActorsPassed == actors.Capacity;
        bool isOtherThanLastFinished = _numberOfActorsPassed == actors.Capacity - 1;

        bool isPlayerWillBeLast = isOtherThanLastFinished && !IsPlayerPassed;

        if (isPlayerWillBeLast)
            onPlayerLost?.Raise();


        return isBotLost;

    }

    private void OpenAfterGameMenu() => StartCoroutine(IE_OpenAfterGameMenu());

    IEnumerator IE_OpenAfterGameMenu()
    {
        var menu = FindObjectOfType<AfterGameMenu>();

        var seconds = IsPlayerPassed ? secondsBeforeAfterMenu : secondsBeforeAfterMenuLost;
        yield return new WaitForSeconds(seconds);

        onWinnersAnnounced?.Raise(GiveNames());

    }



 
}


