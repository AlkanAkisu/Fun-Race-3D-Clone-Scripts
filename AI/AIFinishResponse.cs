using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFinishResponse : ActorFinishResponse
{

    public override void Finished(bool isLost)
    {
        if (!CanFinish())
            return;
        base.Finished(isLost);
        if (!isLost)
            RandomDance();
        else
            LostDance();

    }




}
