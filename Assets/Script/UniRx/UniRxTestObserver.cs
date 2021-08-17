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
    int a = 0;
    Subject<int> subject = new Subject<int>();
    Subject<string> subject1 = new Subject<string>();
    Subject<Unit> Subject3 = new Subject<Unit>();
    Subject<Unit> subject4 = new Subject<Unit>();

    public IObservable<Unit> Observable1 => Subject3;
    // Start is called before the first frame update
    void Start()
    {
        //イベントの登録
        subject.Subscribe(_ => Debug.Log(a++));
        subject1.Subscribe(str => Text(str));
        subject4.Subscribe(_ => VirtualUpDate());
        subject4.OnNext(Unit.Default);

        //イベントを発行

        //subject1.OnNext("AAA");

        //1秒ごとにイベントを
        //Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ => subject.OnNext(a++));

        //コルーチンを上から順に発行（実行）
        //Observable.FromCoroutine(CoroutineA)
        //.SelectMany(CoroutineB)
        //.SelectMany(CoroutineA)//SelectManyで合成可能
        //.Subscribe(_ => Debug.Log("All coroutine finished"));
    }

    // Update is called once per frame
    void Update()
    {
        //subject.OnNext(a);
        //subject.OnCompleted();
        //Subject3.OnNext(Unit.Default);
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

    void Text(string str)
    {
        Debug.Log(str);
    }

    void VirtualUpDate()
    {
        while(true)
        {
            Subject3.OnNext(Unit.Default);
        }
    }
}
