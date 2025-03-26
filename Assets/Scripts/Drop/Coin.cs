using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public float moveDuration = 1f;  
    public float scaleDuration = 0.5f; 
    public float absorbDuration = 1f;

    private CoinPool coinPool;
    public void CoinMove(Transform spawnPosition, CoinPool pool)
    {
        coinPool = pool;
        transform.position = spawnPosition.position;

        Sequence coinSequence = DOTween.Sequence();

        coinSequence.Append(transform.DOMoveY(transform.position.y + 3f, moveDuration).SetEase(Ease.OutQuad))
                 .Join(transform.DOScale(Vector3.one * 2f, scaleDuration).SetEase(Ease.OutQuad))  
                 .Append(transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.InQuad))  
                 .OnComplete(() =>
                 {
                     // »ç¶óÁü
                     DOVirtual.DelayedCall(absorbDuration, () => {
                         ReturnToPool();  
                     });
                 });
    }

    void ReturnToPool()
    {
        coinPool.ReturnCoin(gameObject);  
    }

}

