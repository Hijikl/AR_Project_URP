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
    //�o��������L�����N�^�[�̃v���n�u
    //��XAddressable�ɂ��悤
    [SerializeField]
    GameObject _characterPrefab;

    //AR�ŔF�������ʂɃ��C�������������̃��X�g
    List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

    //AR���C�L���X�g
    [SerializeField]
    ARRaycastManager raycastManager;

    [SerializeField]
    Transform _marker;

    [SerializeField]
    Transform _parentTransform;

    //�J����
    [SerializeField]
    Transform _mainCameraTransform;

    //�L�������o����������
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
        //�X�|�[���{�^���̊Ď��J�n
        _spawnButton.OnClickAsObservable().//�Ď�����Ώۂɐݒ�
          Subscribe(_ =>//Subscribe�c�w��
          {
              _doSpawned = true;
          }
         ).AddTo(this);//this�����ł����玩���ōw�ǂ���������

        //��ʂ��^�b�v���ꂽ��


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


        //�o��������ꏊ
        Transform spawnTrans = _marker;

        spawnTrans.LookAt(_mainCameraTransform.position);
        spawnTrans.eulerAngles = new Vector3(0, spawnTrans.eulerAngles.y, 0);


        //�}�[�J�[�̂���ʒu�Ƀ��f�����o��������
        var characterObj = Instantiate(_characterPrefab, spawnTrans.position, spawnTrans.rotation, _parentTransform);
        //  characterObj.transform.parent = _parentTransform;

        //�J�����ڐ��ɂ���
        if (characterObj.TryGetComponent<Blackboard>(out var blackboard))
        {
            var lookAtHead = blackboard.GetValue<VRM.VRMLookAtHead>("LookAtHead");
            if (lookAtHead)
            {
                lookAtHead.Target = _mainCameraTransform;
            }
        }

        _labetText.text = "Reset";

        //�}�[�J�[���\����
        _marker.gameObject.SetActive(false);

      _planeEnable= false;

        _isSpawned = true;

    }

    void ResetCharacter()
    {
        //�L�����N�^�[�폜
        foreach (Transform n in _parentTransform)
        {
            Destroy(n.gameObject);
        }

        _labetText.text = "Spawn";
        //�}�[�J�[��\��
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
                //�^�b�`�����ӏ��Ƀ}�[�J�[���ړ�������
                //                _marker.position = _raycastHits[0].pose.position;
                await _marker.DOMove(_raycastHits[0].pose.position, 0.8f).SetEase(ease: Ease.OutCirc);

            }
        }
    }
}
