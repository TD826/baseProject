using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ターン制のゲームに使用するスクリプト
/// </summary>
public enum GameState
{
    KeyInput,   // キー入力
    PlayerTurn, // プレイヤー行動中
    EnemyStart, // エネミー行動開始
    EnemyTurn,  // エネミー行動中
    TurnEnd,    // ターン終了⇒KeyInputに戻る
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager turnInstance;
    public GameState nowGameState;  // 現在のGameState
    [SerializeField] private float turnDelay = 0.2f;    // 移動時に掛けるディレイの秒数

    void Awake()
    {
        nowGameState = GameState.KeyInput;  // 最初の設定はKeyInput
        if(turnInstance == null)
        {
            turnInstance = this;
        }
        else if(turnInstance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 現在のゲームステートを次に進める関数
    /// </summary>
    /// <param name="setState"></param>
    public void SetNextGameState()
    {
        if(nowGameState != GameState.TurnEnd)
        {
            nowGameState++;
        }
        else
        {
            nowGameState = GameState.KeyInput;
        }
        
    }

    //プレイヤーの移動中の処理
    // エネミーのターン処理
}
