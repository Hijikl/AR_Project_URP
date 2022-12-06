using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�B�N�V���i���[�ƃ��X�g�̕ϊ����s����N���X
/// �V���A���C�Y�ł���悤�ɂȂ�
/// </summary>
[System.Serializable]//�V���A���C�Y�ł���悤��
public class DictionaryEx<Tkey, TValue> : Dictionary<Tkey, TValue>//Dictionary���p��
    , ISerializationCallbackReceiver//�V���A���C�Y�A�f�V���A���C�Y�O�ɏ���������
    ,IEnumerable<KeyValuePair<Tkey, TValue>>
{
    [System.Serializable]
    class Node
    {
        public Tkey Key;
        public TValue Value;
    }
    [SerializeField] List<Node> _list;

    //Dic���X�V�ς��i�_�[�e�B�t���O�j
    bool _updateDictionary = false;

    //List����Dictionary�ɃR�s�[
    void ListToDic()
    {
        //���Ƀ��X�g���X�V����Ă���X���[
        if (_updateDictionary) return;

        //Dic���e�i�����j�̃N���A
        Clear();

        foreach (var node in _list)
        {
            base.Add(node.Key, node.Value);
        }

        //�X�V���ꂽ�t���O
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

    //�L�[�擾
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
        //Dic���ύX����ĂȂ��Ȃ�X�L�b�v
        if (_updateDictionary == false) return;
        _updateDictionary = false;

        //Dic��list
        if (_list != null) _list = new();//list��null�̏ꍇ�V�������

        _list.Clear();
        foreach (var pair in this)
        {
            Node node = new();
            node.Key = pair.Key;
            node.Value = pair.Value;
            _list.Add(node);
        }
    }

    //�f�V���A���C�Y��ɂ���������
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.Clear();
    }

    /// <summary>
    /// �v���p�e�B
    /// </summary>
    /// <param name="key">�L�[</param>
    /// <returns>�l</returns>
    public new TValue this[Tkey key]//c++�̃I�y���[�^�[�݂����Ȃ�
    {
        get
        {
            ListToDic();
            return base[key];
        }
        set
        {
            //�l�����

            ListToDic();
            base[key] = value;
        }
    }

}
