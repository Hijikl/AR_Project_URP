using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] CharacterBrain _brain;

    //�U���p�̃e���v���[�g�I�u�W�F�N�g����
    [SerializeField] Transform _templateObjs;

    Animator _animator;

     void Awake()
    {
        TryGetComponent(out _animator);
    }

    //Root Motion�Ȃǂňړ��Ȃǂ��������Ɏ��s
    //��RM:�A�j���[�V�����œ��������Q�[���̈ړ��Ƃ��Ĉ�����̂���
    void OnAnimatorMove()
    {
        if (_brain == null)
            return;

        _brain.Move(_animator.deltaPosition);
    }

    
}
