using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ターン制に従うオブジェクト
/// </summary>
public class TurnObject : MonoBehaviour
{  
    public int objectPriority;
    void OnEnable()
    {
        TurnManager.Instance.AddTurnObject(this);
    }

    void OnDisable()
    {
        TurnManager.Instance.RemoveTurnObject(this);
    }

    /// <summary>
    /// ターンが開始したときに必要な行動を記述した関数
    /// </summary>
    public void TurnEnable()
    {
        
    }
}
