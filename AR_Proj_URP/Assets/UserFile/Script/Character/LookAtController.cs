using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform targetTransform;

    Vector3? targetPos = null;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    //    targetTransform = Camera.main.transform;
    }

    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (targetTransform != null)
        {
            if (targetPos.HasValue)
            {
                animator.SetLookAtWeight(1.0f, 0.8f, 1.0f, 0.0f, 0.7f);
                animator.SetLookAtPosition(targetPos.Value);
            }
        }
    }

    //��������ꏊ�̗v��
    public void SetLookAtTarget(Transform target)
    {
        targetTransform = target;
        targetPos=targetTransform.position;
    }

    //��������߂�
    public void StopLookAt()
    {
        targetTransform = null;
        targetPos=null;
    }

}
