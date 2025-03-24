using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase: MonoBehaviour
{
    public CharacterData characterData;

    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    public float currentHealth;

    private Canvas worldCanvas;

    MonsterBase target;

    public virtual void Start()
    {
        currentHealth = characterData.maxHealth;

        worldCanvas = FindObjectOfType<Canvas>();

        healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform);
        healthBarInstance.transform.position = transform.position + Vector3.up * 4f;
        healthBarInstance.transform.SetParent(worldCanvas.transform);
        //skillText.text = monsterData.skillName;
    }
    private void Update()
    {
        UpdateHealthBar();
        healthBarInstance.transform.position = transform.position + Vector3.up * 4f;
    }

    public void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / characterData.maxHealth;
        healthBarInstance.GetComponentInChildren<UnityEngine.UI.Image>().fillAmount = healthPercentage;
    }
    public virtual void Attack()
    {
        Debug.Log("Player Attack");
    }
    public virtual void Hit()
    {
        Debug.Log("Player Hit");
    }
    public virtual void Die()
    {
        Debug.Log("Player Die");
    }
    public virtual void UseSkill()
    {
        Debug.Log("Using skill");
    }

}
