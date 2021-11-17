using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] CustomEvent _onPlayButtonPressed;
    [SerializeField] DOTweenAnimation[] _animations;
    [SerializeField] Button _playButton;
    [SerializeField, NaughtyAttributes.Scene] string _mainScene;

    public void PlayButtonPressed()
    {
        StartCoroutine(PlayButton());
    }

    IEnumerator PlayButton()
    {
        Array.ForEach<DOTweenAnimation>(
           _animations,
           (anim) => anim.DOPlay()
       );

        _playButton.interactable = false;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_mainScene);
    }

    private void OnEnable() => _onPlayButtonPressed += PlayButtonPressed;
    private void OnDisable() => _onPlayButtonPressed -= PlayButtonPressed;
}
