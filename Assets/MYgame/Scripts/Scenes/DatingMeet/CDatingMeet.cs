using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CDatingMeet : CScenesChangChar
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected DataTimeLine m_LoveDataTimeLine = null;
    [SerializeField] protected DataTimeLine m_OverDataTimeLine = null;
 

    [SerializeField] protected bool m_Love = true;

    // ==================== SerializeField ===========================================

    protected ResultUI m_ResultUI = null;

    protected override void Awake()
    {
        base.Awake();

        if (StaticGlobalDel.SelectSkin != null)
            m_Love = StaticGlobalDel.SelectSkin.DataID == StaticGlobalDel.TargetDataObj.LoveSkin.DataID;

        DataTimeLine lTempDataTimeLine = null;
        lTempDataTimeLine = m_Love ? m_LoveDataTimeLine : m_OverDataTimeLine;
        lTempDataTimeLine.m_TimelineObj.SetActive(true);
        lTempDataTimeLine.ChangeTrackObj("Target_Obj", m_ManObj);

        m_ResultUI = this.GetComponentInChildren<ResultUI>();
    }

    public void EndFunc()
    {
        if (m_Love)
        {
            m_ResultUI.NextButton.onClick.AddListener(() =>
            {
                StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });
            m_ResultUI.ShowSuccessUI();
        }
        else
        {
            m_ResultUI.OverButton.onClick.AddListener(() =>
            {
                StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });
            m_ResultUI.ShowFailedUI();
        }
    }

}
