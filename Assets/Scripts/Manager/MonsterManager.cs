using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance;
    public static MonsterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("MonsterManager");
                    instance = go.AddComponent<MonsterManager>();
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
    public List<MonsterBase> monsters;
    public List<MonsterBase> Bossmonsters;


    public MonsterBase SpawnMonster(Transform monsterSpawn)
    {
        int randomIndex = Random.Range(0, monsters.Count);
        MonsterBase newMonster = Instantiate(monsters[randomIndex], monsterSpawn.position, monsterSpawn.rotation);
        return newMonster;
    }
    public MonsterBase SpawnBossMonster(Transform monsterSpawn)
    {
        int StageIndex = GameManager.Instance.Stage;
        MonsterBase newMonster = Instantiate(Bossmonsters[0], monsterSpawn.position, monsterSpawn.rotation);
        return newMonster;
    }
}
