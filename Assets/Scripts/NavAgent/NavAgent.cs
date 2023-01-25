using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : Agent
{
    [SerializeField] NavNode startNode;

    public NavNode targetNode { get; set; }

    void Start()
    {
        targetNode = (startNode != null) ? startNode : NavNode.GetRandomNode();
    }

    void Update()
    {
        if (targetNode != null)
        {
            movement.MoveTowards(targetNode.transform.position);
        }
    }
}
