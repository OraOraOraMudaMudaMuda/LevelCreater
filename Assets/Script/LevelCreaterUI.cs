//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.Tilemaps;
//using System;
//using System.Linq;
//using System.IO;
//using System.Text;

//public class LevelCreaterUI : MonoBehaviour
//{
//    public TMP_InputField xGridTMP;
//    public TMP_InputField yGridTMP;
//    public TMP_InputField obstarclePercentageTMP;
//    public TMP_InputField saveLevelNameTMP;


//    public List<Tile> tileBase;
//    public List<Tile> tileDeco;

//    public Tilemap tileMapBase;
//    public Tilemap tileMapDeco;

//    public Grid grid;

//    public GameObject[] startWithEnableButtons;
//    public GameObject[] startWithDisableButtons;
//    public void CreateLevel()
//    {
//        int xGrid = Convert.ToInt32(xGridTMP.text);
//        int yGrid = Convert.ToInt32(yGridTMP.text);
//        int obstarclePercentage = Convert.ToInt32(obstarclePercentageTMP.text);

//        tileMapBase.ClearAllTiles();
//        tileMapDeco.ClearAllTiles();

//        for (int i = -xGrid / 2; i < xGrid / 2; i++)
//        {
//            for (int j = -yGrid / 2; j < yGrid / 2; j++)
//            {
//                tileMapBase.SetTile(new Vector3Int(i, j, 0), tileBase[UnityEngine.Random.Range(0, tileBase.Count)]);

//                if (obstarclePercentage > UnityEngine.Random.Range(0, 100))
//                    tileMapDeco.SetTile(new Vector3Int(i, j, 0), tileDeco[UnityEngine.Random.Range(0, tileDeco.Count)]);
//            }
//        }
//    }

//    public void SaveLevel()
//    {
//        var newLevel = ScriptableObject.CreateInstance<ScriptbleLevel>();
//        newLevel.LevelName = saveLevelNameTMP.text;
//        newLevel.baseTiles = GetTilesFromMap(tileBase, tileMapBase, TileType.Base).ToList();
//        newLevel.decoTiles = GetTilesFromMap(tileDeco, tileMapDeco, TileType.Deco).ToList();

//        //json으로 변환 후 저장
//        var json = JsonUtility.ToJson(newLevel, true);
//        var sb = new StringBuilder();
//        sb.Append(Application.dataPath);
//        sb.Append("/");
//        sb.Append(newLevel.LevelName);
//        sb.Append(".json");
//        var file = File.CreateText(sb.ToString());
//        file.WriteLine(json);
//        file.Close();

//        IEnumerable <TileData> GetTilesFromMap(List<Tile> _tiles, Tilemap _map, TileType _type)
//        {
//            foreach (var pos in _map.cellBounds.allPositionsWithin)
//            {
//                if (_map.HasTile(pos))
//                {
//                    //var levelTile = _map.GetTile<Tile>(pos);
//                    var tile = _tiles.Find(_x => _x.sprite.Equals(_map.GetSprite(pos)));

//                    yield return new TileData()
//                    {
//                        Position = pos,
//                        tileType = _type,
//                        tileNum = _tiles.IndexOf(tile),
//                        //tile = levelTile,
//                    };
//                };

//            }
//        }
//    }

//    public void StartButton()
//    {
//        for (int i = 0; i < startWithEnableButtons.Length; i++)
//            startWithEnableButtons[i].SetActive(true);
//        for (int i = 0; i < startWithDisableButtons.Length; i++)
//            startWithDisableButtons[i].SetActive(false);
//        GameManager.Instance.GameStart();
//    }
//    public void StopButton()
//    {
//        for (int i = 0; i < startWithEnableButtons.Length; i++)
//            startWithEnableButtons[i].SetActive(false);
//        for (int i = 0; i < startWithDisableButtons.Length; i++)
//            startWithDisableButtons[i].SetActive(true);
//        GameManager.Instance.GameStop();
//    }

//}
