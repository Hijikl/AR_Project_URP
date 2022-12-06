using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

using DG.Tweening;


public class CharacterSpawn : MonoBehaviour
{
    //出現させるキャラクターのプレハブ
    //後々Addressableにしよう
    [SerializeField]
    GameObject _characterPrefab;

    //ARで認識した面にレイが当たったかのリスト
    List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

    //ARレイキャスト
    [SerializeField]
    ARRaycastManager raycastManager;

    [SerializeField]
    Transform _marker;

    [SerializeField]
    Transform _parentTransform;

    //カメラ
    [SerializeField]
    Transform _mainCameraTransform;

    //キャラを出現させたか
    bool _isSpawned = false;

    [SerializeField]
    Button _spawnButton;
    [SerializeField]
    TMPro.TextMeshProUGUI _labetText;

    [SerializeField]
    ARPlaneManager _planeManager;
    bool _planeEnable = true;

    bool _doSpawned = false;

    void Start()
    {
        //スポーンボタンの監視開始
        _spawnButton.OnClickAsObservable().//監視する対象に設定
          Subscribe(_ =>//Subscribe…購読
          {
              _doSpawned = true;
          }
         ).AddTo(this);//thisが消滅したら自分で購読を解除する

        //画面をタップされたか


    }

    async void Update()
    {

        foreach (var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(_planeEnable);
        }

        if (_doSpawned)
        {
            if (_isSpawned == false)
            {
                SpawnCharacter();
            }
            else
            {
                ResetCharacter();
            }


            _doSpawned = false;
        }
        else
        {
            if (_isSpawned == false)
            {
                await MoveSpawnPoint();
            }
        }

    }

    void SpawnCharacter()
    {


        //出現させる場所
        Transform spawnTrans = _marker;

        spawnTrans.LookAt(_mainCameraTransform.position);
        spawnTrans.eulerAngles = new Vector3(0, spawnTrans.eulerAngles.y, 0);


        //マーカーのある位置にモデルを出現させる
        var characterObj = Instantiate(_characterPrefab, spawnTrans.position, spawnTrans.rotation, _parentTransform);
        //  characterObj.transform.parent = _parentTransform;

        //カメラ目線にする
        if (characterObj.TryGetComponent<Blackboard>(out var blackboard))
        {
            var lookAtHead = blackboard.GetValue<VRM.VRMLookAtHead>("LookAtHead");
            if (lookAtHead)
            {
                lookAtHead.Target = _mainCameraTransform;
            }
        }

        _labetText.text = "Reset";

        //マーカーを非表示に
        _marker.gameObject.SetActive(false);

      _planeEnable= false;

        _isSpawned = true;

    }

    void ResetCharacter()
    {
        //キャラクター削除
        foreach (Transform n in _parentTransform)
        {
            Destroy(n.gameObject);
        }

        _labetText.text = "Spawn";
        //マーカーを表示
        _marker.gameObject.SetActive(true);

        _planeEnable = true;


        _isSpawned = false;

    }


    async UniTask MoveSpawnPoint()
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
                //                _marker.position = _raycastHits[0].pose.position;
                await _marker.DOMove(_raycastHits[0].pose.position, 0.8f).SetEase(ease: Ease.OutCirc);

            }
        }
    }
}
