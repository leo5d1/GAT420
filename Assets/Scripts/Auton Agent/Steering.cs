using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering
{
	public static Vector3 Seek (Agent agent, GameObject target)
	{
		Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));

		return force;
	}

	public static Vector3 Flee(Agent agent, GameObject target)
	{
		Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));

		return force;
	}

	public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
	{
		Vector3 desired = direction.normalized * agent.movement.maxSpeed;
		Vector3 steer = desired - agent.movement.velocity;
		Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

		return force;
	}
}
