using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_���[�W�K�p�C���^�[�t�F�C�X
/// ��������t���邱�ƂŃ_���[�W������s����
/// �p�����g�킸�Ƃ������������Ƃ��ł����Ⴄ
/// </summary>
public interface ITapApplicate
{
    //�����ꂾ�ƃR�s�[���ł����Ⴄ�i�\���̂͒l�^�Ȃ̂Łj
     bool ApplyTap();

  
    //MainObjectParameter�擾�i���҂���m��j
    MainObjectParamater MainObjParam { get; }

}

public class TapApplicant : MonoBehaviour
{

    [SerializeField]
    MainObjectParamater _mainObjParam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��
        //�^�b�v����
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //�^�b�v���ꂽ
            if (Physics.Raycast(ray, out hit))
            {
                var tapApp= hit.collider.gameObject.GetComponent<ITapApplicate>();
                if(tapApp != null)
                {
                    tapApp.ApplyTap();
                }

            }

        }
    }
}
