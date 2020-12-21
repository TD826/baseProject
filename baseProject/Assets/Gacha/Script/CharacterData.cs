using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "CharacterName",
    menuName = "ScriptableObject/CharacterData",
    order = 0)]
    
// ※ScriptableObjectで弄りたいパラメータをプロパティで生成すると弄れなくなる（インスペクター）に表示されないので注意
public class CharacterData : ScriptableObject
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
