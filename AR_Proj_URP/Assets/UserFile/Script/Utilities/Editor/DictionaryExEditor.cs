using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

/// <summary>
/// �G�f�B�^�[�g��
/// </summary>
[CustomPropertyDrawer(typeof(DictionaryEx<,>))]
public class DictionaryExEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //DictionaryEx�̃����o�A�u_list�v���擾
        var fieldProp = 
            property.FindPropertyRelative("_list");//�ϐ��{�̂����Ă�񂶂�Ȃ��A�V���A���C�Y���ꂽ�i�r�n�m�����Ă�C���[�W
        EditorGUI.PropertyField(position, fieldProp,label,true);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var fieldProp =
           property.FindPropertyRelative("_list");//�ϐ��{�̂����Ă�񂶂�Ȃ��A�V���A���C�Y���ꂽ�i�r�n�m�����Ă�C���[�W

        return EditorGUI.GetPropertyHeight(fieldProp, true);
    }
}
