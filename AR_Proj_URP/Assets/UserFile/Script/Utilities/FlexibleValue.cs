using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Reflection;

//�����Ȍ^�ɂȂ����
namespace FlexibleValue
{

    /// <summary>
    /// �ėp�^�̊�{�N���X
    /// </summary>
    [System.Serializable]
    public class FV_base
    {
        //�l�擾
        //obuject�^�c�Ȃ�ł��N���X�@�S�Ă̌^�̂��ƂɂȂ��Ă�
        public virtual object GetValue() => null;

        public virtual void SetValue(object value) { }

    }

    //int�^
    [System.Serializable]
    public class FV_int : FV_base
    {
        [SerializeField] int _value = 0;

        public override object GetValue() => _value;

        public override void SetValue(object value)
        {
            //�����łł����̂�int��������ϐ��ɓ����
            if (value is int v) _value = v;
        }
        public static System.Type GetValueType() => typeof(int);

    }

    //string�^
    [System.Serializable]
    public class FV_string : FV_base
    {
        [SerializeField] string _value = "";
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //�����łł����̂�int��������ϐ��ɓ����
            if (value is string v) _value = v;
        }
        public static System.Type GetValueType() => typeof(string);
    }

    //Object�^(MonoBehavior�p���N���X�Ƃ�)
    [System.Serializable]
    public class FV_object: FV_base
    {
        [SerializeField] Object _value;
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //�����łł����̂�int��������ϐ��ɓ����
            if (value is Object v) _value = v;
        }
        public static System.Type GetValueType() => typeof(Object);

    }

    //���̑��N���X�iC#�̃X�[�[�p�[�[�N���X�j�@���ʂ̃N���X�^
    [System.Serializable]
    public class FV_class : FV_base
    {
        [SerializeField] object _value;
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //�����łł����̂�int��������ϐ��ɓ����
            if (value is object v) _value = v;
        }
        public static System.Type GetValueType() => typeof(object);

    }

    [System.Serializable]
    public class Value
    {
        [SerializeReference, SubclassSelector] FV_base _fv = new FV_int();
       static List<System.Type> _valueClassTypes;


        public T GetValue<T>()
        {
            //object�^�ŋA���Ă��邽�߁A�h�������N���X�ɕς���

            if (_fv.GetValue() is T v) return v;
            return default;
        }

        public void SetValue<T>(T value)
        {
            //���쐬�@�܂��́@�^�Ⴂ
            if (_fv == null || _fv.GetValue() is T == false)
            {
                //�쐬

                //����̂�FV_base�̔h���N���X�������ׂĎ擾
                if (_valueClassTypes != null)
                {
                    _valueClassTypes = Assembly.GetAssembly(typeof(FV_base)).GetTypes().Where(t =>
                    {
                        return t.IsSubclassOf(typeof(FV_base)) && !t.IsAbstract;
                    }).ToList();
                }

                //T��FV_Base�h���N���X�̂ǂ�ɂ�����̂�����
                System.Type targetType = typeof(T);

                foreach (var type in _valueClassTypes)
                {
                    //�w�肵��static�֐������s����
                    var t = (System.Type)type.InvokeMember(
                         "GetValueType",//���̖��O���������֐���T��
                         BindingFlags.InvokeMethod, null, null, null);

                    //�T���Ă��Ď�ꂽ�^�ƁA�����Ŏ擾���Ă����^�����������m�F
                    if (targetType == t ||          //���S�ɓ����^���H���ꂾ�����MonoBehavior���u�p�������^���ǂ����܂ł͔���ł��Ȃ��v
                        targetType.IsSubclassOf(t)) //�h���N���X���H
                    {
                        //�w�^���x����^���쐬����
                        _fv = (FV_base)System.Activator.CreateInstance(type);
                    }
                }
            }



            _fv.SetValue(value);
        }
    }
}