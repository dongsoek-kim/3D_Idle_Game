using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    public TextMeshProUGUI attackStack;
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI healthStack;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI coin;
    public Image BossGauge;
    public Button BossButton;
    private int currentHits = 0;
    private int maxHits = 5;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        currentHits = 0;
    }

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
        BossButton.gameObject.SetActive(true);
    }

    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }
}
