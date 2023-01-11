using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(1, 10)] public float maxSpeed = 5;
    [Range(1, 10)] public float minSpeed = 1;
    [Range(1, 10)] public float maxForce = 5;

    public Vector3 velocity { get; set; } = Vector3.zero;
    public Vector3 acceleration { get; set; } = Vector3.zero;
    public Vector3 direction { get { return velocity.normalized; } } 

    public void ApplyForce(Vector3 force)
    {
        acceleration += force;
    }

    void LateUpdate()
    {
        velocity += acceleration * Time.deltaTime;
        velocity = Utilities.ClampMagnitude(velocity, minSpeed, maxSpeed);

        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        acceleration = Vector3.zero;
    }
}
