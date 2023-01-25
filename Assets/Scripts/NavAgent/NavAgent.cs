using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavAgent : Agent
{
    [SerializeField] NavPath navPath;
    [SerializeField] NavNode navPathStartNode;
    [SerializeField] NavNode navPathEndNode;

    public NavNode targetNode { get; set; }

    void Start()
    {
		if (navPath == null)
		{
			targetNode = NavNode.GetRandomNode();
		}
		else
		{
			navPath.startNode = (navPathStartNode != null) ? navPathStartNode : GetNearestNode();
			navPath.endNode = (navPathEndNode != null) ? navPathEndNode : NavNode.GetRandomNode();
			navPath.StartPath();
			targetNode = navPath.startNode;
		}
    }

    void Update()
    {
        if (targetNode != null)
        {
            movement.MoveTowards(targetNode.transform.position);
        }
    }

	public NavNode GetNextTarget(NavNode node)
	{
		if (navPath == null) return node.neighbors[Random.Range(0, node.neighbors.Count)];
		else return navPath.GetNextNode(node);
	}

	public NavNode GetNearestNode()
	{
		var nodes = NavNode.GetNodes();
		SortByDistance(nodes.ToList());

		return (nodes.Length == 0) ? null : nodes[0];
	}


	public void SortByDistance(List<NavNode> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(NavNode a, NavNode b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
}
