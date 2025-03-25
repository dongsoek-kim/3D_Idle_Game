using UnityEngine;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public BigInteger Coin;
    

    private static GameManager instance;  // Singleton �ν��Ͻ�

    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���ٸ� ���� ����
            if (instance == null)
            {
                GameObject gameManagerObj = new GameObject("GameManager");
                instance = gameManagerObj.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManagerObj);
            }
            return instance;
        }
    }

    // ���� �Ŵ��� ������ ���⿡ �߰�
    public void Start()
    {
        Debug.Log(AlphabetNumberFormatter.FormatNumber(Coin));
        Debug.Log("���� ����!");
    }

    public void EndGame()
    {
        Debug.Log("���� ����!");
    }
}
