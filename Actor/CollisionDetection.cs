using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

[RequireComponent(typeof(Collider), typeof(MoveController))]
public class CollisionDetection : MonoBehaviour
{
    [SerializeField] bool isAI;
    [SerializeField, NaughtyAttributes.HideIf(nameof(isAI))] CustomEvent onPlayerDied;
    MoveController _moveController;

    public MoveController MoveController => (_moveController = GetComponent<MoveController>());
    public SplineFollower Follower => MoveController.Follower;
    private void Awake()
    {
        isAI = GetComponent<Player>() == null;
    }
    private void OnValidate()
    {
        isAI = GetComponent<Player>() == null;
    }

    private void OnCollisionEnter(Collision other)
    {

        bool isObstacle = other.transform.GetComponent<Obstacle>() == null;
        if (isObstacle)
            return;

        if (!Follower.enabled)
            return;


        Follower.enabled = false;
        if (!isAI)
        {
            onPlayerDied?.Raise();
        }
        else
        {
            var aIDeathHandler = GetComponent<AIDeathHandler>();
            aIDeathHandler.ActorDied();
        }
    }
}
