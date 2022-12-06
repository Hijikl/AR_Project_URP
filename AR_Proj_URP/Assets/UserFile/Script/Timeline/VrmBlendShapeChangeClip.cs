//using System.Linq;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//using UnityEngine;
//using UnityEngine.Playables;
//using UnityEngine.Timeline;
//using VRM;

//[System.Serializable]
//public class VrmBlendShapeChangeClip : PlayableAsset, ITimelineClipAsset
//{

//    public string BlendShapeKeyName { get; set; }
//    public VRMBlendShapeProxy Proxy { get; set; }
//    public ClipCaps clipCaps => ClipCaps.Blending | ClipCaps.SpeedMultiplier;

//    public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject owner)
//    {
//        var playable = ScriptPlayable<VrmBlendShapeChangeBehaviour>.Create(graph);
//        var behaviour = playable.GetBehaviour();
//        behaviour.BlendShapeKeyName = BlendShapeKeyName;
//        behaviour.Proxy = Proxy;
//        return playable;
//    }
//}


//#if UNITY_EDITOR

//[CustomEditor(typeof(VrmBlendShapeChangeClip))]
//public class VrmBlendShapeChangeClipEditor : Editor
//{
//    private VrmBlendShapeChangeClip clip;
//    private string selectBlendShapeKeyName;
//    private readonly string selectedIndexKey = $"{nameof(VrmBlendShapeChangeClip)}.{nameof(selectBlendShapeKeyName)}";
//    private readonly string emptyString = ".Not Select BlendShape Key";

//    void OnEnable()
//    {
//        clip = (VrmBlendShapeChangeClip)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        EditorGUI.BeginChangeCheck();

//        var key = $"#{selectedIndexKey}.#{clip.GetHashCode()}";

//        selectBlendShapeKeyName = EditorPrefs.GetString(key);

//        if (clip == null) return;

//        var clipsList = clip.Proxy.BlendShapeAvatar.Clips.Select(x => x.Key.ToString()).ToList();
//        var selectedIndex = clipsList.IndexOf(selectBlendShapeKeyName);

//        var index = EditorGUILayout.Popup("Select BlendShape", selectedIndex, clipsList.ToArray());
//        if (index == -1) return;
//        var selected = clipsList[index];

//        if (!EditorGUI.EndChangeCheck() || selectBlendShapeKeyName == emptyString || selectBlendShapeKeyName == selected) return;
//        Undo.RecordObject(clip, nameof(VrmBlendShapeChangeTrack));
//        EditorPrefs.SetString(key, selected);
//        clip.BlendShapeKeyName = selected;
//    }
//}

//#endif
