using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//入力のベースとなるクラス

public class BaseInputProvider : MonoBehaviour
{
    //左軸の取得
    public virtual Vector2 GetAxisL() => Vector2.zero;

    //右軸取得
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //攻撃ボタン
    public virtual bool GetButtonAppeal() => false;

}
