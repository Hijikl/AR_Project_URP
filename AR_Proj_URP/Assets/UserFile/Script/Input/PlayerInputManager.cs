using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;//略そう


using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

using UniRx;
using UniRx.Triggers;
//PlayerInputはゲーム中に一つしかないから別クラスにまとめて
//どこからでもアクセスできるようにする

public class PlayerInputManager : MonoBehaviour
{
    //シングルトンインスタンス
    //static変数…staticフィールドに置かれ、１こだけになる
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
        //１回目、インスタンスを登録した時だけ通る
        if (Instance != null) { return; }
        Instance = this;
        //シーン遷移時にオブジェクトを消さないようにする
        //普通のクラスなら消えることはないけど、unityのobjectは消えちゃう
        DontDestroyOnLoad(gameObject);

        TryGetComponent(out _input);

        //いちいち検索しなくていいように変数に格納
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

    //アクションマップの切り替え
    public void ChangeActionMap(ActionMapTypes actionMapType)
    {
        //引数で渡された名前のアクションマップに切り替え
        _input.SwitchCurrentActionMap(actionMapType.ToString());
    }

    //いちいち書かなくていいように便利関数を作っておく

    //======================================
    //　ゲームプレイ関係
    //======================================
    public Vector2 GamePlay_GetAxisL()
    {
        return _actionMapGamePlay["AxisL"].ReadValue<Vector2>();
    }

    public bool GamePlay_GetButtonAttack()
    {
        //WasPressedThisFrame…押した瞬間の１フレームだけtrueを返す
        return _actionMapGamePlay["Attack"].WasPressedThisFrame();
    }

    //ラムダ式で１行にまとめちゃう
    public bool GamePlay_GetButtonMenu() => _actionMapGamePlay["Menu"].WasPressedThisFrame();

    //======================================
    //　UI関係
    //======================================

    public bool GamePlay_GetButtonCall()
    {
        bool r = _doAppeal;
        
        if(r)Debug.Log("呼ばれた！");
        
        _doAppeal = false;
        
        return r;

    }
    
    public bool GamePlay_GetButtonComeHere()
    {
        bool r = _doInduction;
        
        if(r)Debug.Log("誘導しろ！");

        _doInduction = false;
        
        return r;

    }


}
