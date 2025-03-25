using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize = 20;
    [SerializeField]private Queue<GameObject> coinPool = new Queue<GameObject>();
    public Transform coinParent;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coin.transform.SetParent(coinParent, false);
            coinPool.Enqueue(coin);
        }
    }

    public GameObject GetCoin()
    {
        if (coinPool.Count > 0)
        {
            GameObject coin = coinPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            Debug.LogWarning("Coin Pool is empty!");
            return null;
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }

    public void ReturnAllCoins()
    {
        foreach (GameObject coin in coinPool)
        {
            coin.SetActive(false);
        }
    }

    public void OnMonsterDeath(Transform spawnPoint)
    {
        Debug.Log("동전뿌리기");
        for (int i = 0; i < coinPool.Count; i++)  // 풀에 있는 동전 수만큼 반복
        {
            GameObject coin = GetCoin();  // 동전 풀에서 하나씩 꺼내기
            if (coin != null)
            {
                coin.SetActive(true);  // 동전 활성화
                coin.GetComponent<Coin>().CoinMove(spawnPoint, this);  // 동전 연출 실행
            }
        }
    }
}