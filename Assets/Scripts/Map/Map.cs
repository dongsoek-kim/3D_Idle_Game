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
    public bool bossmap = false;
    private void Start()
    {
        if (monsterSpawnPoint != null)
        {
            if (bossmap)
            {
                monster = MonsterManager.Instance.SpawnBossMonster(monsterSpawnPoint);
            }
            else
            {
                monster = MonsterManager.Instance.SpawnMonster(monsterSpawnPoint);
            }
                if (monster != null)
            {
                monster.transform.SetParent(this.transform);
            }
        }
    }
}
