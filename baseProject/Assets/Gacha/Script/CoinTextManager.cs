using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class CoinTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    
    void Start()
    {
        coinText.text = TextManager.GetText("NowCoin") +  CoinData.Instance.Coin;
    }
}
