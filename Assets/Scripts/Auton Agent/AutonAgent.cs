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

        if (gameObjects.Length >= 1)
        {
            Vector3 direction = (gameObjects[0].transform.position - transform.position).normalized;
            movement.ApplyForce(direction * 2);
        }
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
