using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public Vector2Int mapGrid;
    public Vector2Int minGrid = new Vector2Int(50, 50);
    public Vector2Int maxGrid = new Vector2Int(100, 100);
    public int obstarclePercentage = 5;
    public string levelName;

    [System.Serializable]
    public struct TileMapValue
    {
        public Tilemap map;
        public List<Tile> tiles;
    }

    public TileMapValue baseTile;
    public TileMapValue obstacleTile;

    public void Awake()
    {
        Instance = this;
    }

    public void CreateLevel()
    {
        baseTile.map.ClearAllTiles();
        obstacleTile.map.ClearAllTiles();

        int xGrid = Random.Range(minGrid.x, maxGrid.x);
        int yGrid = Random.Range(minGrid.y, maxGrid.y);
        mapGrid = new Vector2Int(xGrid, yGrid);

        for (int i = -xGrid / 2; i < xGrid / 2; i++)
        {
            for (int j = -yGrid / 2; j < yGrid / 2; j++)
            {
                baseTile.map.SetTile(new Vector3Int(i, j, 0), baseTile.tiles[UnityEngine.Random.Range(0, baseTile.tiles.Count)]);
                if (obstarclePercentage > Random.Range(0, 100) ||
                    ((i == -xGrid / 2 || i == (xGrid / 2) - 1) || (j == -yGrid / 2 || j == (yGrid / 2) - 1))) //set obstacle on edge)
                {
                    obstacleTile.map.SetTile(new Vector3Int(i, j, 0), obstacleTile.tiles[UnityEngine.Random.Range(0, obstacleTile.tiles.Count)]);
                }
            }
        }
    }




    public Vector3 GetWorldPositionFromCellPosition(Vector3Int cellPosition)
    {
        return baseTile.map.layoutGrid.GetCellCenterWorld(cellPosition);
    }

    public bool IsObstacleCell(Vector3Int cellPosition)
    {
        return obstacleTile.map.GetTile(cellPosition) != null;
    }
}
