using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] protected Transform wayPoints;
    [SerializeField] protected int currentWaypoint;

    [Header("Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] protected float stopDistance = 1.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.2f)
        {
            currentWaypoint++;
            if (currentWaypoint >= wayPoints.childCount)
            {
                currentWaypoint = 0;
            }

            agent.SetDestination(wayPoints.GetChild(currentWaypoint).position);
        }

        if (playerOnNavMesh())
        {
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDirection);

            agent.stoppingDistance = stopDistance;

            agent.destination = player.position;

        }
        else
        {
            agent.stoppingDistance = 0f;
            agent.SetDestination(wayPoints.GetChild(currentWaypoint).position);
        }
    }
    protected bool playerOnNavMesh()
    {
        NavMeshHit hit;
        float maxDistanceToNavMesh = 1.0f;
        bool isOnNavMesh = NavMesh.SamplePosition(player.position, out hit, maxDistanceToNavMesh, NavMesh.AllAreas);

        if (!isOnNavMesh)
        {
            return false;
        }

        return true;
    }
}
