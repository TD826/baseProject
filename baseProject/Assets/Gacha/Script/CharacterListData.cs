using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(
    fileName = "CharacterListData",
    menuName = "ScriptableObject/CharacterListData",
    order = 1)]
// 名前や、レアリティの情報、初期パラメータ、初回入手時用のフラグをセットする
// 今回は初期パラメータに関してはHPと攻撃力のみをセットするという仕様設定
public class CharacterListData : ScriptableObject
{
    private const string path = "CharacterListData";
    // キャラクターのリスト
    public CharacterData1[] characterArray = new CharacterData1[10];
    private static CharacterListData characterListInstance;
    public static CharacterListData CharacterListInstance
    {
        get
        {
            if(characterListInstance == null)
            {
                characterListInstance = Resources.Load<CharacterListData>(path);
            }

            if(characterListInstance == null)
            {
                Debug.LogError("CharacterListDataのパス("+ path + ")が正しくありません");
            }

            return characterListInstance;
        }
    }
    // キャラクターのリストから指定したレアリティを抜き出すゲッター
    public IEnumerable<CharacterData1> GetCharacterList(CharacterData1.RarityOfCharacter rarity)
    {   // ※System.LinqのWhere(通常のListにWhereはない)
        return characterArray.Where(x => x.characterRarity == rarity);
    }

 }


[System.Serializable]
public class CharacterData1 /*: ScriptableObject*/
{
    
    public enum RarityOfCharacter
    {
        R_RARE,
        SR_RARE,
        SSR_RARE
    }
    // キャラクターの名前
    public string characterName; 
    // キャラクターのレアリティ
    public RarityOfCharacter characterRarity;
    // キャラクターの当選確率
    public float winningProbability;
    // 初期HP
    public int firstHealth;
    // 初期攻撃力
    public int firstAttack;
    // 入手フラグプロパティ
    public bool obtainFlg;


    // 以下ゲッターとセッター
    public string GetCharacterName()=>characterName;
    public RarityOfCharacter GetCharacter()=>characterRarity;
    public float GetWinningProbability()=>winningProbability;
    public int GetFirstHealth()=>firstHealth;
    public int GetFirstAttack()=>firstAttack;
    public bool GetObtainFlg()=>obtainFlg;
    public void SetObtainFlg(bool flg)=>obtainFlg = flg;
}
