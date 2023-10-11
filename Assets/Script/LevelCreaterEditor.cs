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
        levelCreater.grid = EditorGUILayout.Vector2IntField("Grid", levelCreater.grid);

        GUILayout.Space(5f);
        GUILayout.Label("Obstarcle Creater Percentage");
        levelCreater.obstarclePercentage = EditorGUILayout.Slider(levelCreater.obstarclePercentage, 0f, 1f);


        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tilemapInfoProp, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Create Level", GUILayout.Width(200f)))
        {
            levelCreater.CreateLevel();
        }

        GUILayout.Space(10f);
        //Json Options
        //levelCreater.levelName = EditorGUILayout.TextField("File Name", levelCreater.levelName);
        if (GUILayout.Button("Save Json File", GUILayout.Width(200f)))
        {
            var path = EditorUtility.SaveFilePanel("Save Json", Application.dataPath, "LevelData", "json");
            if(path != "")
                SaveLevel(path);
        }

        EditorUtility.SetDirty(target);
        //EditorApplication.MarkSceneDirty();
    }

    public void SaveLevel(string _path)
    {
        var newLevel = CreateInstance<ScriptbleLevel>();
        newLevel.LevelName = levelCreater.levelName;

        if (newLevel.baseTileDatas == null)
            newLevel.baseTileDatas = new List<TileData>();

        if (newLevel.decoTileDatas == null)
            newLevel.decoTileDatas = new List<TileData>();

        newLevel.baseTileDatas.AddRange(GetTilesFromMap(levelCreater.tileMap[0].tiles, levelCreater.tileMap[0].map, 0).ToList());
        newLevel.decoTileDatas.AddRange(GetTilesFromMap(levelCreater.tileMap[1].tiles, levelCreater.tileMap[1].map, 0).ToList());

        //json으로 변환 후 저장
        var json = JsonUtility.ToJson(newLevel, true);
        File.WriteAllText(_path, json, Encoding.Default);

        IEnumerable<TileData> GetTilesFromMap(List<Tile> _tiles, Tilemap _map, int _type)
        {
            foreach (var pos in _map.cellBounds.allPositionsWithin)
            {
                if (_map.HasTile(pos))
                {
                    //var levelTile = _map.GetTile<Tile>(pos);
                    var tile = _tiles.Find(_x => _x.sprite.Equals(_map.GetSprite(pos)));

                    yield return new TileData()
                    {
                        Position = pos,
                        tileType = _type,
                        tileNum = _tiles.IndexOf(tile),
                    };
                };

            }
        }
    }
}
