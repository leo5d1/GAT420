using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
	[Range(1, 40)] public float distance = 1;
	[Range(1, 180)] public float maxAngle = 45;

	public GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
		foreach (Collider collider in colliders) 
		{
			if (collider.gameObject == gameObject) continue;

			result.Add(collider.gameObject);
		}

		return result.ToArray();
	}
}
