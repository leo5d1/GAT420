using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 velocity { get; set; } = Vector3.zero;
    public Vector3 acceleration { get; set; } = Vector3.zero;
    [Range(1, 10)] public float maxSpeed = 5;

    public void ApplyForce(Vector3 force)
    {
        acceleration += force;
    }

    void LateUpdate()
    {
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        acceleration = Vector3.zero;
    }
}
