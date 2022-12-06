using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ディクショナリーとリストの変換を行えるクラス
/// シリアライズできるようになる
/// </summary>
[System.Serializable]//シリアライズできるように
public class DictionaryEx<Tkey, TValue> : Dictionary<Tkey, TValue>//Dictionaryを継承
    , ISerializationCallbackReceiver//シリアライズ、デシリアライズ前に処理をする
    ,IEnumerable<KeyValuePair<Tkey, TValue>>
{
    [System.Serializable]
    class Node
    {
        public Tkey Key;
        public TValue Value;
    }
    [SerializeField] List<Node> _list;

    //Dicが更新済か（ダーティフラグ）
    bool _updateDictionary = false;

    //ListからDictionaryにコピー
    void ListToDic()
    {
        //既にリストが更新されてたらスルー
        if (_updateDictionary) return;

        //Dic内容（自分）のクリア
        Clear();

        foreach (var node in _list)
        {
            base.Add(node.Key, node.Value);
        }

        //更新されたフラグ
        _updateDictionary = true;

    }

   

    new IEnumerator<KeyValuePair<Tkey,TValue>> GetEnumerator()
    {
        ListToDic();
        return base.GetEnumerator();
    }


    public new void Add(Tkey key, TValue value)
    {
        ListToDic();
        base.Add(key, value);
    }


    public new bool Remove(Tkey key)
    {
        ListToDic();
        return base.Remove(key);
    }

    //キー取得
    public new KeyCollection Keys
    {
        get
        {
            ListToDic();
            return base.Keys;
        }
    }

    public new ValueCollection Values
    {
        get
        {
            ListToDic();
            return base.Values;
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        //Dicが変更されてないならスキップ
        if (_updateDictionary == false) return;
        _updateDictionary = false;

        //Dic→list
        if (_list != null) _list = new();//listがnullの場合新しく作る

        _list.Clear();
        foreach (var pair in this)
        {
            Node node = new();
            node.Key = pair.Key;
            node.Value = pair.Value;
            _list.Add(node);
        }
    }

    //デシリアライズ後にしたいこと
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.Clear();
    }

    /// <summary>
    /// プロパティ
    /// </summary>
    /// <param name="key">キー</param>
    /// <returns>値</returns>
    public new TValue this[Tkey key]//c++のオペレーターみたいなの
    {
        get
        {
            ListToDic();
            return base[key];
        }
        set
        {
            //値入れる

            ListToDic();
            base[key] = value;
        }
    }

}
