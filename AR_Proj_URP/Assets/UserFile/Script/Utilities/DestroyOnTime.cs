using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間経過で破壊するスクリプト
public class DestroyOnTime : MonoBehaviour
{

    [Header("破壊されるまでの時間")]
    [SerializeField] float _time;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
