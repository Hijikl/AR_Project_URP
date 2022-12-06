using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//  �Q�[���ɓo�ꂷ��I�u�W�F�N�g�̃g�b�v�ɂ���R���|�[�l���g
//  ���̃I�u�W�F�N�g�����҂��A�ǂ��������������
/// </summary>
public class MainObjectParamater : MonoBehaviour
{
    //�`�[��ID
    //�I�u�W�F�N�g�����҂���m�邽�߂̂��
    //�G���������Ƃ�
    [SerializeField]
    int teamID = 0;

    public int TeamID
    { get { return teamID; } set { teamID = value; } }

    //���O
    [SerializeField]
    string _name;
    public string Name => _name;

    //�q�b�g�X�g�b�v����
    public float HitStopTimer { get; set; } = 0;

    //�X�V����
    void Update()
    {
        //�q�b�g�X�g�b�v���Ԃ̌o��
        HitStopTimer -= Time.deltaTime;

        if(HitStopTimer <= 0)
        {
            HitStopTimer = 0;
        }
    }
}
