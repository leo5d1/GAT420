using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutonAgentData", menuName = "AI/AutonAgentData")] 
public class AutonAgentData : ScriptableObject
{
	[Header("Wander")]
	[Range(0, 20)] public float wanderDistance = 4;
	[Range(0, 20)] public float wanderRadius = 4;
	[Range(0, 20)] public float wanderDisplacement = 4;
	[Header("Flocking")]
	[Range(0, 20)] public float separationRadius = 1;
	[Header("Weights")]
	[Range(0, 5)] public float seekWeight = 1;
	[Range(0, 5)] public float fleeWeight = 1;
	[Range(0, 5)] public float cohesionWeight = 1;
	[Range(0, 5)] public float separationWeight = 1;
	[Range(0, 5)] public float alignmentWeight = 1;
}
