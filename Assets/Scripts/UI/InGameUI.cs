using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI attackStack;
    public TextMeshProUGUI attackVelue;
    public TextMeshProUGUI healthStack;
    public TextMeshProUGUI healthVelue;
    public TextMeshProUGUI coin;

    public void UPdateCoin(BigInteger bigInteger)
    {
        Debug.Log("UpdateCoin »£√‚µ ");

        coin.text=AlphabetNumberFormatter.FormatNumber(bigInteger);
    }
}
