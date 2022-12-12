using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIStateIdle : GameStateMachine.StateNodeBase
{

    //仮
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:待ち状態です");

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

            //移動場所指定があった場合
            var input = StateMgr.Blackboard.GetValue<StateBaseAIInputProvider>("AIInput");

            var target = input.TargetSearcher.GetClosestTarget();

            //ターゲットがいるか
            if (target.HasValue)
            {
                Debug.Log("発見");
                Animator.SetTrigger("GoInduction");
                return;
            }
        }
        }
    }
[System.Serializable]
public class AIStateChase : GameStateMachine.StateNodeBase
{

    //仮
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:待ち状態です");

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


        //移動場所指定があった場合
        var input = StateMgr.Blackboard.GetValue<StateBaseAIInputProvider>("AIInput");

        var target = input.TargetSearcher.GetClosestTarget();

        //ターゲットがいるか
        if (target.HasValue)
        {
            Debug.Log("発見");
            Animator.SetTrigger("GoInduction");
            return;
        }
    }
}

//誘導中
[System.Serializable]
public class AIStateInduction : GameStateMachine.StateNodeBase
{

    //仮
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:誘導中です");

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

        //ターゲットがいるか
        if (target != null)
        {
            //相手の座標
            if (input.PathFinding.SetDestination(target.Value.MainObjParam.transform.position))
            {
                Vector3 v = input.PathFinding.DesiredVelocity;
                v.y = 0;
                v.Normalize();
                v *= 0.5f;

                //移動入力
                input.AxisL = new Vector2(v.x, v.z);
                //Animator.SetTrigger("GoChase");
            }

        }

        //到着したか
        if (input.PathFinding.IsArrived())
        {

            Animator.SetTrigger("GoIdle");
            input.AxisL = Vector2.zero;
        }


    }
}