using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
/// <summary>
/// UniRxのDelayやDelayFrameを使った方がよさそう(最終的に)
/// </summary>
public static class DelayMethodExtension
{
    /// <summary>
    /// 渡されたメソッドを指定時間後に実行する
    /// </summary>
    /// 
    public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime, Action action)
    {
        Debug.Log("Delay");
        yield return new WaitForSeconds(waitTime);
        action();
    }   
    public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime, bool ignoreTimeScale, Action action)
    {
        if (ignoreTimeScale)
        {
            yield return new WaitForSecondsRealtime(waitTime);
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }
        action();
    }
}
public sealed class WaitForSecondsRealtime : CustomYieldInstruction
{
    private float waitTime;
    public override bool keepWaiting
    {
        get { return Time.realtimeSinceStartup < waitTime; }
    }
    public WaitForSecondsRealtime(float time)
    {
        waitTime = Time.realtimeSinceStartup + time;
    }
}
