using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpUp : SkillTreeBase
{
    [SerializeField] private int hpUpValue;
    
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
            if (skill.necessarySkill.isSkillGet == true || skill.necessarySkill == null)
            {
                PlayerStatus.FindObjectOfType<PlayerStatus>().HpChange(hpUpValue);
                base.SkillGet(skill);
            }
        }
    }
}
