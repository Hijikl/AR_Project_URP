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
    [Header("�x�C�N�X�V����"), SerializeField] int bakeUpdateTime;
    private NavMeshSurface _surface;

    void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
       BakeUpdate().Forget();
    }

    async UniTask BakeUpdate()
    {
        var cancelToken = this.GetCancellationTokenOnDestroy();

        //�������g��������܂ŌJ��Ԃ�
        while (cancelToken.IsCancellationRequested == false)
        {

            _surface.BuildNavMesh();
            await UniTask.Delay(bakeUpdateTime);
        }

    }
}