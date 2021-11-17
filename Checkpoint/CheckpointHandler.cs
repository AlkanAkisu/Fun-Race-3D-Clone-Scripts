using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    // Start is called before the first frame update

    Transform _currentCheckpoint;

    public Transform CurrentCheckpoint => _currentCheckpoint;
    public Checkpoint CurrentCheckpointScript => _currentCheckpoint.GetComponent<Checkpoint>();


    public void SetCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }


}
