using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    [SerializeField] Material _seaMaterial;
    [SerializeField] Vector2 _speed;

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = _seaMaterial.mainTextureOffset;
        offset += _speed * Time.deltaTime;
        _seaMaterial.mainTextureOffset = offset;
    }
}
