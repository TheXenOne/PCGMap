using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Testing))]
public class TestingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Testing test = (Testing)target;

        if (GUILayout.Button("Build Object"))
        {
            test.ResetGenerator();
            test.UpdateTexture();
        }

        if (GUILayout.Button("Test Merge Sort"))
        {
            test.TestMergeSort();
        }
    }
}
