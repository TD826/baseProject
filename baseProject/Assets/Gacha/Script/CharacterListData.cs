using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(
    fileName = "CharacterListData",
    menuName = "ScriptableObject/CharacterListData",
    order = 0)]
// 名前や、レアリティの情報、初期パラメータ、初回入手時用のフラグをセットする
// 今回は初期パラメータに関してはHPと攻撃力のみをセットするという仕様設定
public class CharacterListData : ScriptableObject
{
    private const string path = "CharacterListData";
    // キャラクターのリスト
    public List<CharacterData> characterList = new List<CharacterData>();

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
    public IEnumerable<CharacterData> GetCharacterList(CharacterData.RarityOfCharacter rarity)
    {   // ※LinqのWhere(通常のListにWhereはない)
        return characterList.Where(x => x.characterRarity == rarity);
    }
    
 }
