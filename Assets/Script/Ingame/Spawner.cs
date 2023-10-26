using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public SpawnInfo[] itemSpawnInfos;
    [SerializeField] public SpawnInfo[] enemySpawnInfos;
    [SerializeField] public int enemyAmount;

    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject go;
        public int minAmount = 5;
        public int maxAmount = 20;
    }

    public void Initialize()
    {
        Transform[] childList = GetComponentsInChildren<Transform>();
        
        if (childList != null)
        {
            for (int i = 0; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
    }

    public void SpawnObject()
    {
        var levelManager = LevelManager.Instance;

        foreach (var itemInfo in itemSpawnInfos)
        {
            int amount = Random.Range(itemInfo.minAmount, itemInfo.maxAmount);
            
            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int xPos = Random.Range(-levelManager.mapGrid.x/2, levelManager.mapGrid.x/2);
                    int yPos = Random.Range(-levelManager.mapGrid.y/2, levelManager.mapGrid.y/2);
                    var cellPos = new Vector3Int(xPos, yPos);

                    if (!levelManager.IsObstacleCell(cellPos))
                    {
                        var worldPos = levelManager.GetWorldPositionFromCellPosition(cellPos);
                        Instantiate(itemInfo.go, worldPos, Quaternion.identity, transform);
                        break;
                    }
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        var levelManager = LevelManager.Instance;
        enemyAmount = 0;

        foreach (var enemyInfo in enemySpawnInfos)
        {
            int amount = Random.Range(enemyInfo.minAmount, enemyInfo.maxAmount);
            
            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int xPos = Random.Range(-levelManager.mapGrid.x / 2, levelManager.mapGrid.x / 2);
                    int yPos = Random.Range(-levelManager.mapGrid.y / 2, levelManager.mapGrid.y / 2);
                    var cellPos = new Vector3Int(xPos, yPos);

                    if (!levelManager.IsObstacleCell(cellPos))
                    {
                        var worldPos = levelManager.GetWorldPositionFromCellPosition(cellPos);
                        var enemy = Instantiate(enemyInfo.go, worldPos, Quaternion.identity, transform);
                        enemy.GetComponent<Enemy>().AIStart(GameManager.Instance.Player.transform);
                        enemyAmount++;
                        break;
                    }
                }
            }
        }
    }
}
