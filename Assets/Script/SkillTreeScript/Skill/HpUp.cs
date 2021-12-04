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
                PlayerStatus.FindObjectOfType<PlayerStatus>().HpChange(hpUpValue);
                base.SkillGet(skill);
            }
        }
    }
}
