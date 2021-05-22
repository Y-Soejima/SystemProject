using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class SkillTreeBase : MonoBehaviour
{
    [SerializeField] public bool isSkillGet = false;
    [SerializeField] public SkillTreeBase[] necessarySkill;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public virtual void SkillGet(SkillTreeBase skill)
    {
        skill.isSkillGet = true;
        //var button = skill.gameObject.GetComponentInChildren<Button>();
        //ColorBlock buttonColor = button.colors;
    }
}
