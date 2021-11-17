using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PunchWall : MonoBehaviour
{
    [SerializeField] Transform initial, destination;
    [SerializeField] private float duration, initialDelay;
    [SerializeField] private Ease ease;
    private IEnumerator coroutine;


    private void Awake()
    {
        transform.position = initial.position;
        coroutine = Movement();
        StartCoroutine(coroutine);

    }
    private void OnValidate()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            var tween = transform.DOMove(destination.position, duration)
            .SetEase(ease);
            tween.Play();
            yield return tween.WaitForKill();

            tween = transform.DOMove(initial.position, duration)
            .SetEase(ease);
            tween.Play();

            yield return tween.WaitForKill();
        }
    }
}
