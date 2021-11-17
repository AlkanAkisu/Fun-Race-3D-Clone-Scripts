using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathHandler : DeathHandler
{

    public override void GoToCheckpoint()
    {
        base.GoToCheckpoint();
        transform.position = _checkpoint.position;
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        base.CheckPointArrived();
    }

}
