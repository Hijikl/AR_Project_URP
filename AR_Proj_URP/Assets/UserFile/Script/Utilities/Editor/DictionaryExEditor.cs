using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

/// <summary>
/// エディター拡張
/// </summary>
[CustomPropertyDrawer(typeof(DictionaryEx<,>))]
public class DictionaryExEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //DictionaryExのメンバ、「_list」を取得
        var fieldProp = 
            property.FindPropertyRelative("_list");//変数本体が来てるんじゃなく、シリアライズされたＪＳＯＮが来てるイメージ
        EditorGUI.PropertyField(position, fieldProp,label,true);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var fieldProp =
           property.FindPropertyRelative("_list");//変数本体が来てるんじゃなく、シリアライズされたＪＳＯＮが来てるイメージ

        return EditorGUI.GetPropertyHeight(fieldProp, true);
    }
}
