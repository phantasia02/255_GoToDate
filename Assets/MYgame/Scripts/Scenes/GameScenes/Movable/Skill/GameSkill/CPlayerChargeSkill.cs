using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerChargeSkill : CSkillBase
{
    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;
    protected float m_CSkillRadius = 0.0f;
    public float CSkillRadius => m_CSkillRadius;
    public const float m_CSkillTime = 0.5f;


    public CPlayerChargeSkill(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_CDTime = 0.0f;

        m_MyPlayerMemoryShare = (CPlayerMemoryShare)m_MyMemoryShare;
        
        m_CSkillRadius = m_MyGameManager.HalfScreenWidth / 2.0f;
    }

    protected override bool ConfirmUseSkill()
    {
        Vector3 lTempMousePosV3 = Input.mousePosition - m_MyPlayerMemoryShare.m_DownMouseDownPos;

        lTempMousePosV3.z = 0.0f;
        float lTempMagnitude = lTempMousePosV3.magnitude;

        if (m_MyPlayerMemoryShare.m_ChargeCount.Value <= 0)
            return false;

        if (m_CSkillRadius > lTempMagnitude)
            return false;

        if (m_CSkillTime < (Time.realtimeSinceStartup - m_MyPlayerMemoryShare.m_DownTime))
            return false;

        float lTempDisRatio = Mathf.Min(lTempMagnitude / m_CSkillRadius, 2.5f);

        //Debug.Log("ConfirmUseSkill Use");
        lTempMousePosV3.z = lTempMousePosV3.y;
        lTempMousePosV3.y = 0.0f;
        m_MyPlayerMemoryShare.m_CtrlSkillBuffDir = lTempMousePosV3.normalized;
        m_MyPlayerMemoryShare.m_SkillTargetpos = m_MyPlayerMemoryShare.m_MyPlayer.transform.position + (m_MyPlayerMemoryShare.m_CtrlSkillBuffDir * (lTempDisRatio * 17.25f));
        m_MyPlayerMemoryShare.m_MyPlayer.SameStatusUpdate = true;

        m_MyPlayerMemoryShare.m_MyPlayer.SetChangState(CMovableStatePototype.EMovableState.eMove, 1);
        return true;
    }
}
