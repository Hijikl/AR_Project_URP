using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brain��Animator�𒇉�Ă����N���X
namespace GameStateMachine
{

    /// <summary>
    /// �X�e�[�g�}�V���m�[�h���N���X
    /// </summary>
    [System.Serializable]//Serialize�c���񉻁@able�c�ł���
    [RequireComponent(typeof(Animator))]
    public class StateNodeBase
    {
        //���̃X�e�[�g��"������"���P�x�������s
        public virtual void OnEnter() { }

        //���̃X�e�[�g����"�o��"���P�x�������s
        public virtual void OnExit() { }

        //���t���[�����s
        public virtual void OnUpdate() { }

        //��莞�Ԃ��ƂɎ��s
        public virtual void OnFixedUpdate() { }

        //�Ǘ����}�l�[�W���[�ւ̎Q��
        StateMachineManager _StateMgr;

        public StateMachineManager StateMgr => _StateMgr;

        //�A�j���[�^�ւ̎Q��
        Animator _animator;
        public Animator Animator => _animator;
        public void Initialize(Animator animator, StateMachineManager mgr)
        {
            _animator = animator;
            _StateMgr = mgr;
        }

    }

    //���A�j���[�^�[�ƈꏏ�ɓ���������A�j���[�^�[�̂��Ă�I�u�W�F�N�g�ɃA�^�b�`����R���|�[�l���g

    /// <summary>
    /// �X�e�[�g�}�V���Ǘ��N���X
    /// </summary>
    public class StateMachineManager : MonoBehaviour
    {

        ////�i���j����ܐ搶�̓I�X�X�����Ȃ�
        //[SerializeField] 
        //private CharacterBrain _charabrain;

        //���L�f�[�^
        [SerializeField] Blackboard _blackboard;
        public Blackboard Blackboard => _blackboard;

        //���݂̃X�e�[�g
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


        //�X�e�[�g�̕ύX
        public void ChangeState(StateNodeBase state)
        {
            if (_nowState != null)
            {
                _nowState.OnExit();
            }

            _nowState = state;


            //null�`�F�b�N&�֐��Ă�
            //���̏��������ł���͎̂���N���X�����BMonoBehavior�p��������ɂ͐�Ύg��Ȃ��I�I
            _nowState?.OnEnter();

        }

    }
}