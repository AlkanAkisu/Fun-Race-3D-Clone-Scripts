using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Touch;


[RequireComponent(typeof(MoveController))]
public class InputController : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] KeyCode _speedButton = KeyCode.Space;

    #endregion

    #region Private Fields
    MoveController _moveController;


    #endregion

    #region Dependencies
    public MoveController MoveController => (_moveController = GetComponent<MoveController>());


    #endregion


    private void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleSpeedInput;
        LeanTouch.OnFingerUp += HandleSpeedInput;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleSpeedInput;
        LeanTouch.OnFingerUp -= HandleSpeedInput;


    }
    private void Update()
    {
#if UNITY_EDITOR
        if (_speedButton.Up())
            MoveController.SlowDown();
        else if (_speedButton.Down())
            MoveController.SpeedUp();

        if (KeyCode.R.Up())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    void HandleSpeedInput(LeanFinger finger)
    {
        bool fingerDown, fingerUp;
        fingerDown = finger.Down;
        fingerUp = finger.Up;

        if (fingerUp)
            MoveController.SlowDown();
        else if (fingerDown)
            MoveController.SpeedUp();
    }

}
