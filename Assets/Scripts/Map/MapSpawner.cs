using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapSpawner : MonoBehaviour
{
    [Header("MapPrefabs")]
    public Map startMap;
    public Map bossMap;
    public Map preMap;
    public Map[] mapPrefabs;
    [SerializeField] private Transform spawnPoint;

    public Queue<Map> mapSpawnQueue = new Queue<Map>();

    private System.Random random=new System.Random();

    private void Start()
    {
        random = new System.Random();
    }

    public void StartGame()
    {
        MapSpawn();
        MapSpawn();
        MapSpawn();
    }

    public void MapSpawn()
    {

        Map map;
        if (preMap == null)
        {
            map = startMap;
            spawnPoint.position = Vector3.zero;
            spawnPoint.rotation = Quaternion.identity;
        }
        else
        {
            map = mapPrefabs[GetRandomNumber()];
            spawnPoint.position = preMap.endPoint.position;
            spawnPoint.rotation = preMap.endPoint.rotation;

        }
        Map newMap = Instantiate(map, spawnPoint.position, spawnPoint.rotation);
        preMap = newMap;
        mapSpawnQueue.Enqueue(newMap);
        if (mapSpawnQueue.Count > 3)
        {
            Map mapToDestroy = mapSpawnQueue.Dequeue();
            StartCoroutine(DestroyAfterDelay(mapToDestroy, 5f));
        }

    }
    public IEnumerator DestroyAfterDelay(Map mapToDestroy,float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(mapToDestroy.gameObject);
    }
    public int GetRandomNumber()
    {
        return random.Next(0, 4);
    }
}
