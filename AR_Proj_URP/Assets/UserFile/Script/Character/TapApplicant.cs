using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージ適用インターフェイス
/// これを取り付けることでダメージ判定を行える
/// 継承を使わずともそういうことができちゃう
/// </summary>
public interface ITapApplicate
{
    //↓これだとコピーができちゃう（構造体は値型なので）
     bool ApplyTap();

  
    //MainObjectParameter取得（何者かを知る）
    MainObjectParamater MainObjParam { get; }

}

public class TapApplicant : MonoBehaviour
{

    [SerializeField]
    MainObjectParamater _mainObjParam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //仮
        //タップ判定
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            //タップされた
            if (Physics.Raycast(ray, out hit))
            {
                var tapApp= hit.collider.gameObject.GetComponent<ITapApplicate>();
                if(tapApp != null)
                {
                    tapApp.ApplyTap();
                }

            }

        }
    }
}
