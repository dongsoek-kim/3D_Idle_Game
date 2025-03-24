using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/MonsterData")]
public class MonsterData : ScriptableObject
{
    public GameObject prefab;
    [Header("MonsterData Stats")]
    public string monsterName; 
    public int health;         
    public int damage;         
    public float attackSpeed;
    public string skillName;
}
