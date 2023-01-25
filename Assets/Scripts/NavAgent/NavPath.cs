using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NavPath : MonoBehaviour
{
	List<NavNode> path = new List<NavNode>();

	public NavNode startNode { get; set; }
	public NavNode endNode { get; set; }

	public void StartPath()
	{
		GeneratePath();
	}

	public NavNode GetNextNode(NavNode navNode)
	{
		if (path.Count == 0) return null;

		int index = path.FindIndex(node => node == navNode);
		// check if noode index is at the end of the path
		if (index == path.Count - 1)
		{
			SetRandomEndNode();
			// generate new path
			GeneratePath();

			index = 0;
		}

		// get the next node using index + 1
		NavNode nextNode = path[index + 1];

		return nextNode;
	}

	private void SetRandomEndNode()
	{
		// set the start node to the current end node
		startNode = endNode;
		// find a new random end node that isn't the start node
		do
		{
			endNode = NavNode.GetRandomNode();
		} while (startNode == endNode);
	}

	private void GeneratePath()
	{
		NavNode.ResetNodes();
		Path.Dijkstra(startNode, endNode, ref path);
	}

	private void OnDrawGizmos()
	{
		foreach (NavNode node in path)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(node.gameObject.transform.position, node.radius);
		}
		if (startNode != null) Gizmos.DrawIcon(startNode.transform.position + Vector3.up, "nav_nodeB.png", true, Color.green);
		if (endNode != null) Gizmos.DrawIcon(endNode.transform.position + Vector3.up, "nav_nodeB.png", true, Color.red);
	}
}
