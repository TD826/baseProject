using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ターン制に従う敵性オブジェクト
/// </summary>
public class EnemyObject : MonoBehaviour
{  
    [Tooltip("行動優先度")]public int objectPriority;
    void OnEnable()
    {
        TurnManager.Instance.AddTurnObject(this);
    }

    void OnDisable()
    {
        TurnManager.Instance.RemoveTurnObject(this);
    }

    /// <summary>
    /// ターンが開始したときに敵性オブジェクトが必要な行動を記述した関数
    /// </summary>
    public void EnemyEnable()
    {
        
    }
}
