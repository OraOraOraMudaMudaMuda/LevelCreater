using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptbleLevel : ScriptableObject
{
    public string LevelName;
    public List<TileData> baseTileDatas;
    public List<TileData> decoTileDatas;
}

[System.Serializable]
public class TileData
{
    public Vector3Int Position;
    public int tileType;
    public int tileNum;
}
