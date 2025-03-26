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

    /// <summary>
    /// 몬스터 죽으면 사용
    /// 몬스터 위치에서 코인 등장
    /// </summary>
    /// <param name="spawnPoint"></param>
    public void OnMonsterDeath(Transform spawnPoint)
    {
        for (int i = 0; i < coinPool.Count; i++)  
        {
            GameObject coin = GetCoin(); 
            if (coin != null)
            {
                coin.SetActive(true);  
                coin.GetComponent<Coin>().CoinMove(spawnPoint, this); 
            }
        }
    }
}