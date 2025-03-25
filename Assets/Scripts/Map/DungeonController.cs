using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public Party party;
    public MonsterBase monster;
    public MapManager mapManager;
    private static DungeonController instance;
    public static DungeonController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DungeonController>();

                if (instance == null)
                {
                    GameObject go = new GameObject("DungeonManager");
                    instance = go.AddComponent<DungeonController>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        mapManager.mapSpawner.StartGame();
        mapManager.mapSpawner.DequeueMap();
        party.MoveParty(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fight()
    {
        monster = mapManager.mapSpawner.mapSpawnQueue.Peek().monster;
        monster.onDeath += MonsterDie;
        party.GetTarget(monster);
        monster.Gettarget(party.partyMembers);
        mapManager.mapSpawner.MapSpawn();
    }

    public void MonsterDie()
    {
        try
        {
            Debug.Log("Monster Die");
            party.GetTarget(null);   
            //mapManager.mapSpawner.MapSpawn();
            Debug.Log(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint.position);
            party.MoveParty(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint);
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception occurred: " + ex.Message);
        }
    }
}
