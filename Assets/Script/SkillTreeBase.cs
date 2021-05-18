using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeBase : MonoBehaviour
{
    [SerializeField] bool[] isSkillGets = new bool[Enum.GetValues(typeof(SkillList)).Length];
    [SerializeField] private GameObject skillCell;
    int skillNum;
    bool isSkillGet;
    public enum SkillList
    {
        AttackUp,
        MagicAttackUp,
        HpUp,
        MpUp,
    }

    void Start()
    {
        for (int i = 0; i < isSkillGets.Length; i++)
        {

            isSkillGets[i] = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SkillGetChack(0, isSkillGets[0]);
            SkillGet();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SkillGetChack(2, isSkillGets[2]);
            SkillGet();
        }
    }

    void SkillGetChack(int skillNum, bool isSkillGet)
    {
        this.skillNum = skillNum;
        this.isSkillGet = isSkillGet;
    }

    void SkillGet()
    {
        if (skillNum == 0 && !isSkillGet)
        {
            isSkillGets[0] = true;
        }
        else if (skillNum == 2 && !isSkillGet && isSkillGets[0])
        {
            isSkillGets[2] = true;
        }
    }
}
