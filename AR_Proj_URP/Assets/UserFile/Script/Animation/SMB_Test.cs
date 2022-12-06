using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SMB_Test : StateMachineBehaviour
{
    //[SerializeField]GameStateMachine.StateNodeBase _state;
    //↑間違い　これだと「StateNodoBase」のインスタンス（実体）がはいっちゃう
    //基底クラスのインスタンスを持っても…

    [SerializeReference,SubclassSelector] GameStateMachine.StateNodeBase _state;

    //切り替わった（このステートになった）瞬間に呼ばれる
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_state.StateMgr==null)
        {
            //初回は初期設定を行う
            var mgr=animator.GetComponent<GameStateMachine.StateMachineManager>();
            _state.Initialize(animator,mgr);
        }


        //変わるやつに応じてnowStateを切り換える
        _state.StateMgr.ChangeState(_state);
    }

    //このステートが動いてる間ず～～っと動く
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
