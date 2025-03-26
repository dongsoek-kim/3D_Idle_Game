using UnityEngine;
using System.Numerics;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("InGame")]
    public int stage;

    [Header("player")]
    public Party party;
    public int attckPowerStack;
    public int HealthStack;
    public int attckUpgradePriceBase;
    public int HealthUpgradePriceBase;
    private BigInteger coin;


    BigInteger attckUpgradePrice;
    BigInteger HealthUpgradePrice;
    BigInteger prevAttck = 0;
    BigInteger currAttck = 1;
    BigInteger prevHealth = 0;
    BigInteger currHealth = 1;

    [Header("Dungeonscene")]
    public DungeonController dungeonController;
    public UIManager uIManager;

    public int Stage { get; set; }

    private static GameManager instance;
    public GameManager gameManager;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManagerObj = new GameObject("GameManager");
                instance = gameManagerObj.AddComponent<GameManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameManager = GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void Start()
    {
        attckUpgradePriceBase = 10;
        HealthUpgradePriceBase = 10;
        attckUpgradePrice = 10;
        HealthUpgradePrice= 10;

    }
    public void Init()
    {
        Debug.Log("게임 재시작");
        Time.timeScale = 1f;
        uIManager.Init();
    }
    public void PartyDefeat()
    {   
        foreach(Player member in party.partyMembers)
        {
            if(member!=null && !member.IsDead)
            {
                break;
            }
        }
        StartCoroutine(StopGameAfterDelay(2f));
    }
    public BigInteger Coin
    {
        get => coin;
        set
        {
            coin = value;

            uIManager.inGameUI.UPdateCoin(coin);
        }
    }
    public void UsedCoin(BigInteger bigInteger)
    {
        Coin -= bigInteger;
    }

    public void OnAttackPowerPlus()
    {
        if (coin >= attckUpgradePrice)
        {
            UsedCoin(attckUpgradePrice);
            attckPowerStack += 1;
            BigInteger next = prevAttck + currAttck;
            prevAttck = currAttck;
            currAttck = next;
            attckUpgradePrice = currAttck * (BigInteger)attckUpgradePriceBase * attckPowerStack;
            uIManager.inGameUI.UpGradeAttak(currAttck, attckUpgradePrice);
            foreach (Player mamber in DungeonController.Instance.party.partyMembers)
            {
                if (mamber != null)
                {
                    mamber.UpgradeAttackPower(currAttck);
                }
            }
        }
        else
        {
            Debug.Log(AlphabetNumberFormatter.FormatNumber(Coin));
        }
    }
    public void OnHealthPlus()
    {
        if (coin >= HealthUpgradePrice)
        {
            UsedCoin(HealthUpgradePrice);
            HealthStack += 1;
            BigInteger next = prevHealth + currHealth;
            prevHealth = currHealth;
            currHealth = next;
            HealthUpgradePrice = currHealth * (BigInteger)HealthUpgradePriceBase * HealthStack;
            uIManager.inGameUI.UpGradeHealth(currHealth, HealthUpgradePrice);
            foreach (Player mamber in DungeonController.Instance.party.partyMembers)
            {
                if (mamber != null)
                {
                    mamber.UpgradeHealth(currHealth);
                }
            }
        }
        else
        {
            Debug.Log(AlphabetNumberFormatter.FormatNumber(Coin));
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DungeonScenes")
        {
            if (dungeonController == null)
            {
                dungeonController = FindObjectOfType<DungeonController>();
            }
            if (uIManager == null)
            {
                uIManager = FindObjectOfType<UIManager>();
            }
            if(party ==null)
            {
                party = FindObjectOfType<Party>();
            }
        }
        Init();
    }
    private IEnumerator StopGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uIManager.ChangeState(UIState.GameEnd);
        Time.timeScale = 0f;
    }
}


