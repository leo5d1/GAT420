using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AutonAgent : Agent
{

	void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects) 
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * 0);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * 1);
		}
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
