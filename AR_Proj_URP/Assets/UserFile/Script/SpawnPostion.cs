using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnPostion : MonoBehaviour
{
    //�o���ʒu�ɕ\��������}�[�J�[
    [SerializeField]
    Transform _marker;

    List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

    //AR���C�L���X�g
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
                //�^�b�`�����ӏ��Ƀ}�[�J�[���ړ�������
                _marker.position = _raycastHits[0].pose.position;
            }
        }
    }
}
