using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AutonAgent : Agent
{
    public Perception flockPerception;
    public ObstacleAvoidance obstacleAvoidance;
    public AutonAgentData data;

    public float wanderAngle { get; set; } = 0;

    void Update()
    {
        var gameObjects = perception.GetGameObjects();

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
		}

        gameObjects = flockPerception.GetGameObjects();
        if(gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * data.alignmentWeight);
        }

		// obstacle avoidance 
		if (obstacleAvoidance.IsObstacleInFront())
		{
			Vector3 direction = obstacleAvoidance.GetOpenDirection();
			movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
		}

        // wander
		if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        Vector3 position = transform.position;
        position = Utilities.Wrap(position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
        position.y = 0;
        transform.position = position;
    }
}
