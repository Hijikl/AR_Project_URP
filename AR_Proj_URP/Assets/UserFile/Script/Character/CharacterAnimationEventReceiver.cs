using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationEventReceiver : MonoBehaviour
{
    [SerializeField] CharacterBrain _brain;

    //攻撃用のテンプレートオブジェクトたち
    [SerializeField] Transform _templateObjs;

    Animator _animator;

     void Awake()
    {
        TryGetComponent(out _animator);
    }

    //Root Motionなどで移動などをした時に実行
    //↑RM:アニメーションで動いた分ゲームの移動として扱うやつのこと
    void OnAnimatorMove()
    {
        if (_brain == null)
            return;

        _brain.Move(_animator.deltaPosition);
    }

    
}
