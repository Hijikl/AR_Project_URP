using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBaseAIInputProvider : BaseInputProvider
{
    //���o���
    [SerializeField] TargetSearcher _targetSearcher;
    public TargetSearcher TargetSearcher => _targetSearcher;

    //�o�H�T��
    [SerializeField] PathFinding _pathFinding;
    public PathFinding PathFinding => _pathFinding;

    //���Ƃŏ���
    private void Awake()
    {
   
    }

    private void Start()
    {
        
    }

    //AI�����͂���f�[�^
    public Vector2 AxisL { get;set; }
    public Vector2 AxisR { get;set; }
    public bool ButtonAppeal { get;set; }

    //==================
    //���͏��
    //==================
    //�����̎擾
    public override Vector2 GetAxisL() => AxisL;

    //�E���擾
    public override Vector2 GetAxisR() => AxisR;

    //�U���{�^��
    public override bool GetButtonAppeal() => PlayerInputManager.Instance.GamePlay_GetButtonCall();
}
