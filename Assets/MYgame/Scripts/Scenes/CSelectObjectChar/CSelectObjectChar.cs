using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;
using DG.Tweening;

public class CSelectObjectChar : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected CUITextImage m_SelectRoleData = null;
    [SerializeField] protected CUITextImage m_ShoObjChar = null;

    [SerializeField] protected CUIButton m_OK = null;
    [SerializeField] protected CUIButton m_No_X = null;
    [SerializeField] protected GameObject m_StartUI = null;

    // ==================== SerializeField ===========================================

    protected CDataObjChar m_CurShowDataObjChar = null;
    protected int m_CurShowDataObjCharIndex = 0;
    public CDataObjChar CurShowDataObjChar
    {
        get
        {
            if (m_CurShowDataObjChar == null)
            {
                if (0 > m_CurShowDataObjCharIndex || m_CurShowDataObjCharIndex >= StaticGlobalDel.StageData.AllDataObjChar.Count)
                    return null;

                m_CurShowDataObjChar = StaticGlobalDel.StageData.AllDataObjChar[m_CurShowDataObjCharIndex];
            }

            return m_CurShowDataObjChar;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        StaticGlobalDel.TargetDataObj = null;


        DOTween.Sequence()
        .AppendInterval(1.0f)
        .AppendCallback(() => {
            m_StartUI.SetActive(false);
        });

        UpdateCurDataObjChar(0);
    }

    public void Start()
    {
        CDataRole lTempMyRole = StaticGlobalDel.BuffMyRoleData;

        m_SelectRoleData.SetSprite(lTempMyRole.MugShot);

        m_No_X.AddListener(() => {
            UpdateCurDataObjChar(m_CurShowDataObjCharIndex + 1);
        });

        m_OK.AddListener(() => {
            
            CSaveManager.m_status.m_ObjTargetIndex = m_CurShowDataObjCharIndex;
            //BuffTargetObj

            StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameChatroom);
        });

        m_ShoObjChar.SetText(StaticGlobalDel.StageData.PD);
    }

    public void UpdateCurDataObjChar(int lNextIndex)
    {
        if (lNextIndex >= StaticGlobalDel.StageData.AllDataObjChar.Count)
            lNextIndex = 0;

        m_CurShowDataObjCharIndex = lNextIndex;
        m_CurShowDataObjChar = null;

        m_ShoObjChar.SetSprite(CurShowDataObjChar.MugShot);

    }
}
