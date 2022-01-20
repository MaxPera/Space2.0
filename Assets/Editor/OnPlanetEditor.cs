using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(OnPlanet))]
public class OnPlanetEditor : Editor
{
    OnPlanet onPlanet;
    Editor shapeEditor;
    Editor colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                onPlanet.GenerateOnPlanet();
            }
        }

        if (GUILayout.Button("Generate onPlanet"))
        {
            onPlanet.GenerateOnPlanet();
        }

        DrawSettingsEditor(onPlanet.shapeSetting, onPlanet.OnShapeSettingsUpdate, ref onPlanet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(onPlanet.colorSettings, onPlanet.OnColorSettingsUpdated, ref onPlanet.colorSettingsFoldout, ref colorEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdate, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {


                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdate != null)
                        {
                            onSettingsUpdate();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        onPlanet = (OnPlanet)target;
    }
}