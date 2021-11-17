using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] float maxRotation;
    [SerializeField] private float duration, initialDelay;
    [SerializeField] private Ease upEase, downEase;
    [SerializeField] Direction direction = Direction.LeftToRight;
    private IEnumerator coroutine;
    private RotateMode rotateMode;

    public Vector3 EndValueLeft => new Vector3(maxRotation, transform.rotation.y, transform.rotation.z);
    public Vector3 EndValueRight => new Vector3(-maxRotation, transform.rotation.y, transform.rotation.z);
    public Vector3 InitialValue => new Vector3(0f, transform.rotation.y, transform.rotation.z);


    enum Direction
    {
        LeftToRight,
        RightToLeft
    }


    private void Awake()
    {
        var initial = transform.rotation;
        initial.x = 0;
        transform.rotation = initial;

        coroutine = Rotate();
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

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(initialDelay);
        var endValues = direction == Direction.LeftToRight ? new Vector3[] { EndValueLeft, EndValueRight } : new Vector3[] { EndValueRight, EndValueLeft };

        while (true)
        {
            var tween = transform.DOLocalRotate(endValues[0], duration, rotateMode)
            .SetEase(upEase);
            tween.Play();
            yield return tween.WaitForKill();

            tween = transform.DOLocalRotate(InitialValue, duration, rotateMode)
            .SetEase(downEase);
            tween.Play();
            yield return tween.WaitForKill();

            tween = transform.DOLocalRotate(endValues[1], duration, rotateMode)
            .SetEase(upEase);
            tween.Play();
            yield return tween.WaitForKill();

            tween = transform.DOLocalRotate(InitialValue, duration, rotateMode)
            .SetEase(downEase);
            tween.Play();
            yield return tween.WaitForKill();

        }
    }
}
