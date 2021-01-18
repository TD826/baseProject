using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class TrackingObject : MonoBehaviour
{
    [Tooltip("追跡したいオブジェクトのトランスフォーム"),SerializeField] private Transform trackingObject;
    // private ReactivePropety<Transform> rpTrackingObjectTransform = new ReactivePropety<Transform>(null);
    // // Start is called before the first frame update
    // void Start()
    // {
    //     rpTrackingObjectTransform = trackingObject.transform;
    //     // Transformの値を監視、値が変わったら関数を呼び出してpositionのXZ
    
    //     rpTrackingObjectTransform.ObserveEveryValueChanged(_ => _.Value)
    //                         .Subscribe(_ => XZPositionChanger(trackingObject));
    // }

    // /// <summary>
    // /// XZのPosition情報のみを変更する関数
    // /// </summary>
    // private void XZPositionChanger(Transform trackObjTransform)
    // {
    //     this.transform.position.Set(trackObjTransform.position.x,this.transform.position.y,trackObjTransform.position.z);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
