using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ガチャの抽選を行うスクリプト
// レアリティ抽選⇒キャラ抽選
public class LotteryGacya : MonoBehaviour
{    
    [Header("Rの確率は100-SR確率+SSR確率")]
    
    [Tooltip("Rキャラの確率"),NonEditibleSerializeField] private float probabilityR = 0;
    [Tooltip("SRキャラの確率"),SerializeField] private float probabilitySR;
    [Tooltip("SSRキャラの確率"),SerializeField] private float probabilitySSR;
    // [SerializeField] private CharacterTable
    
    private List<CharacterData> acquiredList = new List<CharacterData>(); // 当たったキャラのリスト
    private const float MAX_PERCENT = 100;
    private CharacterListData characterListData;

    void Start()
    {
        // R確率の算出
        probabilityR = MAX_PERCENT - (probabilitySR + probabilitySSR);
    }

    /// <summary>
    /// ガチャの抽選をする関数
    /// </summary>
    /// <param name="lotteryNum">ガチャを回す回数</param>
    public void LotteryPlay(int lotteryNum)
    {
        CharacterData character;
        for(int i = 0; i< lotteryNum;i++){
            switch(LotteryRarity())
            {
                case CharacterData.RarityOfCharacter.R_RARE:
                // キャラクター情報をrandを使ってその情報を直でListへ引き渡す
                acquiredList.Add(LottertCharacter(CharacterData.RarityOfCharacter.R_RARE));
                break;
                
                case CharacterData.RarityOfCharacter.SR_RARE:
                acquiredList.Add(LottertCharacter(CharacterData.RarityOfCharacter.SR_RARE));
                break;

                case CharacterData.RarityOfCharacter.SSR_RARE:
                character = LottertCharacter(CharacterData.RarityOfCharacter.SSR_RARE);
                acquiredList.Add(character);
                break;
                
                default:
                break;
            }
        }
        // ↑が終わったと同時に次のアクションを行う(アニメーションやテキスト表示など)
        for(int i =0;i<acquiredList.Count;++i)
            Debug.Log(acquiredList[i].characterName);
    }

    /// <summary>
    /// レアリティの抽選を行う関数
    /// </summary>
    /// <returns>抽選結果のレアリティ</returns>
    private CharacterData.RarityOfCharacter LotteryRarity()
    {
        float rarityRand = Random.Range(0,MAX_PERCENT);
        // Rの減算
        rarityRand -= probabilityR;
        if(rarityRand <= 0)
        {
            return CharacterData.RarityOfCharacter.R_RARE;
        }
        // SRの減算
        rarityRand -= probabilitySR;
        if(rarityRand <= 0)
        {
            return CharacterData.RarityOfCharacter.SR_RARE;
        }
        // SSR(する必要なし)
        return CharacterData.RarityOfCharacter.SSR_RARE;
    }

    /// <summary>
    /// キャラクタの抽選を行う関数
    /// </summary>
    /// <param name="rarity">抽選するレアリティ</param>
    private CharacterData LottertCharacter(CharacterData.RarityOfCharacter rarity)
    {
        List<CharacterData> characterList = new List<CharacterData>();
        int acquiredNum = 0;
        switch(rarity)
        {
            // Rキャラ
            case CharacterData.RarityOfCharacter.R_RARE:
            // CharacterListDataから対応するレアリティのリストを取得して抽選
            characterList = CharacterListData.CharacterListInstance.GetCharacterRRareList();
            // randでキャラクターを特定
            acquiredNum = Random.Range(0,characterList.Count);
            // CharacterDataで返す(return)
            return characterList[acquiredNum];

            // SRキャラ
            case CharacterData.RarityOfCharacter.SR_RARE:
            characterList = CharacterListData.CharacterListInstance.GetCharacterSRRareList();
            acquiredNum = Random.Range(0,characterList.Count);
            return characterList[acquiredNum];
            
            // SSRキャラ
            case CharacterData.RarityOfCharacter.SSR_RARE:
            characterList = CharacterListData.CharacterListInstance.GetCharacterSSRRareList();
            acquiredNum = Random.Range(0,characterList.Count);
            return characterList[acquiredNum];
            
            default:
            Debug.LogError("対応していないレアリティが渡されました");
            return null;
        }
    }
}
