using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���͂̃x�[�X�ƂȂ�N���X

public class BaseInputProvider : MonoBehaviour
{
    //�����̎擾
    public virtual Vector2 GetAxisL() => Vector2.zero;

    //�E���擾
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //�U���{�^��
    public virtual bool GetButtonAppeal() => false;

}
