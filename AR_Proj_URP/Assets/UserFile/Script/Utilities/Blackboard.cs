using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour, ISerializationCallbackReceiver
{
   // [SerializeField] DictionaryEx<string, FlexibleValue.Value> _test;


    [System.Serializable]
    class Node
    {
        public string Name;
        public FlexibleValue.Value Value;
    }
    [SerializeField] List<Node> _parameters;

    //↓文字列検索は早いがシリアライズできない
    //blackboardを複製した時、空のリスト、マップが作成されてしまう
    Dictionary<string, FlexibleValue.Value> _dic;
    Dictionary<string, FlexibleValue.Value> Dic
    {
        get
        {
            //ノードを見に来る初回のタイミングで、listから値をコピー
            if (_dic == null)
            {
                _dic = new();
                foreach (var node in _parameters)
                {
                    _dic[node.Name] = node.Value;
                }

                //もう使わないのでリストの方は消す
                _parameters = null;

            }
            return _dic;
        }
    }

    //値を取得
    public T GetValue<T>(string key)
    {

        //var node = _parameters.Find(
        //     x => x.Name == key);//全要素取り出す→名前が同じやつを探す


        FlexibleValue.Value fv;

        if (Dic.TryGetValue(key, out fv) == false)
        {
            //↓指定したキーの値が存在しなかった場合
            return default;
        }
        return fv.GetValue<T>();

        ////if (node == null) return default;//型のデフォルト値が入る　0とかnullとか""とか

        ////return node.Value.GetValue<T>();
    }

    public void GetValue<T>(string key, ref T ret)
    {

        //var node = _parameters.Find(
        //     x => x.Name == key);//全要素取り出す→名前が同じやつを探す

        //if (node == null) return;//型のデフォルト値が入る　0とかnullとか""とか

        //ret = node.Value.GetValue<T>();


        FlexibleValue.Value fv;

        if (Dic.TryGetValue(key, out fv) == false)
        {
            //↓指定したキーの値が存在しなかった場合
            return;
        }
        ret = fv.GetValue<T>();

    }

    //値入れる
    //実行中にブラックボードに書き込みたいときに
    public void SetValue<T>(string key, T value)
    {
        //    var node = _parameters.Find(x => x.Name == key);
        ////見つからなかった：新規作成
        //if (node == null)
        //{
        //    Node newNode = new();
        //    newNode.Value = new FlexibleValue.Value();
        //    _parameters.Add(newNode);
        //}
        ////見つかった：上書き
        //else
        //{
        //    node.Value.SetValue(value);
        //}
        FlexibleValue.Value fv;

        //見つからないなら新規作成
        if (Dic.TryGetValue(key, out fv) == false)
        {
            //新規作成
            fv = new FlexibleValue.Value();
            //ディクショナリーに登録
            Dic[key] = fv;
        }

        //値を型に入れる
        fv.SetValue(value);
    }

    //シリアライズ直前に呼ばれる
    public void OnBeforeSerialize()
    {
        //dictionaryからlistに一旦変換（コピー）
        if (_dic != null)//マジの初回かもしれんのでnullチェック
        {
            //一旦全消去
            _parameters.Clear();

            foreach (var pair in _dic)
            {
                //ペアの情報からノードを作って
                //dictionaryの情報をリストにコピーしていく
                Node node = new();
                node.Name = pair.Key;
                node.Value = pair.Value;
                _parameters.Add(node);
            }


        }
    }

    //デシリアライズ直後に呼ばれる
    public void OnAfterDeserialize()
    {
        //dictionaryをすっきりきれいに
        _dic = null;
    }
}
