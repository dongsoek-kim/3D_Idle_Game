using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI attackStack;
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI healthStack;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI coin;
    public Image BossGauge;
    private int currentHits = 0;
    private int maxHits = 5;

    public void Start()
    {
        BossGauge.fillAmount = 0;
    }
    public void UPdateCoin(BigInteger bigInteger)
    {
        coin.text = AlphabetNumberFormatter.FormatNumber(bigInteger);
    }
    public void UpGradeAttak(BigInteger attackPower, BigInteger price)
    {
        attackStack.text = "+ " + AlphabetNumberFormatter.FormatNumber(attackPower);
        attackValue.text = AlphabetNumberFormatter.FormatNumber(price) + "Coin";
    }
    public void UpGradeHealth(BigInteger health,BigInteger price)
    {
        healthStack.text = "+ " + AlphabetNumberFormatter.FormatNumber(health);
        healthValue.text = AlphabetNumberFormatter.FormatNumber(price) + "Coin";
    }

    public void FillBossGauge()
    {
        if (currentHits < maxHits)
        {
            currentHits++;
            BossGauge.fillAmount = (float)currentHits / maxHits;
        }
    }

    public void Init()
    {
        currentHits = 0;
    }
}
