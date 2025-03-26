using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
//using UnityEditor.Search;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public Party party;
    public MonsterBase monster;
    public MapManager mapManager;
    public CoinPool coinpool;
    private static DungeonController instance;
    public int Stage;

    public bool OnbossStage;
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
        Stage = GameManager.Instance.Stage;
        mapManager.mapSpawner.StartGame();
        mapManager.mapSpawner.DequeueMap();
        party.MoveParty(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint);
    }

    /// <summary>
    /// 방에 도착하면 전투하기위한 메서드
    /// 몬스터와 파티에게 타겟을 전달
    /// 타겟이 있으면 자동전투
    /// </summary>
    public void Fight()
    {
        monster = mapManager.mapSpawner.mapSpawnQueue.Peek().monster;
        monster.onDeath += MonsterDie;
        party.GetTarget(monster);
        monster.Gettarget(party.partyMembers);
        mapManager.mapSpawner.MapSpawn();
    }


    /// <summary>
    /// 몬스터가 사망하면 호출되는 메서드
    /// 코인을 얻고 다음포인트로 이동한다
    /// </summary>
    public void MonsterDie()
    {
        try
        {
            party.GetTarget(null);
            coinpool.OnMonsterDeath(monster.transform);
            GetDrop();
            GameManager.Instance.uIManager.inGameUI.FillBossGauge();
            if (mapManager.mapSpawner.mapSpawnQueue.Count != 0)
            {
                party.MoveParty(mapManager.mapSpawner.mapSpawnQueue.Peek().partyPoint);
            }
            else 
            { GameManager.Instance.StageClear(); }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception occurred: " + ex.Message);
        }
    }
    public void GetDrop()
    {
        GameManager.Instance.Coin+=((BigInteger)(monster.Drop() * (1 + Stage * 10000)));
        Debug.Log(GameManager.Instance.Coin);
    }
}
