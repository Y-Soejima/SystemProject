using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ShopBase
{

    public override void OnClick(int value)
    {
        base.OnClick(base.m_value);
    }
}
