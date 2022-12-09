using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBaseAIInputProvider : BaseInputProvider
{
    //視覚情報
    [SerializeField] TargetSearcher _targetSearcher;
    public TargetSearcher TargetSearcher => _targetSearcher;

    //経路探索
    [SerializeField] PathFinding _pathFinding;
    public PathFinding PathFinding => _pathFinding;

    //あとで消す
    private void Awake()
    {
   
    }

    private void Start()
    {
        
    }

    //AIが入力するデータ
    public Vector2 AxisL { get;set; }
    public Vector2 AxisR { get;set; }
    public bool ButtonAppeal { get;set; }

    //==================
    //入力状態
    //==================
    //左軸の取得
    public override Vector2 GetAxisL() => AxisL;

    //右軸取得
    public override Vector2 GetAxisR() => AxisR;

    //攻撃ボタン
    public override bool GetButtonAppeal() => PlayerInputManager.Instance.GamePlay_GetButtonCall();
}
