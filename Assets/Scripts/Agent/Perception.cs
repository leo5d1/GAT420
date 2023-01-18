using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
	[Range(1, 40)] public float distance = 1;
	[Range(1, 180)] public float maxAngle = 45;
	public string tagName = "";

	public abstract GameObject[] GetGameObjects();

	public void SortByDistance(List<GameObject> gameObjects)
	{
		gameObjects.Sort(CompareDistance);
	}

	public int CompareDistance(GameObject a, GameObject b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
}
