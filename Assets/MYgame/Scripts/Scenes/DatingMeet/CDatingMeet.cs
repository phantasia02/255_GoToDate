using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CDatingMeet : CScenesCtrlBase
{
    [System.Serializable]
    public class DataTimeLine
    {
        public GameObject       m_TimelineObj = null;
        public PlayableAsset    m_TimelinePlayableAsset = null;
        public PlayableDirector m_TimelinePlayableDirector = null;
    }


    // ==================== SerializeField ===========================================

    [SerializeField] protected DataTimeLine m_LoveDataTimeLine = null;
    [SerializeField] protected DataTimeLine m_OverDataTimeLine = null;
 
    [SerializeField] protected GameObject m_PlayerObj = null;


    [SerializeField] protected bool m_Love = true;

    // ==================== SerializeField ===========================================

    protected ResultUI m_ResultUI = null;
    CChangeScenes m_ChangeScenes = new CChangeScenes();

    protected override void Awake()
    {
        base.Awake();

        m_ResultUI = this.GetComponentInChildren<ResultUI>();

        DataTimeLine lTempDataTimeLine = null;

        lTempDataTimeLine = m_Love ? m_LoveDataTimeLine : m_OverDataTimeLine;
        lTempDataTimeLine.m_TimelineObj.SetActive(true);
        ChangeActor(lTempDataTimeLine);
    }

    public void ChangeActor(DataTimeLine updateActor)
    {
        var outputs = updateActor.m_TimelinePlayableAsset.outputs;
        foreach (var itm in outputs)
        {
            if (itm.streamName == "PlayerTrack")
            {
                Debug.Log("aaaaaaaaaaaaaaaaaa");
                updateActor.m_TimelinePlayableDirector.SetGenericBinding(itm.sourceObject, m_PlayerObj);
            }

        }
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
