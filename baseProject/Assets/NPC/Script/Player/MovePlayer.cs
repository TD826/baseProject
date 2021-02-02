using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーの動き、移動に関するスクリプト
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    void Update()
    {
        if(TurnManager.Instance.nowGameState == GameState.PlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //左
            {
                transform.rotation = Quaternion.Euler(0,270,0);
                if(ConfirmationBeforeMoving())
                {

                    TurnManager.Instance.SetGameState(GameState.PlayerTurn);

                    transform.Translate(-distance, 0, 0, Space.World);

                    // Observable.Timer(TimeSpan.FromSeconds(TurnManager.Instance.turnDelay))
                    //     .Subscribe(_ => {Debug.Log("遅延処理が実行されます");
                    // });
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) //右
            {
                transform.rotation = Quaternion.Euler(0,90,0);
                if(ConfirmationBeforeMoving())
                {
                    TurnManager.Instance.SetGameState(GameState.PlayerTurn);

                
                    transform.Translate(distance, 0, 0, Space.World);
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) //上
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                if(ConfirmationBeforeMoving())
                {

                    TurnManager.Instance.SetGameState(GameState.PlayerTurn);

                
                    transform.Translate(0, 0, distance, Space.World);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) //下
            {
                transform.rotation = Quaternion.Euler(0,180,0);
                if(ConfirmationBeforeMoving())
                {

                    TurnManager.Instance.SetGameState(GameState.PlayerTurn);

                
                    transform.Translate(0, 0, -distance, Space.World);
                }
            }
        }
    }


    /// <summary>
    /// キー入力後、移動前に行う共通処理
    /// </summary>
    /// <returns>移動するかしないかを判定するbool</returns>
    private bool ConfirmationBeforeMoving()
    {   
        RaycastHit raycastHit;
        bool ret = Physics.Raycast(transform.position,transform.forward, out raycastHit, distance);
        return !ret;
    }
}
