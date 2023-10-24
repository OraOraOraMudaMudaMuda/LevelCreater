using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCreater : MonoBehaviour
{
    public static LevelCreater Instance { get; private set; }
    public Vector2Int mapGrid;
    public Vector2Int minGrid = new Vector2Int(50, 50);
    public Vector2Int maxGrid = new Vector2Int(100, 100);
    public int obstarclePercentage = 5;
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

        int xGrid = Random.Range(minGrid.x, maxGrid.x);
        int yGrid = Random.Range(minGrid.y, maxGrid.y);
        mapGrid = new Vector2Int(xGrid, yGrid);

        for (int i = -xGrid / 2; i < xGrid / 2; i++)
        {
            for (int j = -yGrid / 2; j < yGrid / 2; j++)
            {
                for (int k = 0; k < tileMap.Length; k++)
                {
                    var tileMapData = tileMap[k];
                    if (!tileMapData.isObstarcleType)
                        tileMapData.map.SetTile(new Vector3Int(i, j, 0), tileMapData.tiles[UnityEngine.Random.Range(0, tileMapData.tiles.Count)]);
                    else if (obstarclePercentage > Random.Range(0, 100) ||
                        ((i == -xGrid / 2 || i == (xGrid / 2) - 1) || (j == -yGrid / 2 || j == (yGrid / 2) - 1))) //set obstacle on edge
                    {
                        tileMapData.map.SetTile(new Vector3Int(i, j, 0), tileMapData.tiles[UnityEngine.Random.Range(0, tileMapData.tiles.Count)]);
                    }
                }
            }
        }
    }
}
