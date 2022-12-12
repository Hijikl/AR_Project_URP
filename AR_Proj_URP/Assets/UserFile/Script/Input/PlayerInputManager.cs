using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;//������


using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

using UniRx;
using UniRx.Triggers;
//PlayerInput�̓Q�[�����Ɉ�����Ȃ�����ʃN���X�ɂ܂Ƃ߂�
//�ǂ�����ł��A�N�Z�X�ł���悤�ɂ���

public class PlayerInputManager : MonoBehaviour
{
    //�V���O���g���C���X�^���X
    //static�ϐ��cstatic�t�B�[���h�ɒu����A�P�������ɂȂ�
    //static PlayerInputManager s_instance = null;
    //public PlayerInputManager Instance => s_instance;

    public static PlayerInputManager Instance { get; private set; }

    PlayerInput _input;

    InputActionMap _actionMapGamePlay;
    InputActionMap _actionMapUI;

    [SerializeField]
    UnityEngine.UI.Button _callButton;
    bool _doAppeal = false;

    [SerializeField]
    UnityEngine.UI.Button _comeHereBtn;
    bool _doInduction = false;

    void Awake()
    {
        //�P��ځA�C���X�^���X��o�^�����������ʂ�
        if (Instance != null) { return; }
        Instance = this;
        //�V�[���J�ڎ��ɃI�u�W�F�N�g�������Ȃ��悤�ɂ���
        //���ʂ̃N���X�Ȃ�����邱�Ƃ͂Ȃ����ǁAunity��object�͏������Ⴄ
        DontDestroyOnLoad(gameObject);

        TryGetComponent(out _input);

        //���������������Ȃ��Ă����悤�ɕϐ��Ɋi�[
        _actionMapGamePlay = _input.actions.FindActionMap("GamePlay");
        _actionMapUI = _input.actions.FindActionMap("UI");
    }

    private void Start()
    {
        _callButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _doAppeal = true;
            }).AddTo(this);
    _comeHereBtn.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _doInduction = true;
            }).AddTo(this);


    }

    public enum ActionMapTypes
    {
        GamePlay = 0,
        UI = 1

    }

    //�A�N�V�����}�b�v�̐؂�ւ�
    public void ChangeActionMap(ActionMapTypes actionMapType)
    {
        //�����œn���ꂽ���O�̃A�N�V�����}�b�v�ɐ؂�ւ�
        _input.SwitchCurrentActionMap(actionMapType.ToString());
    }

    //�������������Ȃ��Ă����悤�ɕ֗��֐�������Ă���

    //======================================
    //�@�Q�[���v���C�֌W
    //======================================
    public Vector2 GamePlay_GetAxisL()
    {
        return _actionMapGamePlay["AxisL"].ReadValue<Vector2>();
    }

    public bool GamePlay_GetButtonAttack()
    {
        //WasPressedThisFrame�c�������u�Ԃ̂P�t���[������true��Ԃ�
        return _actionMapGamePlay["Attack"].WasPressedThisFrame();
    }

    //�����_���łP�s�ɂ܂Ƃ߂��Ⴄ
    public bool GamePlay_GetButtonMenu() => _actionMapGamePlay["Menu"].WasPressedThisFrame();

    //======================================
    //�@UI�֌W
    //======================================

    public bool GamePlay_GetButtonCall()
    {
        bool r = _doAppeal;
        
        if(r)Debug.Log("�Ă΂ꂽ�I");
        
        _doAppeal = false;
        
        return r;

    }
    
    public bool GamePlay_GetButtonComeHere()
    {
        bool r = _doInduction;
        
        if(r)Debug.Log("�U������I");

        _doInduction = false;
        
        return r;

    }


}
