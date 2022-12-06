using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 対象を探す
/// </summary>
public class TargetSearcher : MonoBehaviour
{
    //検索対象のレイヤーマスク
    [SerializeField] LayerMask _layerMasks;

    //自キャラの情報
    [SerializeField] MainObjectParamater _mainObjParam;

    //半径
    [SerializeField] float _radius = 1.0f;

    //検索対象
    [System.Flags]
    public enum Comparisionflags
    {
        None = 0,       //なし
        SameTeam = 1 << 0,  //同じチームを探す
        OtherTeam = 1 << 1   //違うチームを探す
    }
    [SerializeField] Comparisionflags _comparisionFlags;

    //ターゲット情報記憶用
    public struct Node
    {
        public float Distance;//相手との距離
        public MainObjectParamater MainObjParam;//相手のパラメータ
    }
    List<Node> _nodes = new List<Node>();
    //   public List<Node> Nodes => _nodes;


    //最も近いやつ返す
    //ノードは近い順に並んでいる
    public Node? GetClosestTarget()//?でnullを返せる
    {
        //int a = 0;
        //int? b= 0;
        //b.HasValue();//中身があるかを確認できる

        if (_nodes.Count == 0) return null;
        return _nodes[0];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //作業用配列
    //毎フレームコライダー用のメモリを確保するのを回避する
    int _numColliders = 0;
    Collider[] _tempColliders = new Collider[100];

    // Update is called once per frame
    void Update()
    {
        //判定実行
        //        var colliders = Physics.OverlapSphere(transform.position, _radius, _layerMasks);
        //↓「NonAlloc」でGCに優しく
        _numColliders = Physics.OverlapSphereNonAlloc
            (transform.position, _radius, _tempColliders, _layerMasks);
        _nodes.Clear();
        for (uint i = 0; i < _numColliders; i++)
        {
            var collider = _tempColliders[i];

            //自キャラ無視
            var targetMainObjParam = collider.attachedRigidbody.GetComponent<MainObjectParamater>();
            if (_mainObjParam == targetMainObjParam)
            {
                continue;
            }

            // Debug.Log(targetMainObjParam.gameObject.name);

            //同じチームも判定する
            bool teamResult = false;
            if (_comparisionFlags.HasFlag(Comparisionflags.SameTeam))
            {
                if (_mainObjParam.TeamID == targetMainObjParam.TeamID) teamResult = true;
            }

            //他のチームも判定する
            if (_comparisionFlags.HasFlag(Comparisionflags.OtherTeam))
            {
                if (_mainObjParam.TeamID != targetMainObjParam.TeamID) teamResult = true;
            }
            //判定
            if (teamResult == false) continue;

            Node node = new();
            node.MainObjParam = targetMainObjParam;
            node.Distance
                = (collider.transform.position
                - transform.position).magnitude;
            _nodes.Add(node);

        }

        //ソート（昇順）
        ///距離の近い順
        _nodes.Sort((a, b) =>
        //aとbを比較
        a.Distance.CompareTo(b.Distance));

    }

    //ギズモを自分で描画できる
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);

        //ワイヤーフレームの球描画
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
