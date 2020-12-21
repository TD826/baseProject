using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルのボタン用スクリプト
/// </summary>
public class TitleButton : MonoBehaviour
{
    [SerializeField] private float waitTimeSceneChange = 2.0f;
    [SerializeField] private SceneObject sceneGameMain = null;
    public void TittleToGameMain()
    {
        Debug.Log("ButtonEvent1");
        StartCoroutine(DelayMethodExtension.DelayMethod(this,waitTimeSceneChange,()=>
        {
            Debug.Log("ButtonEvent2");
            Fade.instance.FadeChange(sceneGameMain);
        }));
    }
}
