using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIStateIdle : GameStateMachine.StateNodeBase
{

    //‰¼
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:‘Ò‚¿ó‘Ô‚Å‚·");

        _waitTime = 10.0f;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
      
        //ˆÚ“®êŠw’è‚ª‚ ‚Á‚½ê‡

        //‡@–Ú“I’n‚Ì

      _waitTime -= Time.deltaTime;
        if( _waitTime < 0.0f )
        {
            Debug.Log("•à‚«‚ÉˆÚs");
          //  Animator.SetTrigger("GoInduction");

            return;
        }

    }
}

//—U“±’†
[System.Serializable]
public class AIStateInduction : GameStateMachine.StateNodeBase
{

    //‰¼
    float _waitTime = 0.0f;

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("AI:—U“±’†‚Å‚·");

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

        //ˆÚ“®—Êİ’è
        
        //‡@ƒ^[ƒQƒbƒg‚ÌŒŸõ

        //‡AˆÚ“®

        //‡B“’…‚µ‚½‚©‚Ç‚¤‚©‚Ì”»’è

        input.AxisL = new Vector2(0.0f,0.5f);

        
    }
}