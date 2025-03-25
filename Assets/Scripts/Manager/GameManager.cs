using UnityEngine;
using System.Numerics;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("player")]
    public int attckPowerStack;
    public int HealthStack;
    private BigInteger coin; 


    [Header("Dungeonscene")]
    public DungeonController dungeonController;
    public InGameUI ingameUI;

    public int Stage { get; set; }


    private static GameManager instance;
    public GameManager gameManager;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없다면 새로 생성
            if (instance == null)
            {
                GameObject gameManagerObj = new GameObject("GameManager");
                instance = gameManagerObj.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManagerObj);
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


    public BigInteger Coin
    {
        get => coin;
        set
        {
            coin = value;
            if (ingameUI == null)
            {
                Debug.LogError("ingameUI가 null입니다.");
            }
            else
            {
                ingameUI.UPdateCoin(coin);
            }
        }
    }
    public void UsedCoin(BigInteger bigInteger)
    {
        coin -= bigInteger;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DungeonScenes")
        {
            if (dungeonController == null)
            {
                dungeonController = FindObjectOfType<DungeonController>();
            }
            if (ingameUI == null)
            {
                ingameUI = FindObjectOfType<InGameUI>();
            }
        }
    }
}

