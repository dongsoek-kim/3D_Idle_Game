using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform end;
    public Transform party;
    public Transform monsterSpawn;
    public MonsterBase monster;

    private void Start()
    {
        if (monsterSpawn != null)
        {
           Debug.Log("Monster Spawned");
            monster = MonsterManager.Instance.SpawnMonster(monsterSpawn);
        }
    }
}
