using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SMB_Test : StateMachineBehaviour
{
    //[SerializeField]GameStateMachine.StateNodeBase _state;
    //���ԈႢ�@���ꂾ�ƁuStateNodoBase�v�̃C���X�^���X�i���́j���͂������Ⴄ
    //���N���X�̃C���X�^���X�������Ă��c

    [SerializeReference,SubclassSelector] GameStateMachine.StateNodeBase _state;

    //�؂�ւ�����i���̃X�e�[�g�ɂȂ����j�u�ԂɌĂ΂��
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_state.StateMgr==null)
        {
            //����͏����ݒ���s��
            var mgr=animator.GetComponent<GameStateMachine.StateMachineManager>();
            _state.Initialize(animator,mgr);
        }


        //�ς���ɉ�����nowState��؂芷����
        _state.StateMgr.ChangeState(_state);
    }

    //���̃X�e�[�g�������Ă�Ԃ��`�`���Ɠ���
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
