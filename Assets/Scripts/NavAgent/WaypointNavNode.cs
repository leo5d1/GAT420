using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WaypointNavNode : NavNode
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            if (navAgent.targetNode == this && neighbors.Count > 0)
            {
                navAgent.targetNode = neighbors[Random.Range(0, neighbors.Count)];
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent navAgent))
        {
            if (navAgent.targetNode == this)
            {
                navAgent.targetNode = neighbors[Random.Range(0, neighbors.Count)];
            }
        }
    }
}
