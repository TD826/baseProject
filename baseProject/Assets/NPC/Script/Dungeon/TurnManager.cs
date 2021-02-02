using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;
/// <summary>
/// ターン制のゲームに使用するスクリプト
/// </summary>

/// <summary>
/// ゲームでどの種類のユニットが行動しているかを示す列挙体
/// </summary>
public enum GameState
{
    PlayerTurn, // プレイヤー行動中
    EnemyTurn,  // エネミー行動中
}

public class TurnManager : SingletonMonoBehaviour<TurnManager>
{
    public GameState nowGameState;  // 現在のGameState
    private List<EnemyObject> turnObjectList = new List<EnemyObject>();   // ターン制で動かすオブジェクト(エネミーのみ)
    public float turnDelay = 0.2f;    // 移動時に掛けるディレイの秒数
    private bool sortFlg = false;   // ソート完了フラグ

    private ReactiveProperty<GameState> rpGameState = new ReactiveProperty<GameState>();

    public void Awake(){
        if(this != Instance){
            Destroy(this);
            return;
        }
    }

    /// <summary>
    /// リスト内のオブジェクトを優先度順に並べ替える
    /// </summary>
    private void SortingObjectList()
    {
        turnObjectList.Sort((a,b)=>a.objectPriority - b.objectPriority);
    }


    /// <summary>
    /// 現在のゲームステートを設定する関数
    /// </summary>
    /// <param name="setState"></param>
    public void SetGameState(GameState currentGameState)
    {
        nowGameState = currentGameState;
        GameStateFunc();
    }

    /// <summary>
    /// GameStateの切り替わり時に実行する関数
    /// </summary>
    private void GameStateFunc()
    {
        switch(nowGameState)
        {
            case GameState.PlayerTurn:
            break;
            
            case GameState.EnemyTurn:
            TurnObjectChackList();
            break;
        }
    }

    /// <summary>
    /// TurnObjectListに格納されている数をチェックする関数
    /// </summary>
    private void TurnObjectChackList()
    {
        if(turnObjectList.Count <= 0)
        {
            SetGameState(GameState.PlayerTurn);
        }
    }

    /// <summary>
    /// TurnObjectのListに追加する関数
    /// </summary>
    /// <param name="turnObject">追加するオブジェクト</param>
    public void AddTurnObject(EnemyObject enemyObject)
    {
        turnObjectList.Add(enemyObject);
    }

    /// <summary>
    /// TurnObjectのListから削除する関数
    /// </summary>
    /// <param name="turnObject">削除するオブジェクト</param>
    public void RemoveTurnObject(EnemyObject enemyObject)
    {
        turnObjectList.Remove(enemyObject);
    }
}
