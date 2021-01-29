using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinData : SingletonMonoBehaviour<CoinData>
{
    private int coin;
    private static int paidCoin;
    // 無償コインのプロパティ
    public int Coin
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

    public void Awake(){
        if(this != Instance){
            Destroy(this);
            return;
        }
    }

}
