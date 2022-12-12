using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

/// <summary>
/// 経路探索を行うクラス
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PathFinding : MonoBehaviour
{
    //経路探索する本体
    NavMeshAgent _agent;

    //到着したか
    public bool IsArrived()
    {
//        get
        {
            if (_agent.pathPending) return false;

            if (_agent.isStopped) return true;
            return _agent.remainingDistance <= _agent.stoppingDistance;
        }
    }
    //希望する移動方向
    public Vector3 DesiredVelocity => _agent.desiredVelocity;

    //残りの距離
    //remainingDistanceはStoppingDistance=離れてても到着したと見なす距離も足してしまうため、
    //その分引いておく
    public float RemainingDistance=>
        Mathf.Max(0.0f, _agent.remainingDistance-_agent.stoppingDistance);


    //目的地の設定
    public bool SetDestination(Vector3 position)
    {
       if(NavMesh.GetSettingsCount() <= 0) { return false; }

        //範囲外なら一番近い場所を求める
        NavMeshHit hit;
        if(NavMesh.SamplePosition(position,out hit,10.0f,NavMesh.AllAreas)==false)
        {
            //移動範囲内のいいとこに設定してくれる
            position=hit.position;
        }
        //停止を解除
        _agent.isStopped=false;//Agentを止めるか

        _agent.SetDestination( position);
        return true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out _agent);

        //Transformが自動で更新されないように設定
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        _agent.angularSpeed = 0.0f;//回転速度
        _agent.acceleration = 0.0f;//加速度

        _agent.isStopped=true ;//最初は止めておく
    }

    // Update is called once per frame
    void Update()
    {
        ////仮
        //_agent.SetDestination(new Vector3(-1,0,-10));

        ////↑でエージェントが動く＝エージェントのついたtransformが動く


        //経路探索が終わっていない場合は何もしない
        if (_agent.pathPending) return;

        //ゴール（目的地）範囲内だったらStop
        //探索が停止したら動きを止める
        if(_agent.remainingDistance<=_agent.stoppingDistance)
        {
            _agent.isStopped = true;
        }

        //動作中のみagentの座標手動で移動させる
        if (_agent.isStopped == false)
        {
            _agent.nextPosition = transform.position;
            _agent.nextPosition = transform.position;
        }
    }

    //Debug表示
    private void OnDrawGizmos()
    {
        if (_agent == null) { return; }

        Gizmos.color = Color.green;

        //全経路を線で表示
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
            _agent.desiredVelocity//進みたい、進もうとしてる方向
            *5);
    }
}
