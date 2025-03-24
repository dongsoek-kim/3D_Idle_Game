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

    public MonsterBase SpawnMonster(Transform monsterSpawn)
    {
        int randomIndex = Random.Range(0, monsters.Count);
        GameObject monsterObject = Instantiate(monsters[randomIndex].monsterData.prefab, transform.position, Quaternion.identity);
        MonsterBase newMonster = monsterObject.GetComponent<MonsterBase>();
        return newMonster;
    }
}
