using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    
    void Start()
    {
        coinText.text = "現在のコイン : " + CoinData.Instance.Coin;
    }
}
