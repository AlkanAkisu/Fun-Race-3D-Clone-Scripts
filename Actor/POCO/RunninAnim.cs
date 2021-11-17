using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunninAnim
{
    // Start is called before the first frame update
    Animator _anim;
    private MoveController _controller;

    public RunninAnim(MoveController controller, Animator anim)
    {
        _controller = controller;
        _anim = anim;
        _anim.SetBool("Grounded", true);
    }


    public void Tick()
    {
        _anim.SetFloat("MoveSpeed", GetPlayerSpeed());
    }

    private float GetPlayerSpeed()
    {
        return _controller.Speed / _controller.MaxSpeed;
    }


}
