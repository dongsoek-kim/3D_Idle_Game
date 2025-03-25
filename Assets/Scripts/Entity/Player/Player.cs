using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
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
    public BigInteger currentHealth;
    public TextMeshProUGUI healthText;
    private Canvas worldCanvas;
    public Animator animator;
    public float atttackdelay;
    MonsterBase? target;

    private BigInteger attackPower;
    private BigInteger Maxhealth;
    public void Start()
    {
        if(attackPower==0 &&Maxhealth ==0)
        {
            attackPower = (BigInteger)characterData.baseAttackPower;
            Maxhealth = (BigInteger)characterData.baseHealth;
        }
        currentHealth = Maxhealth;
        FindWorldCanvas();
        animator = GetComponent<Animator>();

        healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform);
        Image[] images = healthBarInstance.GetComponentsInChildren<Image>();
        healthBarImage = images.FirstOrDefault(img => img.gameObject != healthBarInstance);
        healthBarInstance.transform.SetParent(worldCanvas.transform);
        healthText = healthBarInstance.GetComponentInChildren<TextMeshProUGUI>();

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
        healthBarInstance.transform.position = transform.position + UnityEngine.Vector3.up * 4f;
        healthBarInstance.transform.rotation = transform.rotation;
        healthText.text = $"{AlphabetNumberFormatter.FormatNumber(currentHealth)}/{AlphabetNumberFormatter.FormatNumber(Maxhealth)}";
        float healthPercentage = (float)currentHealth / (float)Maxhealth;
        healthBarImage.fillAmount = healthPercentage;
    }

    public void GetTarget(MonsterBase? monster)
    {
        target = monster;
        Debug.Log($"{characterData.name}target:{target?.monsterData.monsterName}");
    }
    public  void Attack()
    {
        target?.Hit(attackPower);
        animator.SetTrigger("isAttack");
    }
    public  void Hit(BigInteger attackPower)
    {
        animator.SetTrigger("isHit");
        currentHealth -= attackPower;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Player Die");
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

    public void UpgradeAttackPower(BigInteger _attackPower)
    {
        attackPower *= (1 + _attackPower);
        Debug.Log("AttackPower" + AlphabetNumberFormatter.FormatNumber(attackPower));
    }

    public void UpgradeHealth(BigInteger _health)
    {
        Maxhealth *= (1 + _health);
        currentHealth += Maxhealth / 2;
        if (currentHealth > Maxhealth) currentHealth = Maxhealth;
        Debug.Log("Health" + AlphabetNumberFormatter.FormatNumber(Maxhealth));
    }
}
