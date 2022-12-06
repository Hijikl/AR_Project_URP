using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Reflection;

//いろんな型になれるやつ
namespace FlexibleValue
{

    /// <summary>
    /// 汎用型の基本クラス
    /// </summary>
    [System.Serializable]
    public class FV_base
    {
        //値取得
        //obuject型…なんでもクラス　全ての型のもとになってる
        public virtual object GetValue() => null;

        public virtual void SetValue(object value) { }

    }

    //int型
    [System.Serializable]
    public class FV_int : FV_base
    {
        [SerializeField] int _value = 0;

        public override object GetValue() => _value;

        public override void SetValue(object value)
        {
            //引数でできたのがintだったら変数に入れる
            if (value is int v) _value = v;
        }
        public static System.Type GetValueType() => typeof(int);

    }

    //string型
    [System.Serializable]
    public class FV_string : FV_base
    {
        [SerializeField] string _value = "";
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //引数でできたのがintだったら変数に入れる
            if (value is string v) _value = v;
        }
        public static System.Type GetValueType() => typeof(string);
    }

    //Object型(MonoBehavior継承クラスとか)
    [System.Serializable]
    public class FV_object: FV_base
    {
        [SerializeField] Object _value;
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //引数でできたのがintだったら変数に入れる
            if (value is Object v) _value = v;
        }
        public static System.Type GetValueType() => typeof(Object);

    }

    //その他クラス（C#のスーーパーークラス）　普通のクラス型
    [System.Serializable]
    public class FV_class : FV_base
    {
        [SerializeField] object _value;
        public override object GetValue() => _value;
        public override void SetValue(object value)
        {
            //引数でできたのがintだったら変数に入れる
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
            //object型で帰ってくるため、派生したクラスに変える

            if (_fv.GetValue() is T v) return v;
            return default;
        }

        public void SetValue<T>(T value)
        {
            //未作成　または　型違い
            if (_fv == null || _fv.GetValue() is T == false)
            {
                //作成

                //初回のみFV_baseの派生クラス情報をすべて取得
                if (_valueClassTypes != null)
                {
                    _valueClassTypes = Assembly.GetAssembly(typeof(FV_base)).GetTypes().Where(t =>
                    {
                        return t.IsSubclassOf(typeof(FV_base)) && !t.IsAbstract;
                    }).ToList();
                }

                //TがFV_Base派生クラスのどれにあたるのか検索
                System.Type targetType = typeof(T);

                foreach (var type in _valueClassTypes)
                {
                    //指定したstatic関数を実行する
                    var t = (System.Type)type.InvokeMember(
                         "GetValueType",//この名前を持った関数を探す
                         BindingFlags.InvokeMethod, null, null, null);

                    //探してきて取れた型と、引数で取得してきた型が同じかを確認
                    if (targetType == t ||          //完全に同じ型か？これだけやとMonoBehaviorを「継承した型かどうかまでは判定できない」
                        targetType.IsSubclassOf(t)) //派生クラスか？
                    {
                        //『型情報』から型を作成する
                        _fv = (FV_base)System.Activator.CreateInstance(type);
                    }
                }
            }



            _fv.SetValue(value);
        }
    }
}