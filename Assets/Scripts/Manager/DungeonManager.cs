using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Party party;
    public MonsterBase monster;
    public MapManager mapManager;

    void Start()
    {
        mapManager.mapSpawner.StartGame();
        mapManager.mapSpawner.MapSpawn();
        party.MoveParty(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fight()
    {
        monster = mapManager.mapSpawner.mapSpawnQueue.Peek().monster;
        Debug.Log(monster);
        party.GetTarget(monster);
    }
}
