using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeBase : MonoBehaviour
{
    [SerializeField] private int skillNum;
    [SerializeField] public bool isSkillGet = false;
    [SerializeField] public SkillTreeBase necessarySkill;
    void Start()
    {
        
    }

    void Update()
    {

    }

    void SkillGetChack(int skillNum, bool isSkillGet)
    {
        this.skillNum = skillNum;
        this.isSkillGet = isSkillGet;
    }

    public virtual void SkillGet(SkillTreeBase skill)
    {
        skill.isSkillGet = true;
        //var button = skill.gameObject.GetComponentInChildren<Button>();
        //ColorBlock buttonColor = button.colors;
    }
}
