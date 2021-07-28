using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class EventShop : MonoBehaviour
{
    [SerializeField] int money;
    public Subject<int> m_shopEvent = new Subject<int>();
    // Start is called before the first frame update
    void Start()
    {
        m_shopEvent.Subscribe(value => Buy(value));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Buy(int value)
    {
        money -= value;
    }

}
