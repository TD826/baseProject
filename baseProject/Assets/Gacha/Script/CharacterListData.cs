using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "CharacterListData",
    menuName = "ScriptableObject/CharacterListData",
    order = 1)]
// 名前や、レアリティの情報、初期パラメータ、初回入手時用のフラグをセットする
// 今回は初期パラメータに関してはHPと攻撃力のみをセットするという仕様設定
// 今回リストで受け渡しているが、キャラ数の増加と共に受け渡すサイズが増えるのでどうしよう・・・
public class CharacterListData : ScriptableObject
{
    private const string path = "CharacterListData";
    // キャラクターのリスト
    public List<CharacterData> characterRRareList = new List<CharacterData>();      //Rキャラ
    public List<CharacterData> characterSRRareList = new List<CharacterData>();     // SRキャラ
    public List<CharacterData> characterSSRRareList = new List<CharacterData>();   // SSRキャラ

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

    public List<CharacterData> GetCharacterRRareList()
    {
        return characterRRareList;
    }    

    public List<CharacterData> GetCharacterSRRareList()
    {
        return characterSRRareList;
    }

    public List<CharacterData> GetCharacterSSRRareList()
    {
        return characterSSRRareList;
    }
 }
