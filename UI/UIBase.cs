using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] protected RectTransform _system;
    public abstract void Open();

    public abstract void Close();
}
