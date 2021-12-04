using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int p_hp = 100;
    [SerializeField] private int p_mp = 100;
    [SerializeField] private int p_atk = 20;
    [SerializeField] private int p_matk = 20;
    [SerializeField] private int exp;
    [SerializeField] private int maxExp = 100;
    // Start is called before the first frame update

    public void HpChange(int value)
    {
        p_hp += value;
    }

    public void MpChange(int value)
    {
        p_mp += value;
    }

    public void AtkChange(int value)
    {
        p_atk += value;
    }

    public void MatkChange(int value)
    {
        p_matk += value;
    }

    public void ExpUp(int value)
    {
        exp += value;
        if(exp >= maxExp)
        {
            exp = exp - maxExp;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        maxExp = (int)(maxExp * 1.5);
        HpChange((int)(p_hp * 0.2));
        MpChange((int)(p_mp * 0.2));
        AtkChange((int)(p_atk * 0.2));
        MatkChange((int)(p_matk * 0.2));
    }
}
