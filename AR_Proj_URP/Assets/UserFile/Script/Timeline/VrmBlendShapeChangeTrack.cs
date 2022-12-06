//using UnityEngine;
//using UnityEngine.Playables;
//using UnityEngine.Timeline;
//using VRM;

//[TrackBindingType(typeof(VRMBlendShapeProxy))]
//[TrackColor(0, 1, 0)]
//[TrackClipType(typeof(VrmBlendShapeChangeClip))]
//public class VrmBlendShapeChangeTrack : TrackAsset
//{

//    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject director, int inputCount)
//    {
//        var playable = ScriptPlayable<VrmBlendShapeChangeBehaviour>.Create(graph, inputCount);
//        foreach (var clip in GetClips())
//        {
//            if (clip.asset is VrmBlendShapeChangeClip changeClip)
//            {
//                changeClip.Proxy = GetBindingComponent<VRMBlendShapeProxy>(director);
//            }
//        }
//        return playable;
//    }

//    public static T GetBindingComponent<T>(this TrackAsset asset, GameObject gameObject) where T : class
//    {
//        if (gameObject == null) return default;

//        var director = gameObject.GetComponent<PlayableDirector>();
//        if (director == null) return default;

//        var binding = director.GetGenericBinding(asset) as T;

//        return binding switch
//        {
//            { } component => component,
//            _ => default
//        };
//    }
//}
