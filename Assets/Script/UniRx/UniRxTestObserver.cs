using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UniRxTestObserver : MonoBehaviour
{
    /// <summary>
    /// イベント（UniRx）の宣言
    /// </summary>
    Subject<int> subject = new Subject<int>();
    // Start is called before the first frame update
    void Start()
    {
        int a = 0;
        //イベントの登録
        subject.Subscribe(_ => Debug.Log(a));
        //イベントを発行
        subject.OnNext(a);
        //1秒ごとにイベントを発行
        Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => subject.OnNext(a++));
        //subject.OnNext(x);
        //コルーチンを上から順に発行（実行）
        Observable.FromCoroutine(CoroutineA)
        .SelectMany(CoroutineB)
        .SelectMany(CoroutineA)//SelectManyで合成可能
        .Subscribe(_ => Debug.Log("All coroutine finished"));
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    IEnumerator CoroutineA()
    {
        Debug.Log("CoroutineA start");
        yield return new WaitForSeconds(3);
        Debug.Log("CoroutineA finished");
    }

    IEnumerator CoroutineB()
    {
        Debug.Log("CoroutineB start");
        yield return new WaitForSeconds(1);
        Debug.Log("CoroutineB finished");
    }
}
