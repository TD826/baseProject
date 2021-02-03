using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンを使用する際の基底クラス
/// </summary>
/// <typeparam name="T">継承するクラス名</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    protected static T instance;
   public static T Instance
   {
      get
      {
         if(instance == null)
         {
            instance = (T) FindObjectOfType(typeof(T));
 
            if (instance == null)
            {
               Debug.LogError( typeof(T) + "のインスタンスがシーン内に存在しません");
            }
         }
 
         return instance;
      }
   }
}


/*  使用方法（定義方法）
*   public void Awake()
   {
        if(this != Instance)
        {
            Destroy(this);
            return;
        }
        // ↓は必要に応じて
        DontDestroyOnLoad(this.gameObject);//シーン遷移しても破棄されない設定
    }

    参照方法
    T.Instance.~~(~~は変数や関数)
*/