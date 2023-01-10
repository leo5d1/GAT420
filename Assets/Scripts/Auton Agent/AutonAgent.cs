using System.Collections;
using System.Collections.Generic;
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
    }
}
