using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ガチャの抽選を行うスクリプト
// レアリティ抽選⇒キャラ抽選
public class LotteryGacha : MonoBehaviour
{    
    private const float MAX_PERCENT = 100;

    [Header("確率の合計がMAX_PARCENTになっていなければビルド時にエラーが出力されます")]
    [Tooltip("Rキャラの確率"),SerializeField] private float probabilityR = 80;
    [Tooltip("SRキャラの確率"),SerializeField] private float probabilitySR = 20;
    [Tooltip("SSRキャラの確率"),SerializeField] private float probabilitySSR = 1;
    
    private List<CharacterData> acquiredList = new List<CharacterData>(); // 当たったキャラのリスト

    void Start()
    {
        //　確率のエラーチェック
        if(MAX_PERCENT == probabilityR + probabilitySR + probabilitySSR)
        {
            Debug.LogError("確率の合計が100になっていません");
        }
    }

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
    private CharacterData LotteryRarity()
    {
        float rarityRand = Random.Range(0,MAX_PERCENT);
        IEnumerable<CharacterData> characterData;

        // SSRの減算
        rarityRand -= probabilitySSR;
        if(rarityRand <= 0)
        {
            // CharacterListDataから対応するレアリティのリストを取得して抽選
            characterData = CharacterListData.CharacterListInstance.GetCharacterList(CharacterData.RarityOfCharacter.SSR_RARE);
            return LotteryCharacter(characterData);
        }
        // SRの減算
        rarityRand -= probabilitySR;
        if(rarityRand <= 0)
        {
            // CharacterListDataから対応するレアリティのリストを取得して抽選
            characterData = CharacterListData.CharacterListInstance.GetCharacterList(CharacterData.RarityOfCharacter.SR_RARE);
            return LotteryCharacter(characterData);
        }
        // R(レアリティ抽選する必要なし)
        // CharacterListDataから対応するレアリティのリストを取得して抽選
        characterData = CharacterListData.CharacterListInstance.GetCharacterList(CharacterData.RarityOfCharacter.R_RARE);
        return LotteryCharacter(characterData);
    }

    /// <summary>
    /// キャラクターの抽選を行う関数
    /// </summary>
    /// <param name="characterDataList">特定のレアリティでまとめられたキャラクター情報のリスト</param>
    /// <returns>キャラクターの情報</returns>

    private CharacterData LotteryCharacter(IEnumerable<CharacterData> characterDataList)
    {
        List<CharacterData> characterList = new List<CharacterData>();
        int acquiredNum = 0;

        // IEnumerableをListに変換する処理
        characterList = characterDataList.ToList();
        // randでキャラクターを特定
        acquiredNum = Random.Range(0,characterList.Count);
        // CharacterDataで返す(return)
        return characterList[acquiredNum];
    }
    
}
