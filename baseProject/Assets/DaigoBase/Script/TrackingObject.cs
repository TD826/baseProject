using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TrackingObject : MonoBehaviour
{
    [Tooltip("追跡したいオブジェクトのトランスフォーム"),SerializeField] private Transform trackingObject;
    private ReactivePropety<Transform> rpTrackingObjectTransform = new ReactivePropety<Transform>(trackingObject);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
