using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public ShapeSetting shapeSetting;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter meshFilters;
    MeshCollider meshCollider;
    MapFace mapFace;



    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSetting);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null)
        {
            meshFilters = new MeshFilter();
        }
        

        if (meshCollider == null)
        {
            meshCollider = new MeshCollider();
        }

        Vector3 directions = Vector3.up;


            if (meshFilters == null)
            {
                GameObject meshObj = new GameObject("mesh");

                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();


                meshFilters = meshObj.AddComponent<MeshFilter>();
                meshFilters.sharedMesh = new Mesh();



                meshCollider = meshObj.AddComponent<MeshCollider>();

                meshCollider.sharedMesh = meshFilters.sharedMesh;

            }
            meshFilters.GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;


            mapFace = new MapFace(shapeGenerator, meshFilters.sharedMesh, meshCollider, resolution, directions);
        
    }

    private void Awake()
    {
        GenerateOnPlanet();

    }



    public void GenerateOnPlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColor();
    }
    public void OnShapeSettingsUpdate()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }
    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColor();
        }
    }

    void GenerateMesh()
    {

            mapFace.ConstructMesh();


        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColor()
    {
        colorGenerator.UpdateColours();

    }
}

