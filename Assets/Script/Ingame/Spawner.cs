using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnInfo[] spawnInfos;
    [SerializeField] SpawnInfo[] enemySpawnInfos;

    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject go;
        public float respawnTime = 3f;
        public float radius = 5f;
    }

    public void SpawnStart()
    {
        Action<Transform, Transform> enemyAction = (_x, _y) => {
            _x.GetComponent<Enemy>().AIStart(_y);
        };
        for(int i = 0; i < spawnInfos.Length; i++)
            StartCoroutine(Respawn(spawnInfos[i]));
        for (int i = 0; i < enemySpawnInfos.Length; i++)
            StartCoroutine(Respawn(enemySpawnInfos[i], enemyAction));
    }
    public void SpawnStop()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    IEnumerator Respawn(SpawnInfo _spawnInfo, Action<Transform, Transform> func = null)
    {
        var wfs = new WaitForSeconds(_spawnInfo.respawnTime);
        var player = GameManager.Instance.Player.transform;

        while (true)
        {
            var pos = (UnityEngine.Random.insideUnitCircle.normalized * _spawnInfo.radius) + (Vector2)player.position;
            var cloneObj = Instantiate(_spawnInfo.go, pos, Quaternion.identity, transform);
            func?.Invoke(cloneObj.transform, player);
            yield return wfs;
        }
    }
}
