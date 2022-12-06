using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

[System.Serializable]
public class FaceStateNatural : GameStateMachine.StateNodeBase
{

    [SerializeField]
    float _duration = 0.8f;

    public override void OnEnter()
    {
        base.OnEnter();
        var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

        faceController.ChangeFace(BlendShapePreset.Neutral, 0, 1, _duration);

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

     
    }
}


[System.Serializable]
public class FaceStateJoy : GameStateMachine.StateNodeBase
{

    [SerializeField]
    float _duration = 0.8f;

    public override void OnEnter()
    {
        base.OnEnter();
        var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

        faceController.ChangeFace(BlendShapePreset.Joy, 0, 1, _duration);

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();


    }
}

[System.Serializable]
public class FaceStateAngry : GameStateMachine.StateNodeBase
{

    [SerializeField]
    float _duration = 0.8f;

    public override void OnEnter()
    {
        base.OnEnter();
        var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

        faceController.ChangeFace(BlendShapePreset.Angry, 0, 1, _duration);

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();


    }
}


[System.Serializable]
public class FaceStateA : GameStateMachine.StateNodeBase
{
    [SerializeField]
    float _duration = 0.8f;

    public override void OnEnter()
    {
        base.OnEnter();
        var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

        faceController.ChangeFace(BlendShapePreset.A, 0, 0.3f, _duration);

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();


    }
}


//[System.Serializable]
//public class FaceStateSuprised : GameStateMachine.StateNodeBase
//{

//    [SerializeField]
//    float _duration = 0.8f;

//    public override void OnEnter()
//    {
//        base.OnEnter();
//        var faceController = StateMgr.Blackboard.GetValue<KurokumaSoft.FaceController>("FaceController");

//        faceController.ChangeFace(BlendShapePreset., 0, 1, _duration);

//    }

//    public override void OnExit()
//    {
//        base.OnExit();
//    }

//    public override void OnUpdate()
//    {
//        base.OnUpdate();


//    }
//}