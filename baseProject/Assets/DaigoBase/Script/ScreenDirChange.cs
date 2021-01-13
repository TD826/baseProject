using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDirChange : MonoBehaviour
{
    DeviceOrientation beforeOrientation;    // 直前の向き

    /// <summary>
    /// 端末の方向を取得する関数
    /// リファレンス：https://docs.unity3d.com/ja/current/ScriptReference/DeviceOrientation.html
    /// </summary>
    /// <returns></returns>
    DeviceOrientation GetOrientation()
    {
        DeviceOrientation dir = Input.deviceOrientation;
        
        // Unknownの時はピクセル数から判断
        if(dir == DeviceOrientation.Unknown)
        {
            if(Screen.width < Screen.height)
            {
                dir = DeviceOrientation.Portrait;
            }
            else
            {
                dir = DeviceOrientation.LandscapeLeft;
            }
        }

        return dir;
    }

    void Start()
    {
        beforeOrientation = GetOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        DeviceOrientation currentOrientation = GetOrientation();

        if(beforeOrientation != currentOrientation)
        {
            // 画面の向きが変わった時の処理

            
            beforeOrientation = currentOrientation;
        }
    }
}
