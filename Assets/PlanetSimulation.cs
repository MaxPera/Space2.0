using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSimulation : MonoBehaviour
{
    PlanetBody otherBody;
    PlanetBody planetBody;

    private void Awake()
    {
        planetBody = GetComponent<PlanetBody>();
        otherBody = planetBody.otherBody;
    }

    private void Update()
    {
        planetBody.UpdateVelocity(otherBody, Time.fixedDeltaTime * 10);
        planetBody.UpdatePosition(Time.fixedDeltaTime * 10);
    }
}
