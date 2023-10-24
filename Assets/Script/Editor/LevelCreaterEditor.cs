using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Text;
using System.IO;

[CustomEditor(typeof(LevelCreater))]
public class LevelCreaterEditor : Editor
{
    LevelCreater levelCreater;
    SerializedProperty tilemapInfoProp;
    Vector2Int test;

    public void OnEnable()
    {
        tilemapInfoProp = serializedObject.FindProperty("tileMap");        
        levelCreater = (LevelCreater)target;
    }

    public override void OnInspectorGUI()
    {
        //Map Options
        levelCreater.minGrid = EditorGUILayout.Vector2IntField("Min Grid", levelCreater.minGrid);
        levelCreater.maxGrid = EditorGUILayout.Vector2IntField("Max Grid", levelCreater.maxGrid);

        GUILayout.Space(5f);
        GUILayout.Label("Obstarcle Creater Percentage");
        levelCreater.obstarclePercentage = EditorGUILayout.IntSlider(levelCreater.obstarclePercentage, 0, 100);


        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tilemapInfoProp, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Create Level", GUILayout.Width(200f)))
        {
            levelCreater.CreateLevel();
        }
    }
}
