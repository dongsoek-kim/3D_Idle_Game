using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Numerics;

public class MonsterBase: MonoBehaviour
{
    public GameObject healthBarPrefab;     
    private GameObject healthBarInstance;
    Image healthBarImage;
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
        currentHealth = (BigInteger)monsterData.maxHealth;
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
        target.Hit((BigInteger)monsterData.damage);
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

    public int Drop()
    {
        return monsterData.basecoin;
    }

    public  void UseSkill()
    {
        Debug.Log("Using skill");
    }

    void UpdateHealthBar()
    {

        healthBarInstance.transform.position = transform.position + UnityEngine.Vector3.up * 4f;
        healthBarInstance.transform.rotation = transform.rotation;
        healthText.text = $"{currentHealth}/{monsterData.maxHealth}";
        float healthPercentage = (float)currentHealth / monsterData.maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }

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
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();  // 씬에 있는 모든 Canvas를 가져옵니다.

        foreach (Canvas canvas in allCanvases)
        {
            // WorldSpace 모드인 Canvas를 찾습니다.
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                worldCanvas = canvas;  // WorldSpace Canvas를 설정
                break;  // WorldSpace Canvas가 발견되면 루프 종료
            }
        }
    }

}
