using System.Collections;
using UnityEngine;
using UnityEngine.AI;


using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;


[RequireComponent(typeof(NavMeshSurface))]
public class BuildNavMesh : MonoBehaviour
{
    [Header("ベイク更新時間"), SerializeField] int bakeUpdateTime;
    private NavMeshSurface _surface;

    void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
       BakeUpdate().Forget();
    }

    async UniTask BakeUpdate()
    {
        var cancelToken = this.GetCancellationTokenOnDestroy();

        //自分自身が消えるまで繰り返す
        while (cancelToken.IsCancellationRequested == false)
        {

            _surface.BuildNavMesh();
            await UniTask.Delay(bakeUpdateTime);
        }

    }
}