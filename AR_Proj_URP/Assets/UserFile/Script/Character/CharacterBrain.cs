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

    //����
    BaseInputProvider _inputProvider;

    //��̌�����ς��邽�߂̃{�[��

    //�J�����H
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

        //�����̎q�ȉ��I�u�W�F�N�g��BaseInput���p�������R���|�[�l���g���擾
        _inputProvider = GetComponentInChildren<BaseInputProvider>();


        //    _velocity = new Vector3(0, 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _charaCtrl.Move(_velocity * Time.deltaTime);

          //���n�������͏d�͂����E�����
            if (_charaCtrl.isGrounded)
            {
                _velocity.y = 0;
            }

      

    }

    //�O����ړ�����
    public void Move(Vector3 v)
    {
        _charaCtrl.Move(v);
    }

    //�������
    [System.Serializable]
    public class ASStand : GameStateMachine.StateNodeBase
    {
        //���̃X�e�[�g��"������"���P�x�������s
        public override void OnEnter()
        {
            Debug.Log("brain:������Ԃł�");

    
            base.OnEnter();
        }

        //���̃X�e�[�g����"�o��"���P�x�������s
        public override void OnExit()
        {
            base.OnExit();
        }

        //���t���[�����s
        public override void OnUpdate()
        {
            base.OnUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");


            var axisL = brain._inputProvider.GetAxisL();
            float axisPow = axisL.magnitude;//�A�i���O�L�[�X���

            //�X�e�B�b�N�̗V�ѕ��͖�������
            if (axisPow > 0.1f)
            {
                brain._animator.SetBool("IsMoving", true);

            }

            //�d��
            brain._velocity.y += -9.8f * Time.deltaTime;


            //�A�s�[��
            if (brain._inputProvider.GetButtonAppeal())
            {
                //�g���K�[��T/F���L�ڂ���K�v�Ȃ�
                brain._animator.SetTrigger("DoAppeal");

            }

        }

        //��莞�Ԃ��ƂɎ��s
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //���C�͋󒆂ƒn��ŕς���ׂ�
            if (brain._charaCtrl.isGrounded)
            {
                //�n��ɂ��鎞
                brain._velocity *= 0.85f;
            }
            else
            {
                //�󒆂ɂ��鎞(��C��R��)
                brain._velocity *= 0.98f;
            }
        }


    }

    //�������
    [System.Serializable]
    public class ASWalk : GameStateMachine.StateNodeBase
    {
        //���̃X�e�[�g��"������"���P�x�������s
        public override void OnEnter()
        {
            Debug.Log("brain:������Ԃł�");
         
            base.OnEnter();
        }

        //���̃X�e�[�g����"�o��"���P�x�������s
        public override void OnExit()
        {
            base.OnExit();
        }

        //���t���[�����s
        public override void OnUpdate()
        {
            base.OnUpdate();

            //�񐄏��A�����Ȃ���
            //�e�̃R���|�[�l���g���Ƃ��Ă���
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            var axisL = brain._inputProvider.GetAxisL();

            //�L�����̈ړ�
            float axisPow = axisL.magnitude;//�A�i���O�L�[�X���


            //�X�e�B�b�N�̗V�ѕ��͖�������
            if (axisPow <= 0.1f)
            {
                brain._animator.SetBool("IsMoving", false);

                //moveSpeed��ς����Ȃ��悤Stand�ɕς�鎞���̂܂܏o���Ⴄ
                return;
            }

            brain._animator.SetFloat("MoveSpeed", axisPow);

            //xz���ʂōl����@�L�������i�ޕ���
            Vector3 forward = new Vector3(axisL.x, 0, axisL.y);
            forward.Normalize();

            forward *= axisPow * brain._moveSpeed;

            brain._velocity += forward * Time.deltaTime;


            //�d��
            brain._velocity.y += -9.8f * Time.deltaTime;


            //����
            if (axisPow > 0.01f)
            {
                //������new�̓������m�ۂ���Ȃ��AGC�͓����Ȃ�
                //new�ō���Ă���̂��N���X����Ȃ��č\���̂�����
                Vector3 vLook = new Vector3(axisL.x, 0, axisL.y);

                Quaternion rotation = Quaternion.LookRotation(vLook, Vector3.up);

                brain.transform.rotation = Quaternion.RotateTowards(
                    brain.transform.rotation,   //�ω��O�̉�]
                    rotation,                   //�ω���̉�]
                    720 * Time.deltaTime);      //�ω�����p�x
            }

            //�A�s�[��
            if (brain._inputProvider.GetButtonAppeal())
            {
                //�g���K�[��T/F���L�ڂ���K�v�Ȃ�
                brain._animator.SetTrigger("DoAppeal");

            }
            
        }

        //��莞�Ԃ��ƂɎ��s
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //���C�͋󒆂ƒn��ŕς���ׂ�
            if (brain._charaCtrl.isGrounded)
            {
                //�n��ɂ��鎞
                brain._velocity *= 0.85f;
            }
            else
            {
                //�󒆂ɂ��鎞(��C��R��)
                brain._velocity *= 0.98f;
            }
        }


    }

    //�������
    [System.Serializable]
    public class ASAppeal : GameStateMachine.StateNodeBase
    {
        Tweener _nowTween=null;

        Quaternion _toQuaternion;

        //���̃X�e�[�g��"������"���P�x�������s
        public override  void OnEnter()
        {

            base.OnEnter();
            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");

            brain._faceAnimator.SetTrigger("GoJoy");

      
            //�J�����̕���������(����)
            var lookAtController = StateMgr.Blackboard.GetValue<LookAtController>("LookAtController");
            lookAtController.SetLookAtTarget(Camera.main.transform);

           // var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");
            //�J�����̕��������i�́j
            Vector3 vLook = Camera.main.transform.position - brain.transform.position;
            vLook.y = 0;

            _toQuaternion = Quaternion.LookRotation(vLook, Vector3.up);

         
        }

        //���̃X�e�[�g����"�o��"���P�x�������s
        public override void OnExit()
        {
            base.OnExit();
            var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

            faceController.ChangeFace(BlendShapePreset.Neutral, 0, 1, 0.5f);
            //�J�����̕����������̂���߂�
            var lookAtController = StateMgr.Blackboard.GetValue<LookAtController>("LookAtController");
            lookAtController.StopLookAt();

        }

        //���t���[�����s
        public override void OnUpdate()
        {
            base.OnUpdate();

            var brain = StateMgr.Blackboard.GetValue<CharacterBrain>("Brain");

            brain.transform.rotation = Quaternion.RotateTowards(
               brain.transform.rotation,   //�ω��O�̉�]
                _toQuaternion,                   //�ω���̉�]
                720 * Time.deltaTime);      //�ω�����p�x

        }

        //��莞�Ԃ��ƂɎ��s
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }


    }
}
