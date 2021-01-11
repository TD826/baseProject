using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaButton : MonoBehaviour
{
    [Tooltip("コイン消費量(1回分)"),SerializeField] private int LOTTERY_ONE;
    [Tooltip("課金コイン消費量(1回分)"),SerializeField] private int PAID_LOTTERY_ONE;
    [Tooltip("ガチャ回数"),SerializeField]private int GACHA_NUM = 10;   // ガチャ回数
    [Tooltip("コインが足りない時のメッセージ"),SerializeField] private GameObject errorMessageTextBox;
    [SerializeField] private LotteryGacha lotteryGacya;
     
    // コインガチャ(単発)
    public void NormalCoinOneGacha()
    {
        // コインの所持数チェック
        if(CoinData.Coin >= LOTTERY_ONE)
        {
            CoinData.Coin -= LOTTERY_ONE;
            // 抽選処理
            lotteryGacya.LotteryPlay(1);
        }
        else
        {   // エラーメッセージのテキストをアクティブ化
            errorMessageTextBox.SetActive(true);
        }
    }
    // 課金コインガチャ(単発)
    public void PaidCoinOneGacha()
    {
        // 課金コインの所持数チェック
        if(CoinData.PaidCoin >= PAID_LOTTERY_ONE/*課金コインの所持数チェック*/)
        {
            // 課金コインの減算処理
            CoinData.PaidCoin -= PAID_LOTTERY_ONE;
            // 抽選処理
            lotteryGacya.LotteryPlay(1);
        }
        else
        {   // エラーメッセージテキストをアクティブ化
            errorMessageTextBox.SetActive(true);

        }
    }

    // コインガチャ(10連)
    public void NormalCoinTenGacha()
    {
        // コインの所持数チェック
        if(CoinData.Coin >= LOTTERY_ONE * GACHA_NUM)
        {
            CoinData.Coin -= LOTTERY_ONE * GACHA_NUM;
            // 抽選処理
            lotteryGacya.LotteryPlay(GACHA_NUM);

        }
        else
        {   // エラーメッセージのテキストをアクティブ化
            errorMessageTextBox.SetActive(true);
            
        }
    }

    // 課金コインガチャ(10連)
    public void PaidCoinTenGacha()
    {
        // コインの所持数チェック
        if(CoinData.PaidCoin >= PAID_LOTTERY_ONE * GACHA_NUM/*課金コインの所持数チェック*/)
        {
            // 課金コインの減算処理
            CoinData.PaidCoin -= PAID_LOTTERY_ONE * GACHA_NUM;
            // 抽選処理
            lotteryGacya.LotteryPlay(GACHA_NUM);
        }
        else
        {   // エラーメッセージのテキストをアクティブ化
            errorMessageTextBox.SetActive(true);
        }
    }

    // エラーメッセージ
    public void ErrorMessageButtom()
    {
        gameObject.SetActive(false);
    }

    // エラーメッセージ用
    public void ErrorMessageExit()
    {
        // 非アクティブ化
        errorMessageTextBox.SetActive(false);
    }
}
