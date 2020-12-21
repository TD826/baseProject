using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    void Start()
    {
        coinText.text = "現在のコイン : " + CoinData.Coin;
    }
}
