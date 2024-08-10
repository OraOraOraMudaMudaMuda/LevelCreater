using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Text;
using System.IO;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    LevelManager levelManager;
    SerializedProperty baseTileProp;
    SerializedProperty obstacleTileProp;

    public void OnEnable()
    {
        baseTileProp = serializedObject.FindProperty("baseTile");
        obstacleTileProp = serializedObject.FindProperty("obstacleTile");
        levelManager = (LevelManager)target;
    }

    public override void OnInspectorGUI()
    {
        //Map Options
        levelManager.minGrid = EditorGUILayout.Vector2IntField("Min Grid", levelManager.minGrid);
        levelManager.maxGrid = EditorGUILayout.Vector2IntField("Max Grid", levelManager.maxGrid);

        GUILayout.Space(5f);
        GUILayout.Label("Obstarcle Creater Percentage");
        levelManager.obstarclePercentage = EditorGUILayout.IntSlider(levelManager.obstarclePercentage, 0, 100);


        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(baseTileProp, true);
        EditorGUILayout.PropertyField(obstacleTileProp, true);

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Create Level", GUILayout.Width(200f)))
        {
            levelManager.CreateLevel();
        }
    }
}
