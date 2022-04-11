using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CDatingMeet : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected DataTimeLine m_LoveDataTimeLine = null;
    [SerializeField] protected DataTimeLine m_OverDataTimeLine = null;
 
    [SerializeField] protected GameObject m_PlayerObj = null;
    [SerializeField] protected CActorSetSkin m_PlayerSkin = null;
    [SerializeField] protected Transform m_TargetManObj = null;


    [SerializeField] protected bool m_Love = true;

    // ==================== SerializeField ===========================================

    protected ResultUI m_ResultUI = null;

    protected override void Awake()
    {
        base.Awake();


        m_PlayerSkin.SetUpdateSkinMat(StaticGlobalDel.BuffMyRoleData.DataSkinMat);

        if (StaticGlobalDel.SelectSkin != null)
        {
            m_Love = StaticGlobalDel.SelectSkin.DataID == StaticGlobalDel.TargetDataObj.LoveSkin.DataID;
            m_PlayerSkin.SetUpdateSkinObj(StaticGlobalDel.SelectSkin);
        }

        m_ResultUI = this.GetComponentInChildren<ResultUI>();

        
        GameObject lTempManObj = GameObject.Instantiate(StaticGlobalDel.TargetDataObj.Model, m_TargetManObj);
        lTempManObj.transform.localPosition = Vector3.zero;
        lTempManObj.transform.localScale = Vector3.one * 1.55f;

        DataTimeLine lTempDataTimeLine = null;
        lTempDataTimeLine = m_Love ? m_LoveDataTimeLine : m_OverDataTimeLine;
        lTempDataTimeLine.m_TimelineObj.SetActive(true);
       // ChangeActor(lTempDataTimeLine);
        lTempDataTimeLine.ChangeTrackObj("Target_Obj", lTempManObj);
    }

    //public void ChangeActor(DataTimeLine updateActor)
    //{
    //    var outputs = updateActor.m_TimelinePlayableAsset.outputs;
    //    foreach (var itm in outputs)
    //    {
    //        if (itm.streamName == "PlayerTrack")
    //            updateActor.m_TimelinePlayableDirector.SetGenericBinding(itm.sourceObject, m_PlayerObj);

    //    }
    //}

    public void EndFunc()
    {
        if (m_Love)
            Debug.Log("OKOK~~~");
        else
        {
            m_ResultUI.OverButton.onClick.AddListener(() => {
                StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });
            m_ResultUI.ShowFailedUI();
        }
    }

}
