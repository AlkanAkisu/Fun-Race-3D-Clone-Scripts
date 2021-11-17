using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceUI : UIBase
{




    [NaughtyAttributes.Button]
    public override void Open()
    {
        _system.gameObject.SetActive(true);


    }
    [NaughtyAttributes.Button]
    public override void Close()
    {
        _system.gameObject.SetActive(false);
    }



}
