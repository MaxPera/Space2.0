using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBody : GravityObject
{
    [HideInInspector]
    public Rigidbody rbody;

    [HideInInspector]
    public float mass;

    public float radius;
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public Vector3 currentVelocity;

    public ShapeSetting shapeSettings;
    public PlanetBody otherBody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();

        currentVelocity = initialVelocity;

        radius = shapeSettings.planetRadius;

        mass = surfaceGravity * radius * radius / 0.00006674f;
    }



    public void UpdateVelocity (PlanetBody otherBody, float timeStep)
    {
        otherBody = this.otherBody;
        float gConstant = 0.00006674f;

        float sqrDist = (otherBody.rbody.position - rbody.position).sqrMagnitude;
        Vector3 forceDirection = (otherBody.rbody.position - rbody.position).normalized;
        Vector3 force = forceDirection * gConstant * mass * otherBody.mass / sqrDist;
        Vector3 acceleration = force / mass;
        currentVelocity += acceleration * timeStep;
    }



    public void UpdatePosition(float timeStep)
    {
        rbody.position += currentVelocity * timeStep;
    }

    public Vector3 Position
    {
        get
        {
            return rbody.position;
        }
    }
}
