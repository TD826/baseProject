using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterListManager : MonoBehaviour
{
    private const string path = "CharacterListData";
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
                Debug.LogError("CharacterListDataのパス(" + path + ")が正しくありません");
            }

            return characterListInstance;
        }
    }
    /// <summary>
    /// キャラクターのリストから指定したレアリティを抜き出すゲッター
    /// </summary>
    /// <param name="rarity">抜き出したいレアリティ</param>
    /// <returns>キャラクターのデータ</returns>
    public static IEnumerable<CharacterListData.CharacterData> GetCharacterList(CharacterListData.CharacterData.RarityOfCharacter rarity)
    {   // ※System.LinqのWhere(通常のListにWhereはない)
        return CharacterListInstance.characterArray.Where(x => x.characterRarity == rarity);
    }
}
