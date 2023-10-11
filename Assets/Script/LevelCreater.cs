using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCreater : MonoBehaviour
{
    public static LevelCreater Instance { get; private set; }
    public Vector2Int grid;
    public float obstarclePercentage;
    public string levelName;

    [System.Serializable]
    public struct TileMapValue
    {
        public string name;
        public Tilemap map;
        public List<Tile> tiles;
        public bool isObstarcleType;
    }

    public TileMapValue[] tileMap;

    public void Awake()
    {
        Instance = this;
    }

    public void CreateLevel()
    {
        for (int i = 0; i < tileMap.Length; i++)
            tileMap[i].map.ClearAllTiles();

        int xGrid = grid.x;
        int yGrid = grid.y;

        for (int i = -xGrid / 2; i < xGrid / 2; i++)
        {
            for (int j = -yGrid / 2; j < yGrid / 2; j++)
            {
                for (int k = 0; k < tileMap.Length; k++)
                {
                    var tileMapData = tileMap[k];
                    if (!tileMapData.isObstarcleType)
                        tileMapData.map.SetTile(new Vector3Int(i, j, 0), tileMapData.tiles[UnityEngine.Random.Range(0, tileMapData.tiles.Count)]);
                    else if (obstarclePercentage > Random.Range(0f, 1f) ||
                        ((i == -xGrid / 2 || i == (xGrid / 2) - 1) || (j == -yGrid / 2 || j == (yGrid / 2) - 1))) //모서리부분에 장애물 배치
                    {
                        tileMapData.map.SetTile(new Vector3Int(i, j, 0), tileMapData.tiles[UnityEngine.Random.Range(0, tileMapData.tiles.Count)]);
                    }
                }
            }
        }
    }
}
