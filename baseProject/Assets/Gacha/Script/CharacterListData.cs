using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "CharacterListData",
    menuName = "ScriptableObject/CharacterListData",
    order = 0)]
// 名前や、レアリティの情報、初期パラメータ、初回入手時用のフラグをセットする
// 今回は初期パラメータに関してはHPと攻撃力のみをセットするという仕様
public class CharacterListData : ScriptableObject
{
    // キャラクターのリスト
    public CharacterData[] characterArray = new CharacterData[10];
    [Serializable]
    public class CharacterData
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
    }
}





