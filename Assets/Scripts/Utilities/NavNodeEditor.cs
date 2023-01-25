using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class NavNodeEditor : MonoBehaviour
{
	[SerializeField] GameObject navNodePrefab;
	[SerializeField] LayerMask layerMask;

	private Vector3 position = Vector3.zero;
	private Vector3 nodeCameraDirection = Vector3.zero;
	private Vector3 activeCameraDirection = Vector3.zero;
	private bool spawnable = false;
	private NavNode navNode = null;
	private NavNode activeNavNode = null;

	private void OnEnable()
	{
		if (!Application.isEditor)
		{
			//Destroy(this);
		}
		SceneView.duringSceneGui += OnScene;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;
	}

	void OnScene(SceneView scene)
	{
		Event e = Event.current;

		// get scene mouse position
		Vector3 mousePosition = e.mousePosition;
		mousePosition.y = scene.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
		mousePosition.x *= EditorGUIUtility.pixelsPerPoint;
				
		Ray ray = scene.camera.ScreenPointToRay(mousePosition);
		// check mouse over spawn/nav layer
		if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
		{
			position = hitInfo.point;

			if (hitInfo.collider.gameObject.TryGetComponent<NavNode>(out navNode))
			{
				if (activeNavNode == null)
				{
					Selection.activeGameObject = navNode.gameObject;
				}
				spawnable = false;
			}
			else spawnable = true;
		}
		else
		{
			navNode = null;
			spawnable = false;
		}

		// if spawnable and mouse pressed, create nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z)
		{
			if (spawnable && navNode == null && activeNavNode == null) Instantiate(navNodePrefab, position, Quaternion.identity, transform);
			if (navNode != null && activeNavNode == null) 
			{
				activeNavNode = navNode;
				navNode = null;
			}
		}
		// add connection to active nav node
		if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Z)
		{
			if (activeNavNode != null && navNode != null && activeNavNode != navNode) 
			{
				if (!activeNavNode.neighbors.Exists(n => n == navNode))
				{
					activeNavNode.neighbors.Add(navNode);
				}
			}
			activeNavNode = null;
		}

		// delete nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.X) 
		{
			if (navNode != null) 
			{
				DestroyImmediate(navNode.gameObject);
			}
		}

		activeCameraDirection = (activeNavNode != null) ? (scene.camera.transform.position - activeNavNode.transform.position).normalized : Vector3.zero;
		nodeCameraDirection = (navNode != null) ? (scene.camera.transform.position - navNode.transform.position).normalized : Vector3.zero;
	}

	private void OnDrawGizmos()
	{
		if (spawnable && navNode == null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(position, 1);
		}
		if (navNode != null && navNode != activeNavNode)
		{
			Gizmos.DrawIcon(navNode.transform.position + nodeCameraDirection, "nav_nodeB.png", true, Color.green);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(navNode.gameObject.transform.position, navNode.radius);
		}
		if (activeNavNode != null)
		{
			Gizmos.DrawIcon(activeNavNode.transform.position + activeCameraDirection, "nav_nodeB.png", true, Color.red);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(activeNavNode.gameObject.transform.position, activeNavNode.radius * 1.5f);
			Gizmos.DrawLine(activeNavNode.gameObject.transform.position, position);
		}

	}
}
