using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public GameObject prefab;
    [Header("CharacterData Stats")]
    public string CharacterName;
    public float aggro;
    public float maxHealth;
    public float attackSpeed;
    public float damage;
}
