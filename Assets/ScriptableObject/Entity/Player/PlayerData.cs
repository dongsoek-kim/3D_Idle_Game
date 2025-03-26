using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("CharacterData Stats")]
    public string CharacterName;
    public float aggro;
    public float baseHealth;
    public float attackSpeed;
    public float baseAttackPower;
}
