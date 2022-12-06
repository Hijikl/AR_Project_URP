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

    //�������񌟍��͑������V���A���C�Y�ł��Ȃ�
    //blackboard�𕡐��������A��̃��X�g�A�}�b�v���쐬����Ă��܂�
    Dictionary<string, FlexibleValue.Value> _dic;
    Dictionary<string, FlexibleValue.Value> Dic
    {
        get
        {
            //�m�[�h�����ɗ��鏉��̃^�C�~���O�ŁAlist����l���R�s�[
            if (_dic == null)
            {
                _dic = new();
                foreach (var node in _parameters)
                {
                    _dic[node.Name] = node.Value;
                }

                //�����g��Ȃ��̂Ń��X�g�̕��͏���
                _parameters = null;

            }
            return _dic;
        }
    }

    //�l���擾
    public T GetValue<T>(string key)
    {

        //var node = _parameters.Find(
        //     x => x.Name == key);//�S�v�f���o�������O���������T��


        FlexibleValue.Value fv;

        if (Dic.TryGetValue(key, out fv) == false)
        {
            //���w�肵���L�[�̒l�����݂��Ȃ������ꍇ
            return default;
        }
        return fv.GetValue<T>();

        ////if (node == null) return default;//�^�̃f�t�H���g�l������@0�Ƃ�null�Ƃ�""�Ƃ�

        ////return node.Value.GetValue<T>();
    }

    public void GetValue<T>(string key, ref T ret)
    {

        //var node = _parameters.Find(
        //     x => x.Name == key);//�S�v�f���o�������O���������T��

        //if (node == null) return;//�^�̃f�t�H���g�l������@0�Ƃ�null�Ƃ�""�Ƃ�

        //ret = node.Value.GetValue<T>();


        FlexibleValue.Value fv;

        if (Dic.TryGetValue(key, out fv) == false)
        {
            //���w�肵���L�[�̒l�����݂��Ȃ������ꍇ
            return;
        }
        ret = fv.GetValue<T>();

    }

    //�l�����
    //���s���Ƀu���b�N�{�[�h�ɏ������݂����Ƃ���
    public void SetValue<T>(string key, T value)
    {
        //    var node = _parameters.Find(x => x.Name == key);
        ////������Ȃ������F�V�K�쐬
        //if (node == null)
        //{
        //    Node newNode = new();
        //    newNode.Value = new FlexibleValue.Value();
        //    _parameters.Add(newNode);
        //}
        ////���������F�㏑��
        //else
        //{
        //    node.Value.SetValue(value);
        //}
        FlexibleValue.Value fv;

        //������Ȃ��Ȃ�V�K�쐬
        if (Dic.TryGetValue(key, out fv) == false)
        {
            //�V�K�쐬
            fv = new FlexibleValue.Value();
            //�f�B�N�V���i���[�ɓo�^
            Dic[key] = fv;
        }

        //�l���^�ɓ����
        fv.SetValue(value);
    }

    //�V���A���C�Y���O�ɌĂ΂��
    public void OnBeforeSerialize()
    {
        //dictionary����list�Ɉ�U�ϊ��i�R�s�[�j
        if (_dic != null)//�}�W�̏��񂩂������̂�null�`�F�b�N
        {
            //��U�S����
            _parameters.Clear();

            foreach (var pair in _dic)
            {
                //�y�A�̏�񂩂�m�[�h�������
                //dictionary�̏������X�g�ɃR�s�[���Ă���
                Node node = new();
                node.Name = pair.Key;
                node.Value = pair.Value;
                _parameters.Add(node);
            }


        }
    }

    //�f�V���A���C�Y����ɌĂ΂��
    public void OnAfterDeserialize()
    {
        //dictionary���������肫�ꂢ��
        _dic = null;
    }
}
