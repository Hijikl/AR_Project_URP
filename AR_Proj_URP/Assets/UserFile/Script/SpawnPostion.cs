using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnPostion : MonoBehaviour
{
    //出現位置に表示させるマーカー
    [SerializeField]
    Transform _marker;

    List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

    //ARレイキャスト
    [SerializeField]
    ARRaycastManager raycastManager;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended)
            {
                return;
            }
            if (raycastManager.Raycast(touch.position, _raycastHits))
            {
                //タッチした箇所にマーカーを移動させる
                _marker.position = _raycastHits[0].pose.position;
            }
        }
    }
}
