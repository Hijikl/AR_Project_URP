using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

public class proxy4Anim : MonoBehaviour
{
    [Range(0, 1)] public float A;
    [Range(0, 1)] public float I;
    [Range(0, 1)] public float U;
    [Range(0, 1)] public float E;
    [Range(0, 1)] public float O;
    [Range(0, 1)] public float BLINK;
    [Range(0, 1)] public float BLINK_L;
    [Range(0, 1)] public float BLINK_R;
    [Range(0, 1)] public float ANGRY;
    [Range(0, 1)] public float FUN;
    [Range(0, 1)] public float JOY;
    [Range(0, 1)] public float SORROW;
    [Range(0, 1)] public float NEUTRAL;

    [Range(0, 1)] public float LOOKUP;
    [Range(0, 1)] public float LOOKDOWN;
    [Range(0, 1)] public float LOOKLEFT;
    [Range(0, 1)] public float LOOKRIGHT;

    // Šg’£ : (VRoid)
    [Range(0, 1)] public float SURPRISED;

    private VRMBlendShapeProxy targetProxy;


    // Start is called before the first frame update
    void Start()
    {
        targetProxy = this.gameObject.GetComponent<VRMBlendShapeProxy>();

    }

    // Update is called once per frame
    void Update()
    {
        applyShapeKey();
    }


    void applyShapeKey()
    {
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), A);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.I), I);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.U), U);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.E), E);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), O);

        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), BLINK);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_L), BLINK_L);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink_R), BLINK_R);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Angry), ANGRY);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), FUN);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), JOY);
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow),SORROW );
        targetProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Neutral), NEUTRAL);

        targetProxy.AccumulateValue(BlendShapeKey.CreateUnknown("Surprised"), SURPRISED);
     

        targetProxy.Apply();
    }
}
