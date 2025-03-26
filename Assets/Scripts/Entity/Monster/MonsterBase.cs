using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Numerics;
// using UnityEditor.SceneManagement;

public class MonsterBase: MonoBehaviour
{
    public GameObject healthBarPrefab;     
    private GameObject healthBarInstance;
    Image healthBarImage;
    public BigInteger maxHealth;
    public BigInteger currentHealth;
    public MonsterData monsterData;
    public TextMeshProUGUI healthText;
    public Player target;
    public Canvas worldCanvas;
    public float atttackdelay;
    public Animator animator;
    public Action onDeath;
    public void Start()
    {
        maxHealth= (BigInteger)(monsterData.maxHealth) * BigInteger.Pow(100, GameManager.Instance.Stage);
        currentHealth = maxHealth;
        FindWorldCanvas();
        healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform);
        Image[] images = healthBarInstance.GetComponentsInChildren<Image>();
        healthBarImage = images.FirstOrDefault(img => img.gameObject != healthBarInstance);
        healthBarInstance.transform.SetParent(worldCanvas.transform);
        healthText = healthBarInstance.GetComponentInChildren<TextMeshProUGUI>();
        atttackdelay = monsterData.attackSpeed;
    }
    public void Update()
    {
        UpdateHealthBar();

        if (currentHealth >= 0)
        {
            if (target != null)
            {
                if (atttackdelay > Time.deltaTime)
                {
                    atttackdelay -= Time.deltaTime;
                }
                else
                {
                    Attack();
                    atttackdelay = monsterData.attackSpeed;
                }
            }
        }
    }
    

    public void Attack()
    {
        animator.SetTrigger("isAttack");
        target.Hit((BigInteger)(monsterData.damage)* BigInteger.Pow(10, GameManager.Instance.Stage));
        if (target.IsDead) target = null;
    }

    public  void Hit(BigInteger attackPower)
    {
        animator.SetTrigger("isHit");
        currentHealth -= attackPower;
        if (currentHealth <= 0)
        {
            currentHealth= 0;
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("isDead");
        onDeath?.Invoke();
        Destroy(this, 2);
        Destroy(healthBarInstance, 2);
    }

    /// <summary>
    /// 코인 드랍 테이블
    /// </summary>
    /// <returns></returns>
    public int Drop()
    {
        return monsterData.basecoin;
    }

    /// <summary>
    /// 머리위에 헬스바
    /// </summary>
    void UpdateHealthBar()
    {

        healthBarInstance.transform.position = transform.position + UnityEngine.Vector3.up * 4f;
        healthBarInstance.transform.rotation = transform.rotation;
        healthText.text = $"{AlphabetNumberFormatter.FormatNumber(currentHealth)}/{AlphabetNumberFormatter.FormatNumber(maxHealth)}";
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }

    /// <summary>
    /// 목표(플레이어)를 잡는 시스템
    /// </summary>
    /// <param name="partyMembers"></param>
    public void Gettarget(Player[] partyMembers)
    {
        int highestAggro = 0;
        for (int i = 0; i < partyMembers.Length; i++)
        {
            if (partyMembers[i].characterData.aggro > partyMembers[highestAggro].characterData.aggro)
            {
                highestAggro = i;
            }
        }
        target = partyMembers[highestAggro];
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
