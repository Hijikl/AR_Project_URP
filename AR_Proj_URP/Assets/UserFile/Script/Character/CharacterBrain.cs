using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StandardAssets.Characters.Physics;

using VRM;

using DG.Tweening;

[RequireComponent(typeof(OpenCharacterController))]
public class CharacterBrain : MonoBehaviour, ITapApplicate
{
    [SerializeField]
    float _moveSpeed = 1.0f;

    [SerializeField]
    Animator _animator;
    [SerializeField]
    Animator _faceAnimator;
    OpenCharacterController _charaCtrl;

    //入力
    BaseInputProvider _inputProvider;

    //顔の向きを変えるためのボーン

    //カメラ？
    [SerializeField]
    Transform _mainCameraTransform;


    Vector3 _velocity = new Vector3();

    MainObjectParamater _mainObjParam;
    public MainObjectParamater MainObjParam => _mainObjParam;

    bool ITapApplicate.ApplyTap()
    {
        _animator.SetTrigger("DoStagger");
        _faceAnimator.SetTrigger("GoAngry");
        return true;
    }

    void Awake()
    {
        TryGetComponent(out _charaCtrl);
        TryGetComponent(out _mainObjParam);

        //自分の子以下オブジェクトのBaseInputを継承したコンポーネントを取得
        _inputProvider = GetComponentInChildren<BaseInputProvider>();


        //    _velocity = new Vector3(0, 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _charaCtrl.Move(_velocity * Time.deltaTime);

          //着地した時は重力が相殺される
            if (_charaCtrl.isGrounded)
            {
                _velocity.y = 0;
            }

      

    }

    //外から移動さす
    public void Move(Vector3 v)
    {
        _charaCtrl.Move(v);
    }

    //立ち状態
    [System.Serializable]
    public class ASStand : GameStateMachine.StateNodeBase
    {
        //このステートに"入った"時１度だけ実行
        public override void OnEnter()
        {
            Debug.Log("brain:立ち状態です");

    
            base.OnEnter();
        }

        //このステートから"出る"時１度だけ実行
        public override void OnExit()
        {
            base.OnExit();
        }

        //毎フレーム実行
        public override void OnUpdate()
        {
            base.OnUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");


            var axisL = brain._inputProvider.GetAxisL();
            float axisPow = axisL.magnitude;//アナログキー傾き具合

            //スティックの遊び分は無視する
            if (axisPow > 0.1f)
            {
                brain._animator.SetBool("IsMoving", true);

            }

            //重力
            brain._velocity.y += -9.8f * Time.deltaTime;


            //アピール
            if (brain._inputProvider.GetButtonAppeal())
            {
                //トリガーはT/Fを記載する必要なし
                brain._animator.SetTrigger("DoAppeal");

            }

        }

        //一定時間ごとに実行
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //摩擦は空中と地上で変えるべき
            if (brain._charaCtrl.isGrounded)
            {
                //地上にいる時
                brain._velocity *= 0.85f;
            }
            else
            {
                //空中にいる時(空気抵抗分)
                brain._velocity *= 0.98f;
            }
        }


    }

    //歩き状態
    [System.Serializable]
    public class ASWalk : GameStateMachine.StateNodeBase
    {
        //このステートに"入った"時１度だけ実行
        public override void OnEnter()
        {
            Debug.Log("brain:歩き状態です");
         
            base.OnEnter();
        }

        //このステートから"出る"時１度だけ実行
        public override void OnExit()
        {
            base.OnExit();
        }

        //毎フレーム実行
        public override void OnUpdate()
        {
            base.OnUpdate();

            //非推奨、強引なやり方
            //親のコンポーネントをとってくる
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            var axisL = brain._inputProvider.GetAxisL();

            //キャラの移動
            float axisPow = axisL.magnitude;//アナログキー傾き具合


            //スティックの遊び分は無視する
            if (axisPow <= 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);

                //moveSpeedを変えられないようStandに変わる時そのまま出ちゃう
                return;
            }

            brain._animator.SetFloat("MoveSpeed", axisPow);

            //xz平面で考える　キャラが進む方向
            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
            forward.Normalize();

            forward *= axisPow * brain._moveSpeed;

            brain._velocity += forward * Time.deltaTime;


            //重力
            brain._velocity.y += -9.8f * Time.deltaTime;


            //向き
            if (axisPow > 0.01f)
            {
                //ここのnewはメモリ確保じゃない、GCは動かない
                //newで作っているのがクラスじゃなくて構造体だから
                Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);

                Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

                brain.transform.rotation = Quaternion.RotateTowards(
                    brain.transform.rotation,   //変化前の回転
                    rotation,                   //変化後の回転
                    720 * Time.deltaTime);      //変化する角度
            }

            //アピール
            if (brain._inputProvider.GetButtonAppeal())
            {
                //トリガーはT/Fを記載する必要なし
                brain._animator.SetTrigger("DoAppeal");

            }
            
        }

        //一定時間ごとに実行
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //摩擦は空中と地上で変えるべき
            if (brain._charaCtrl.isGrounded)
            {
                //地上にいる時
                brain._velocity *= 0.85f;
            }
            else
            {
                //空中にいる時(空気抵抗分)
                brain._velocity *= 0.98f;
            }
        }


    }

    //歩き状態
    [System.Serializable]
    public class ASAppeal : GameStateMachine.StateNodeBase
    {
        Tweener _nowTween=null;

        Quaternion _toQuaternion;

        //このステートに"入った"時１度だけ実行
        public override  void OnEnter()
        {

            base.OnEnter();
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");

            brain._faceAnimator.SetTrigger("GoJoy");

      
            //カメラの方向を向く(視線)
            var lookAtController = StateMgr.Blackboard.GetValue<LookAtController>("LookAtController");
            lookAtController.SetLookAtTarget(Camera.main.transform);

           // var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //カメラの方を向く（体）
            Vector3 vLook = Camera.main.transform.position - brain.transform.position;
            vLook.y = 0;

            _toQuaternion = Quaternion.LookRotation(vLook, Vector3.up);

         
        }

        //このステートから"出る"時１度だけ実行
        public override void OnExit()
        {
            base.OnExit();
            var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

            faceController.ChangeFace(BlendShapePreset.Neutral, 0, 1, 0.5f);
            //カメラの方向を向くのをやめる
            var lookAtController = StateMgr.Blackboard.GetValue<LookAtController>("LookAtController");
            lookAtController.StopLookAt();

        }

        //毎フレーム実行
        public override void OnUpdate()
        {
            base.OnUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");

            brain.transform.rotation = Quaternion.RotateTowards(
               brain.transform.rotation,   //変化前の回転
                _toQuaternion,                   //変化後の回転
                720 * Time.deltaTime);      //変化する角度

        }

        //一定時間ごとに実行
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }


    }
}
