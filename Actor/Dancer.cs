using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] DanceMovesEvent danceMovesEvent;

    protected void Dance(DanceMoves DanceMove)
    {
        var dance = new ActorDance(_anim);

        dance.Play(DanceMove);
    }

    private void OnEnable()
    {
        danceMovesEvent.RegisterListener(Dance);
    }
    private void OnDisable()
    {
        danceMovesEvent.UnregisterListener(Dance);

    }


}
