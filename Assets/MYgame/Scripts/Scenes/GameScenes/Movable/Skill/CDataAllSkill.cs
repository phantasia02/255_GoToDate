using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDataAllSkill
{
    protected List<CSkillBase> m_ListAllSkill = new List<CSkillBase>();
    public List<CSkillBase> ListAllSkill => m_ListAllSkill;

    public void UpdateSkill(float updatetime)
    {
        foreach (var item in m_ListAllSkill)
            item.UpdateSkill(updatetime);
    }
}
