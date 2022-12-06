using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using VRM;

using DG.Tweening;


using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
//視線をきょろきょろさせるスクリプト
[RequireComponent(typeof(VRMLookAtHead))]
public class ViewPointCotroller : MonoBehaviour
{
    enum eViewDir
    {
        LEFT_UP,
        RIGHT_UP,

        LEFT_DOWN,
        RIGHT_DOWN,
    }

    eViewDir _nowViewDir;

    [SerializeField]
    Transform _viewPoint;

    [SerializeField]
    Transform _leftUp;
    [SerializeField]
    Transform _leftDown;
    [SerializeField]
    Transform _rightUp;
    [SerializeField]
    Transform _rightDown;

    VRMLookAtHead _vrmLookAtHead;

    [SerializeField]
    float _interval = 4.0f;

    bool _doControll = false;

    Tweener _nowTween;

    [SerializeField]
    PlayableDirector _playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _vrmLookAtHead);
        if (_vrmLookAtHead != null)
            _vrmLookAtHead.Target = _viewPoint;



    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Ceil((float)_playableDirector.time) >= Mathf.Ceil((float)_playableDirector.duration))
        {
            if (_doControll == false)
            {
                StartViewPointMove();
            }
        }


        if (_doControll)
        {
            if (_nowTween.IsActive() == false)
            {
                //次の目的地を設定
                switch (_nowViewDir)
                {
                    case eViewDir.LEFT_UP:

                        _nowTween = _viewPoint.DOMove(_rightDown.position, _interval);
                        _nowViewDir = eViewDir.RIGHT_DOWN;
                        break;
                    case eViewDir.RIGHT_UP:
                        _nowTween = _viewPoint.DOMove(_leftDown.position, _interval);
                        _nowViewDir = eViewDir.LEFT_DOWN;
                        break;

                    case eViewDir.LEFT_DOWN:
                        _nowTween = _viewPoint.DOMove(_leftUp.position, _interval);
                        _nowViewDir = eViewDir.LEFT_UP;
                        break;

                    case eViewDir.RIGHT_DOWN:
                        _nowTween = _viewPoint.DOMove(_rightUp.position, _interval);
                        _nowViewDir = eViewDir.RIGHT_UP;

                        break;

                }
            }
        }


    }



    public void StartViewPointMove()
    {
        _doControll = true;
        _nowTween = _viewPoint.DOMove(_leftUp.position, _interval);
        _nowViewDir = eViewDir.LEFT_UP;
    }

    public void EndViewPointMove()
    {
        _doControll = false;
        _vrmLookAtHead.Target = null;

    }
}
