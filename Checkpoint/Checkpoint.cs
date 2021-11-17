using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        var cpHandler = other.GetComponent<CheckpointHandler>();
        if (cpHandler == null)
            return;

        cpHandler.SetCheckpoint(transform);

    }
}
