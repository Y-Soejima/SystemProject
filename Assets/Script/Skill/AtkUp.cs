using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AtkUp : SkillTreeBase
{
    
    [SerializeField] private int atkUpValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SkillGet(SkillTreeBase skill)
    {
        if (skill.isSkillGet == false)
        {
            bool isGet = true;
            for (int i = 0; i < skill.necessarySkill.Length; i++)
            {
                if (skill.necessarySkill[i].isSkillGet == false)
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
