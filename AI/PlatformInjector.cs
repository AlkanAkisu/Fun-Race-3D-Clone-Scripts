using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInjector : MonoBehaviour
{
    [SerializeField] PlatformSO platform;
    private void OnTriggerEnter(Collider other)
    {
        var ai = other.transform.GetComponent<AIMoveController>();
        if (ai == null)
            return;

        ai.InjectPlatformSO(platform);
    }
}
