using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/MonsterData")]
public class MonsterData : ScriptableObject
{
    public GameObject prefab;
    [Header("MonsterData Stats")]
    public string monsterName; 
    public float maxHealth;         
    public float damage;         
    public float attackSpeed;
    public int basecoin;

}
