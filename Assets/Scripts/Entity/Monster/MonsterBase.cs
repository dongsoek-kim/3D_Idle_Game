using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MonsterBase : MonoBehaviour
{
    public GameObject healthBarPrefab;     
    private GameObject healthBarInstance;
    public float currentHealth;

    public MonsterData monsterData;
    public TextMeshPro skillText;

    public int targetSlot;

    private Canvas worldCanvas;
    public virtual void Start()
    { 
        currentHealth = monsterData.maxHealth;

        worldCanvas = FindObjectOfType<Canvas>();

        healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform); 
        healthBarInstance.transform.position = transform.position + Vector3.up * 4f;
        healthBarInstance.transform.rotation = transform.rotation;
        healthBarInstance.transform.SetParent(worldCanvas.transform);
        //skillText.text = monsterData.skillName;
    }
    public virtual void Update()
    {
        UpdateHealthBar();
        healthBarInstance.transform.position = transform.position + Vector3.up * 4f;
    }
    
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Attack()
    {
        Debug.Log("MonsterData Attack");
    }

    public virtual void Hit()
    {
        Debug.Log("MonsterData Hit");
    }

    public virtual void Die()
    {
        Debug.Log("MonsterData Die");
    }

    public virtual void Drop()
    {
        Debug.Log("MonsterData Drop");
    }

    public virtual void UseSkill()
    {
        Debug.Log("Using skill");
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / monsterData.maxHealth;
        healthBarInstance.GetComponentInChildren<UnityEngine.UI.Image>().fillAmount = healthPercentage;
    }
}
