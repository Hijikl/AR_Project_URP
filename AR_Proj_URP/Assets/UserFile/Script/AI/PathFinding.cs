using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

/// <summary>
/// �o�H�T�����s���N���X
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PathFinding : MonoBehaviour
{
    //�o�H�T������{��
    NavMeshAgent _agent;

    //����������
    public bool IsArrived()
    {
//        get
        {
            if (_agent.pathPending) return false;

            if (_agent.isStopped) return true;
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }
    }
    //��]����ړ�����
    public Vector3 DesiredVelocity => _agent.desiredVelocity;

    //�c��̋���
    //remainingDistance��StoppingDistance=����ĂĂ����������ƌ��Ȃ������������Ă��܂����߁A
    //���̕������Ă���
    public float RemainingDistance=>
        Mathf.Max(0.0f, _agent.remainingDistance-_agent.stoppingDistance);


    //�ړI�n�̐ݒ�
    public bool SetDestination(Vector3 position)
    {
       if(NavMesh.GetSettingsCount() <= 0) { return false; }

        //�͈͊O�Ȃ��ԋ߂��ꏊ�����߂�
        NavMeshHit hit;
        if(NavMesh.SamplePosition(position,out hit,10.0f,NavMesh.AllAreas)==false)
        {
            //�ړ��͈͓��̂����Ƃ��ɐݒ肵�Ă����
            position=hit.position;
        }
        //��~������
        _agent.isStopped=false;//Agent���~�߂邩

        _agent.SetDestination( position);
        return true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out _agent);

        //Transform�������ōX�V����Ȃ��悤�ɐݒ�
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        _agent.angularSpeed = 0.0f;//��]���x
        _agent.acceleration = 0.0f;//�����x

        _agent.isStopped=true ;//�ŏ��͎~�߂Ă���
    }

    // Update is called once per frame
    void Update()
    {
        ////��
        //_agent.SetDestination(new Vector3(-1,0,-10));

        ////���ŃG�[�W�F���g���������G�[�W�F���g�̂���transform������


        //�o�H�T�����I����Ă��Ȃ��ꍇ�͉������Ȃ�
        if (_agent.pathPending) return;

        //�S�[���i�ړI�n�j�͈͓���������Stop
        //�T������~�����瓮�����~�߂�
        if(_agent.remainingDistance<=_agent.stoppingDistance)
        {
            _agent.isStopped = true;
        }

        //���쒆�̂�agent�̍��W�蓮�ňړ�������
        if (_agent.isStopped == false)
        {
            _agent.nextPosition = transform.position;
            _agent.nextPosition = transform.position;
        }
    }

    //Debug�\��
    private void OnDrawGizmos()
    {
        if (_agent == null) { return; }

        Gizmos.color = Color.green;

        //�S�o�H����ŕ\��
        int i = 0;
        Vector3 prevPos = new();
        foreach(Vector3 pos in _agent.path.corners)
        {
            Gizmos.DrawWireSphere(pos,0.2f);

            if(i==0)
            {
                prevPos = pos;
            }
            else
            {
                Gizmos.DrawLine(prevPos,pos);
                prevPos = pos;
            }
            i++;
        }

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position,transform.position+
            _agent.desiredVelocity//�i�݂����A�i�����Ƃ��Ă����
            *5);
    }
}
