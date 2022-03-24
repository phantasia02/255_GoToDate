using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSkillBase : CExternalOBJBase
{
    protected float m_CDTime = 0.5f;
    public virtual float CDTime => 0.5f;

    protected float m_CurCDTime = 0.0f;
    public float CurCDTime => m_CurCDTime;

    protected bool m_UseFrame = false;

    public CSkillBase(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
    }

    public void UpdateSkill(float updatetime)
    {
        float oldCdtime = m_CurCDTime;
        m_CurCDTime -= updatetime;

        if (oldCdtime > 0.0f && m_CurCDTime <= 0.0f)
            SaveBuffCtrl();
    }

    public bool UseSkill()
    {
        if (CurCDTime > 0.0f)
            return false;

        if (ConfirmUseSkill())
        {
            ResetCD();
            return true;
        }

        return false;
    }

    public virtual void CDClear()
    {
        m_CurCDTime = 0.0f;
    }

    public void ResetCD()
    {
        m_CurCDTime = CDTime;
    }

    public virtual void SaveBuffCtrl(){}

    protected virtual bool ConfirmUseSkill(){return true;}
}
