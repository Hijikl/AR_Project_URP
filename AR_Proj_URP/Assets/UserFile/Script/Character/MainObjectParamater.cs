using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//  ゲームに登場するオブジェクトのトップにつけるコンポーネント
//  そのオブジェクトが何者か、どういうやつかを見る
/// </summary>
public class MainObjectParamater : MonoBehaviour
{
    //チームID
    //オブジェクトが何者かを知るためのやつ
    //敵か味方かとか
    [SerializeField]
    int teamID = 0;

    public int TeamID
    { get { return teamID; } set { teamID = value; } }

    //名前
    [SerializeField]
    string _name;
    public string Name => _name;

    //ヒットストップ時間
    public float HitStopTimer { get; set; } = 0;

    //更新処理
    void Update()
    {
        //ヒットストップ時間の経過
        HitStopTimer -= Time.deltaTime;

        if(HitStopTimer <= 0)
        {
            HitStopTimer = 0;
        }
    }
}
