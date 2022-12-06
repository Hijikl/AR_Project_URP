using UnityEngine;
using VRM;
using DG.Tweening;

namespace KurokumaSoft
{

    [RequireComponent(typeof(VRMBlendShapeProxy))]
    public sealed class FaceController : MonoBehaviour
    {

        VRMBlendShapeProxy proxy;

        bool isBusy = false;
        BlendShapePreset currentPreset;

        private void Awake()
        {
            TryGetComponent(out proxy);
        }

        void Start()
        {
            InitFace(0);
        }

        public void InitFace(float duration)
        {
            ChangeFace(BlendShapePreset.Neutral, 0, 1, duration);
        }

        public void ChangeFace(BlendShapePreset preset, float startValue, float endValue, float duration, Ease ease = Ease.InOutQuad)
        {
            if (isBusy)
            {
                return;
            }

            isBusy = true;

            startValue = Mathf.Clamp01(startValue);
            endValue = Mathf.Clamp01(endValue);

            UpdateFace(currentPreset, 0);

            float progress = startValue;
            DOTween.To(() => progress, x => progress = x, endValue, duration).OnUpdate(() => { UpdateFace(preset, progress); isBusy = false; }).SetEase(ease);

            currentPreset = preset;
        }

        void UpdateFace(BlendShapePreset nextPreset, float progress)
        {
            proxy.AccumulateValue(BlendShapeKey.CreateFromPreset(nextPreset), progress);
            proxy.Apply();
        }

    }

}