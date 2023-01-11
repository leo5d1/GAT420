using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
	[Range(1, 40)] public float distance = 1;
	[Range(1, 180)] public float maxAngle = 45;
	public string tagName = "";

	public GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
		foreach (Collider collider in colliders) 
		{
			if (collider.gameObject == gameObject) continue;

			if (tagName == "" || collider.CompareTag(tagName))
			{
                // calculate angle from transform forward vector to direction of game object 
                Vector3 direction = (collider.transform.position - transform.position).normalized;

                float cos = Vector3.Dot(transform.forward, direction);
                float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

                if (angle <= maxAngle)
                {
                    result.Add(collider.gameObject);
                }
            }
            
        }
		result.Sort(CompareDistance);

		return result.ToArray();
	}

	public int CompareDistance(GameObject a, GameObject b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
}
