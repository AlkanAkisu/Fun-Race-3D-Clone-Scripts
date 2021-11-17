using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotColor : MonoBehaviour
{
    [SerializeField] List<Material> _colors;
    [SerializeField] Material _currentColor;
    [SerializeField] bool _isRandom = false;
    [SerializeField, NaughtyAttributes.ShowIf(nameof(_isRandom))] bool _isUpperLowerSeperated = false;
    [SerializeField] SkinnedMeshRenderer _meshRenderer;


    private void Awake()
    {
        if (_isRandom)
        {
            ChangeRandom();
        }
    }
    [NaughtyAttributes.Button]
    private void ChangeRandom()
    {
        if (_isUpperLowerSeperated)
        {
            ChangeColor(_colors.RandomElement(), _colors.RandomElement());
        }
        else
        {
            ChangeColor(_colors.RandomElement());
        }
    }

    private void OnValidate()
    {
        if (_currentColor)
            ChangeColor(_currentColor);
    }

    private void ChangeColor(Material upper, Material lower = null)
    {
        lower ??= upper;
        _meshRenderer.materials = new Material[] { lower, upper };
    }
}
