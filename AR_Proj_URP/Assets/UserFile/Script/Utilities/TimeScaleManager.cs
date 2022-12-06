using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

/// <summary>
/// �^�C���X�P�[�����Ǘ�
/// </summary>
public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager Instance { get; private set; }

    //TimeScale�o�^���
    public class Node
    {
        //�ݒ肷��TimeScale
        public float TimeScale = 1.0f;
        //�c�莞��(�b)
        public float RemainingTime = 0;
    }

    LinkedList<Node> _nodes = new();

    //VFX��fixedTimeStep�L���p
    //�����ݒ���o���Ă����Ė߂���悤�ɂ���
    float _defaultVFXFixedTimeStep = 0;

    /// <summary>
    /// TimeScale�ύX��o�^
    /// </summary>
    /// <param name="timeScale">�ݒ肷��</param>
    /// <param name="duration">��������</param>
    /// <param name="startDelayTime">�ݒ���J�n����܂ł̋󔒎���</param>
    public async void RegisterTimeScale(float timeScale, float duration, float startDelayTime)
    {
        //�w�莞�ԑ҂�
        await UniTask.Delay((int)(startDelayTime * 1000.0f));

        //�o�^
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

        //VFX�̍X�V�p�x�L��
        _defaultVFXFixedTimeStep = UnityEngine.VFX.VFXManager.fixedTimeStep;
    }

    //����GameObject�������钼�O
    void OnDestroy()
    {
        UnityEngine.VFX.VFXManager.fixedTimeStep = _defaultVFXFixedTimeStep;
    }


    // Update is called once per frame
    void Update()
    {
        //�S�m�[�h�̎c�莞�Ԃ�i�߂�
        List<Node> _deleteNodes = new();

        float minTimeScale = 1.0f;
        foreach (var node in _nodes)
        {
            //�c�莞�Ԃ����炷
            node.RemainingTime -=
                Time.unscaledDeltaTime;//TimeScale�̉e�����󂯂Ȃ�
            if (node.RemainingTime <= 0)
            {
                //�폜�\�񂷂�
                _deleteNodes.Add(node);
            }

            //�ŏ���TimeScale��I��
            minTimeScale = Mathf.Min(minTimeScale, node.TimeScale);

        }

        //�폜�\�񂳂�Ă���m�[�h�����X�g����O��
        foreach (var node in _deleteNodes)
        {
            _nodes.Remove(node);
        }
        //TimeScale�Z�b�g
        Time.timeScale = minTimeScale;

        //VFX�̍X�V�p�x�ύX
        UnityEngine.VFX.VFXManager.fixedTimeStep = 
            //�ۑ����������̂�ɃQ�[���ɓK�p���ꂽ
            _defaultVFXFixedTimeStep * Time.timeScale;
    }
}
