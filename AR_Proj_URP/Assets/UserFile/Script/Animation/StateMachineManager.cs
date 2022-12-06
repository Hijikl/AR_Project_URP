using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BrainとAnimatorを仲介してくれるクラス
namespace GameStateMachine
{

    /// <summary>
    /// ステートマシンノード基底クラス
    /// </summary>
    [System.Serializable]//Serialize…直列化　able…できる
    [RequireComponent(typeof(Animator))]
    public class StateNodeBase
    {
        //このステートに"入った"時１度だけ実行
        public virtual void OnEnter() { }

        //このステートから"出る"時１度だけ実行
        public virtual void OnExit() { }

        //毎フレーム実行
        public virtual void OnUpdate() { }

        //一定時間ごとに実行
        public virtual void OnFixedUpdate() { }

        //管理元マネージャーへの参照
        StateMachineManager _StateMgr;

        public StateMachineManager StateMgr => _StateMgr;

        //アニメータへの参照
        Animator _animator;
        public Animator Animator => _animator;
        public void Initialize(Animator animator, StateMachineManager mgr)
        {
            _animator = animator;
            _StateMgr = mgr;
        }

    }

    //↓アニメーターと一緒に動かすからアニメーターのついてるオブジェクトにアタッチするコンポーネント

    /// <summary>
    /// ステートマシン管理クラス
    /// </summary>
    public class StateMachineManager : MonoBehaviour
    {

        ////（仮）あんま先生はオススメしない
        //[SerializeField] 
        //private CharacterBrain _charabrain;

        //共有データ
        [SerializeField] Blackboard _blackboard;
        public Blackboard Blackboard => _blackboard;

        //現在のステート
        StateNodeBase _nowState;

        private void Update()
        {
            if (_nowState == null) { return; }
            _nowState.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (_nowState == null) { return; }
            _nowState.OnFixedUpdate();
        }


        //ステートの変更
        public void ChangeState(StateNodeBase state)
        {
            if (_nowState != null)
            {
                _nowState.OnExit();
            }

            _nowState = state;


            //nullチェック&関数呼ぶ
            //この書き方ができるのは自作クラスだけ。MonoBehavior継承したやつには絶対使わない！！
            _nowState?.OnEnter();

        }

    }
}