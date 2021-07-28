using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ShopBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick(int value)
    {
        base.OnClick(base.m_value);
    }
}
