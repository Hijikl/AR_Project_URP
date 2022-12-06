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
      
        //移動場所指定があった場合

        //�@目的地の

      _waitTime -= Time.deltaTime;
        if( _waitTime < 0.0f )
        {
            Debug.Log("歩きに移行");
          //  Animator.SetTrigger("GoInduction");

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

        _waitTime -= Time.deltaTime;
        if(_waitTime<0.0f)
        {
            Animator.SetTrigger("GoIdle");
            input.AxisL = Vector2.zero;
            return;

        }

        //移動量設定
        
        //�@ターゲットの検索

        //�A移動

        //�B到着したかどうかの判定

        input.AxisL = new Vector2(0.0f,0.5f);

        
    }
}