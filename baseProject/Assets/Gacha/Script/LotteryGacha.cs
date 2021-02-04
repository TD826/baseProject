using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ガチャの抽選を行うスクリプト
// レアリティ抽選⇒キャラ抽選
public class LotteryGacha : MonoBehaviour
{    
    private const float MAX_PERCENT = 100.0f;

    [Header("確率の合計を100にしてください")]
    [Tooltip("Rキャラの確率"),SerializeField] private float probabilityR = 79.7f;
    [Tooltip("SRキャラの確率"),SerializeField] private float probabilitySR = 20.0f;
    [Tooltip("SSRキャラの確率"),SerializeField] private float probabilitySSR = 0.3f;
    
    private List<CharacterListData.CharacterData> acquiredList = new List<CharacterListData.CharacterData>(); // 当たったキャラのリスト

    /// <summary>
    /// ガチャの抽選をする関数
    /// </summary>
    /// <param name="lotteryNum">ガチャを回す回数</param>
    public void LotteryPlay(int lotteryNum)
    {
        for(int i = 0; i < lotteryNum; ++i)
        {
            // キャラクターの抽選が終わったものからListに追加していく
            acquiredList.Add(LotteryRarity());
        }
        // ↑が終わったと同時に次のアクションを行う(アニメーションやテキスト表示など)
        for(int i = 0; i < acquiredList.Count; ++i)
        {
            Debug.Log(acquiredList[i].characterName);
        }
    }

    /// <summary>
    /// レアリティの抽選とキャラの抽選（関数別）を行う関数
    /// </summary>
    /// <returns>抽選結果のレアリティ</returns>
    private CharacterListData.CharacterData LotteryRarity()
    {
        float rarityRand = Random.Range(0,MAX_PERCENT);
        IEnumerable<CharacterListData.CharacterData> characterData;
        
        // SSRの減算
        rarityRand -= probabilitySSR;
        if(rarityRand <= 0)
        {
            // CharacterListDataから対応するレアリティのリストを取得して抽選
            characterData = CharacterListManager.GetCharacterList(CharacterListData.CharacterData.RarityOfCharacter.SSR_RARE);
            return LotteryCharacter(characterData);
        }
        // SRの減算
        rarityRand -= probabilitySR;
        if(rarityRand <= 0)
        {
            // CharacterListDataから対応するレアリティのリストを取得して抽選
            characterData = CharacterListManager.GetCharacterList(CharacterListData.CharacterData.RarityOfCharacter.SR_RARE);
            return LotteryCharacter(characterData);
        }
        // R(レアリティ抽選する必要なし)
        // CharacterListDataから対応するレアリティのリストを取得して抽選
        characterData = CharacterListManager.GetCharacterList(CharacterListData.CharacterData.RarityOfCharacter.R_RARE);
        return LotteryCharacter(characterData);
    }

    /// <summary>
    /// キャラクターの抽選を行う関数
    /// </summary>
    /// <param name="characterDataList">特定のレアリティでまとめられたキャラクター情報のリスト</param>
    /// <returns>キャラクターの情報</returns>

    private CharacterListData.CharacterData LotteryCharacter(IEnumerable<CharacterListData.CharacterData> characterDataList)
    {
        List<CharacterListData.CharacterData> characterList = new List<CharacterListData.CharacterData>();
        int acquiredNum = 0;

        // IEnumerableをListに変換する処理
        characterList = characterDataList.ToList();
        // randでキャラクターを特定
        acquiredNum = Random.Range(0,characterList.Count);
        // CharacterDataで返す(return)
        return characterList[acquiredNum];
    }
    
}
