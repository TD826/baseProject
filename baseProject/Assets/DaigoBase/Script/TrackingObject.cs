using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class TrackingObject : MonoBehaviour
{
    [Tooltip("追跡したいオブジェクトのTransform"),SerializeField] private Transform trackingObject;
    private ReactiveProperty<Vector3> rpTrackingObjectPosition = new ReactiveProperty<Vector3>(Vector3.zero);

    // Start is called before the first frame update
    void Start()
    {   
        // rpTrackingObjectPositionのValueにpositionを格納(初期値)
        rpTrackingObjectPosition.Value = trackingObject.transform.position;
        // Transformの値を監視、値が変わったら関数を呼び出してpositionのXZ
    
        rpTrackingObjectPosition.ObserveEveryValueChanged(_ => _.Value)
                            .Subscribe(_ => XZPositionChanger(trackingObject));
    }

    /// <summary>
    /// XZのPosition情報のみを変更し、Valueの値も変更する関数
    /// </summary>
    private void XZPositionChanger(Transform trackObjTransform)
    {
        this.transform.position.Set(trackObjTransform.position.x,this.transform.position.y,trackObjTransform.position.z);
        rpTrackingObjectPosition.Value = trackingObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
