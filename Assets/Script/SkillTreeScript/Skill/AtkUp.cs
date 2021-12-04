using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AtkUp : SkillTreeBase
{
    
    [SerializeField] private int atkUpValue;

    public override void SkillGet(SkillTreeBase skill)
    {
        if (isSkillGet == false)
        {
            bool isGet = true;
            for (int i = 0; i < necessarySkill.Length; i++)
            {
                if (necessarySkill[i].isSkillGet == false)
                {
                    isGet = false;
                    break;
                }
            }
            if (isGet)
            {
                PlayerStatus.FindObjectOfType<PlayerStatus>().AtkChange(atkUpValue);
                base.SkillGet(skill);
            }
        }
    }
}
