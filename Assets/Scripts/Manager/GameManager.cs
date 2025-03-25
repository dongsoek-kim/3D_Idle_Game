using UnityEngine;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public BigInteger Coin;
    

    private static GameManager instance;  // Singleton 인스턴스

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

    // 게임 매니저 로직을 여기에 추가
    public void Start()
    {
        Debug.Log(AlphabetNumberFormatter.FormatNumber(Coin));
        Debug.Log("게임 시작!");
    }

    public void EndGame()
    {
        Debug.Log("게임 종료!");
    }
}
