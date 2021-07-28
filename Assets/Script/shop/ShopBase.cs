using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBase : MonoBehaviour
{
    [SerializeField] GameObject m_gameobject = default;
    [SerializeField] protected int m_value;
    [SerializeField] EventShop m_ev;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnClick(int value)
    {
        m_ev.m_shopEvent.OnNext(value);
    }
}
