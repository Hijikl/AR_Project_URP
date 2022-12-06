using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

/// <summary>
/// タイムスケールを管理
/// </summary>
public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager Instance { get; private set; }

    //TimeScale登録情報
    public class Node
    {
        //設定するTimeScale
        public float TimeScale = 1.0f;
        //残り時間(秒)
        public float RemainingTime = 0;
    }

    LinkedList<Node> _nodes = new();

    //VFXのfixedTimeStep記憶用
    //初期設定を覚えておいて戻せるようにする
    float _defaultVFXFixedTimeStep = 0;

    /// <summary>
    /// TimeScale変更を登録
    /// </summary>
    /// <param name="timeScale">設定する</param>
    /// <param name="duration">持続時間</param>
    /// <param name="startDelayTime">設定を開始するまでの空白時間</param>
    public async void RegisterTimeScale(float timeScale, float duration, float startDelayTime)
    {
        //指定時間待つ
        await UniTask.Delay((int)(startDelayTime * 1000.0f));

        //登録
        Node node = new Node();
        node.TimeScale = timeScale;
        node.RemainingTime = duration;
        _nodes.AddLast(node);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        //VFXの更新頻度記憶
        _defaultVFXFixedTimeStep = UnityEngine.VFX.VFXManager.fixedTimeStep;
    }

    //このGameObjectが消える直前
    void OnDestroy()
    {
        UnityEngine.VFX.VFXManager.fixedTimeStep = _defaultVFXFixedTimeStep;
    }


    // Update is called once per frame
    void Update()
    {
        //全ノードの残り時間を進める
        List<Node> _deleteNodes = new();

        float minTimeScale = 1.0f;
        foreach (var node in _nodes)
        {
            //残り時間を減らす
            node.RemainingTime -=
                Time.unscaledDeltaTime;//TimeScaleの影響を受けない
            if (node.RemainingTime <= 0)
            {
                //削除予約する
                _deleteNodes.Add(node);
            }

            //最小のTimeScaleを選別
            minTimeScale = Mathf.Min(minTimeScale, node.TimeScale);

        }

        //削除予約されているノードをリストから外す
        foreach (var node in _deleteNodes)
        {
            _nodes.Remove(node);
        }
        //TimeScaleセット
        Time.timeScale = minTimeScale;

        //VFXの更新頻度変更
        UnityEngine.VFX.VFXManager.fixedTimeStep = 
            //保存した初期のやつにゲームに適用された
            _defaultVFXFixedTimeStep * Time.timeScale;
    }
}
