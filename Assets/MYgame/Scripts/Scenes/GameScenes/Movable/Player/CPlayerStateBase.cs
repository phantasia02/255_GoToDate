using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

public abstract class CPlayerStateBase : CStateActor
{
    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;


    public CPlayerStateBase(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_MyPlayerMemoryShare = (CPlayerMemoryShare)m_MyMemoryShare;
    }

    protected override void InState()
    {
    }

    protected override void updataState()
    {
    }

    //public override void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == (int)StaticGlobalDel.ELayerIndex.eWater)
    //        SetHitWater(collision);

    //}


    public void UpdateSpeed()
    {
        if (m_MyPlayerMemoryShare.m_TotleSpeed.Value != m_MyPlayerMemoryShare.m_TargetTotleSpeed)
        {
           // m_MyMemoryShare.m_TotleSpeed.Value = Mathf.MoveTowards(m_MyPlayerMemoryShare.m_TotleSpeed.Value, m_MyPlayerMemoryShare.m_TargetTotleSpeed, m_MyPlayerMemoryShare.m_AddSpeedSecond * Time.deltaTime);
            m_MyMemoryShare.m_TotleSpeed.Value = Mathf.Lerp(m_MyPlayerMemoryShare.m_TotleSpeed.Value, m_MyPlayerMemoryShare.m_TargetTotleSpeed, 5.0f * Time.deltaTime);

            if (Mathf.Abs(m_MyPlayerMemoryShare.m_TotleSpeed.Value - m_MyPlayerMemoryShare.m_TargetTotleSpeed) < 0.1f)
                m_MyMemoryShare.m_TotleSpeed.Value = m_MyMemoryShare.m_TargetTotleSpeed;
        }
    }


    public void UpdateDrag()
    {
        if (!m_MyPlayerMemoryShare.m_bDown)
            return;


        Vector3 lTempMouseDrag = UnityEngine.Input.mousePosition - m_MyPlayerMemoryShare.m_OldMouseDownPos;
        lTempMouseDrag.z = lTempMouseDrag.y;
        lTempMouseDrag.y = 0.0f;

        if (lTempMouseDrag.sqrMagnitude < Screen.width / 100.0f)
            return;

        m_MyPlayerMemoryShare.m_OldMouseDragDirNormal += lTempMouseDrag * 3.0f;
        m_MyPlayerMemoryShare.m_OldMouseDragDirNormal.y = 0.0f;

        m_MyPlayerMemoryShare.m_OldMouseDragDirNormal = Vector3.ClampMagnitude(m_MyPlayerMemoryShare.m_OldMouseDragDirNormal, m_MyPlayerMemoryShare.m_MyPlayer.MaxMoveDirSize);

        Vector3 lTempCurforward = Vector3.zero;
        lTempCurforward = Vector3.Lerp(m_MyPlayerMemoryShare.m_MyMovable.transform.forward, m_MyPlayerMemoryShare.m_OldMouseDragDirNormal, Time.fixedDeltaTime);
        //lTempCurforward = Vector3.RotateTowards(m_MyPlayerMemoryShare.m_MyMovable.transform.forward, m_MyPlayerMemoryShare.m_OldMouseDragDirNormal, Time.deltaTime * 6.0f, 2.0f);
        lTempCurforward.y = 0.0f;
        lTempCurforward.Normalize();
        m_MyPlayerMemoryShare.m_OldMouseDownPos = UnityEngine.Input.mousePosition;

        if (lTempCurforward == Vector3.zero)
            return;
        
        m_MyPlayerMemoryShare.m_MyTransform.forward = lTempCurforward;
    }
    
}
