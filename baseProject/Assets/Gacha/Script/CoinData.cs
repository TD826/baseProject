using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinData : MonoBehaviour
{
    private static int coin;
    private static int paidCoin;
    // 無償コインのプロパティ
    public static int Coin
    {
        get
        {
            if(coin <= 0)
            {
                return coin = 999;
            }
            return coin;
        }
        set{coin += value;}
    }

    // 課金コインのプロパティ
    public static int PaidCoin
    {
        get
        {
            if(paidCoin <= 0)
            {
                return paidCoin = 999;
            }
            return paidCoin;
        }
        set{paidCoin += value;}
    }

}
