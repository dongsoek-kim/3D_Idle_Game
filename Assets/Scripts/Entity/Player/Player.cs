using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.UI;
#nullable enable

public class Player: MonoBehaviour
{
    public CharacterData characterData;

    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    Image healthBarImage;
    public float currentHealth;
    private Canvas worldCanvas;
    public Animator animator;
    public float atttackdelay;
    MonsterBase? target;

    public void Start()
    {
        currentHealth = characterData.maxHealth;
        FindWorldCanvas();
        animator = GetComponent<Animator>();

        healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform);
        Image[] images = healthBarInstance.GetComponentsInChildren<Image>();
        healthBarImage = images.FirstOrDefault(img => img.gameObject != healthBarInstance);
        healthBarInstance.transform.SetParent(worldCanvas.transform);

        atttackdelay = characterData.attackSpeed;
    }
    private void Update()
    {
        UpdateHealthBar();
        if (target != null)
        {
            if (atttackdelay > Time.deltaTime)
            {
                atttackdelay -= Time.deltaTime;
            }
            else
            {
                Attack();
                atttackdelay = characterData.attackSpeed;
            }
        }
    }

    public void UpdateHealthBar()
    {
        healthBarInstance.transform.position = transform.position + Vector3.up * 4f;
        healthBarInstance.transform.rotation = transform.rotation;
        float healthPercentage = currentHealth / characterData.maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }

    public void GetTarget(MonsterBase? monster)
    {
        target = monster;
        Debug.Log($"{characterData.name}target:{target?.monsterData.monsterName}");
    }
    public  void Attack()
    {
        target?.Hit(characterData.damage);
        animator.SetTrigger("isAttack");
    }
    public  void Hit(float damage)
    {
        animator.SetTrigger("isHit");
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Player Die");
    }
    public  void UseSkill()
    {
        Debug.Log("Using skill");
    }

    public void FindWorldCanvas()
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();  

        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                worldCanvas = canvas;  
                break;  
            }
        }
    }
}
