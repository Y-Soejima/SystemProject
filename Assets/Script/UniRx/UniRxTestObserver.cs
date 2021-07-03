using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UniRxTestObserver : MonoBehaviour
{
    Subject<int> subject = new Subject<int>();
    // Start is called before the first frame update
    void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_=> subject.Subscribe(x => Debug.Log(x)));
        
    }

    // Update is called once per frame
    void Update()
    {
        subject.OnNext(100);
        subject.OnNext(50);
    }
}
