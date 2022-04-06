using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CDatingMeet : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject m_WinTimeline = null;
    [SerializeField] protected GameObject m_OverTimeline = null;
    [SerializeField] protected bool m_Love = true;

    // ==================== SerializeField ===========================================

    protected ResultUI m_ResultUI = null;
    CChangeScenes m_ChangeScenes = new CChangeScenes();

    protected override void Awake()
    {
        base.Awake();

        m_ResultUI = this.GetComponentInChildren<ResultUI>();

        if (m_Love)
            m_WinTimeline.SetActive(true);
        else
            m_OverTimeline.SetActive(true);
    }

    public void EndFunc()
    {
        if (m_Love)
            Debug.Log("OKOK~~~");
        else
        {
            m_ResultUI.OverButton.onClick.AddListener(() => {
                m_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });
            m_ResultUI.ShowFailedUI();
        }
    }

}
