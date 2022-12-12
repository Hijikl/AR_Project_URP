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


        if (PlayerInputManager.Instance.GamePlay_GetButtonComeHere())
        {

            //�ړ��ꏊ�w�肪�������ꍇ
            var input = StateMgr.Blackboard.GetValue<StateBaseAIInputProvider>("AIInput");

            var target = input.TargetSearcher.GetClosestTarget();

            //�^�[�Q�b�g�����邩
            if (target.HasValue)
            {
                Debug.Log("����");
                Animator.SetTrigger("GoInduction");
                return;
            }
        }
        }
    }
[System.Serializable]
public class AIStateChase : GameStateMachine.StateNodeBase
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

        _waitTime -= Time.deltaTime;
        if (_waitTime > 0.0f) { return; }


        //�ړ��ꏊ�w�肪�������ꍇ
        var input = StateMgr.Blackboard.GetValue<StateBaseAIInputProvider>("AIInput");

        var target = input.TargetSearcher.GetClosestTarget();

        //�^�[�Q�b�g�����邩
        if (target.HasValue)
        {
            Debug.Log("����");
            Animator.SetTrigger("GoInduction");
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

        var target = input.TargetSearcher.GetClosestTarget();

        //�^�[�Q�b�g�����邩
        if (target != null)
        {
            //����̍��W
            if (input.PathFinding.SetDestination(target.Value.MainObjParam.transform.position))
            {
                Vector3 v = input.PathFinding.DesiredVelocity;
                v.y = 0;
                v.Normalize();
                v *= 0.5f;

                //�ړ�����
                input.AxisL = new Vector2(v.x, v.z);
                //Animator.SetTrigger("GoChase");
            }

        }

        //����������
        if (input.PathFinding.IsArrived())
        {

            Animator.SetTrigger("GoIdle");
            input.AxisL = Vector2.zero;
        }


    }
}