using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ԍo�߂Ŕj�󂷂�X�N���v�g
public class DestroyOnTime : MonoBehaviour
{

    [Header("�j�󂳂��܂ł̎���")]
    [SerializeField] float _time;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
