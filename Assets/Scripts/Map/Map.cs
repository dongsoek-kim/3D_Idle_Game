using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform endPoint;
    public Transform partyPoint;
    public Transform monsterSpawnPoint;
    public MonsterBase monster;

    private void Start()
    {
        if (monsterSpawnPoint != null)
        {
            Debug.Log("Monster Spawned");
            monster = MonsterManager.Instance.SpawnMonster(monsterSpawnPoint);
            if (monster != null)
            {
                monster.transform.SetParent(this.transform);
            }
        }
    }
}
