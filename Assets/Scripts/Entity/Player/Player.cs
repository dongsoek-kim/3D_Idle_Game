using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
//using UnityEditor.Experimental.RestService;
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
    private BigInteger maxhealth;
    public BigInteger AttkacPower { get { return attackPower; } }
    public BigInteger Maxhealth { get { return maxhealth; } }
    private bool isDead;
    public bool IsDead { get { return isDead; } }
    public void Start()
    {
        if(attackPower==0 && maxhealth ==0)
        {
            attackPower = GameManager.Instance.CurplayerAttck;
            maxhealth = GameManager.Instance.CurplayerHealth;
            Debug.Log("È÷È÷ÃÊ±âÈ­");

            Debug.Log("Health" + AlphabetNumberFormatter.FormatNumber(maxhealth));
            Debug.Log("attackPower" + AlphabetNumberFormatter.FormatNumber(attackPower));
        }
        currentHealth = maxhealth;
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
        if (target != null&&!isDead)
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
        healthText.text = $"{AlphabetNumberFormatter.FormatNumber(currentHealth)}/{AlphabetNumberFormatter.FormatNumber(maxhealth)}";
        float healthPercentage = (float)currentHealth / (float)maxhealth;
        healthBarImage.fillAmount = healthPercentage;
    }

    public void GetTarget(MonsterBase? monster)
    {
        target = monster;
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
            currentHealth = 0;
            Die();
        }
    }
    public void Die()
    {
        animator.SetTrigger("isDead");
        isDead = true;
        GameManager.Instance.PartyDefeat();
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
        GameManager.Instance.CurplayerAttck = attackPower;
    }

    public void UpgradeHealth(BigInteger _health)
    {
        maxhealth *= (1 + _health);
        GameManager.Instance.CurplayerHealth = maxhealth;
        currentHealth += maxhealth / 2;
        if (currentHealth > maxhealth) currentHealth = maxhealth;
    }

    public void Init(BigInteger _attakcPower,BigInteger _health)
    {
        attackPower = _attakcPower;
        maxhealth = _health;
        currentHealth = maxhealth;
        Debug.Log("½ºÅÝ ÀÌ¾îÁü");
        Debug.Log("Health" + AlphabetNumberFormatter.FormatNumber(maxhealth));
        Debug.Log("attackPower" + AlphabetNumberFormatter.FormatNumber(attackPower));
    }
}
