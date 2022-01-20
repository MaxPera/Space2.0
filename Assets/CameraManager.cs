using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]Transform planetTransform;
    [SerializeField]Planet planet;
    Transform cameraTransform;
    Vector3 offset;

    private void Start()
    {
        offset = new Vector3(planet.shapeSetting.planetRadius*1.5f,0, planet.shapeSetting.planetRadius*1.5f);
    }
    public void SwitchCamera()
    {
        cameraTransform.position = planetTransform.position + offset;
    }


}
