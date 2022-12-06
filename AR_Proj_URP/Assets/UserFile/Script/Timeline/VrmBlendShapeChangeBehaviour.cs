//using UnityEngine.Playables;
//using VRM;

//[System.Serializable]
//public class VrmBlendShapeChangeBehaviour : PlayableBehaviour
//{
//	public string BlendShapeKeyName { get; set; }
//	public VRMBlendShapeProxy Proxy { get; set; }

//	public override void ProcessFrame(UnityEngine.Playables.Playable playable, FrameData info, object playerData)
//	{
//		if (Proxy == null) return;
//		Proxy.ImmediatelySetValue(BlendShapeKey.CreateUnknown(BlendShapeKeyName), info.weight);
//	}
//}

