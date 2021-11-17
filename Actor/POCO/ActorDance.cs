using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDance
{
    private const string DanceLayerName = "Dance";
    Animator animator;
    public ActorDance(Animator animator)
    {
        this.animator = animator;
    }

    public void Play(DanceMoves move)
    {
        int index = animator.GetLayerIndex(DanceLayerName);
        string danceName = GetDanceMoveName(move);
        animator.Play(danceName, index);
    }

    public static string GetDanceMoveName(DanceMoves move)
    {
        return move switch
        {
            DanceMoves.Wave => "Wave",
            DanceMoves.Flair => "Flair",
            DanceMoves.Rumba => "Rumba",
            DanceMoves.Macarena => "Macarena",
            DanceMoves.HipHop => "HipHop",
            DanceMoves.GangnamStyle => "Gangnam Style",
            DanceMoves.Lost => "Sad Idle",
            DanceMoves.BootyDance => "Booty Dance",
            DanceMoves.BreakdanceFreeze => "Breakdance Freeze",
            DanceMoves.ChickenDance => "Chicken Dance",
            DanceMoves.DancingRunning => "Dancing Running",
            DanceMoves.SnakeDance => "Snake Dance",
            DanceMoves.Thriller => "Thriller3",
            _ => "Rumba",

        };
    }
}

public enum DanceMoves
{
    Wave,
    Flair,
    Rumba,
    Macarena,
    HipHop,
    GangnamStyle,
    BootyDance,
    BreakdanceFreeze,
    ChickenDance,
    DancingRunning,
    SnakeDance,
    Thriller,

    // Make Lost Last element
    Lost,
}
