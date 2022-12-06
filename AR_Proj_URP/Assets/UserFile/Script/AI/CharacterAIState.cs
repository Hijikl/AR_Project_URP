using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIStateIdle : GameStateMachine.StateNodeBase
{

    //��
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:�҂���Ԃł�");

        _waitTime = 10.0f;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
      
        //�ړ��ꏊ�w�肪�������ꍇ

        //�@�ړI�n��

      _waitTime -= Time.deltaTime;
        if( _waitTime < 0.0f )
        {
            Debug.Log("�����Ɉڍs");
          //  Animator.SetTrigger("GoInduction");

            return;
        }

    }
}

//�U����
[System.Serializable]
public class AIStateInduction : GameStateMachine.StateNodeBase
{

    //��
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:�U�����ł�");

        _waitTime = 10.0f;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        var input = StateMgr.Blackboard.GetValue<StateBaseAIInputProvider>("AIInput");

        _waitTime -= Time.deltaTime;
        if(_waitTime<0.0f)
        {
            Animator.SetTrigger("GoIdle");
            input.AxisL = Vector2.zero;
            return;

        }

        //�ړ��ʐݒ�
        
        //�@�^�[�Q�b�g�̌���

        //�A�ړ�

        //�B�����������ǂ����̔���

        input.AxisL = new Vector2(0.0f,0.5f);

        
    }
}