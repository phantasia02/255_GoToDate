using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;
using System.Linq;
using System;

public class CDatingDress : CScenesCtrlBase
{
    [SerializeField] protected CUIButton[] m_AllChangeDatingDress = null;
    [SerializeField] protected CUIButton m_ApplyBtn            = null;
    [SerializeField] protected GameObject m_PlayerPosRef            = null;
    [SerializeField] protected DataTimeLine m_DataTimeLine = null;
    [SerializeField] protected CActorSetSkin m_PlayerSkin = null;

    protected List<CDataSkinChange> m_AllDataSkin = new List<CDataSkinChange>();
    protected CDataSkinChange m_CurSelectSkinChange = null;

    protected override void Awake()
    {
        base.Awake();

        CGGameSceneData lTempCGGameSceneData = CGGameSceneData.SharedInstance;
        List<CDataSkinChange> lTempAllSuitSkin = lTempCGGameSceneData.m_AllSuitSkin;

        //StaticGlobalDel.TargetDataObj;

        var rnd = new System.Random();
        List <CDataSkinChange> lTempRandomList = lTempAllSuitSkin.OrderBy(item => rnd.Next()).ToList();


        m_AllDataSkin.Clear();
        int LoveSkinID = StaticGlobalDel.TargetDataObj.LoveSkin.DataID;
        int randoID = -10;

        for (int i = 0; i < lTempRandomList.Count; i++)
        {
            randoID = lTempRandomList[i].DataID;
            if (LoveSkinID != randoID)
            {
                int ltempindex = m_AllDataSkin.Count;
                m_AllDataSkin.Add(lTempRandomList[i]);
                if (m_AllDataSkin.Count == 2)
                    break;
            }
        }

        m_AllDataSkin.Add(StaticGlobalDel.TargetDataObj.LoveSkin);
        rnd = new System.Random();
        List<CDataSkinChange> randomized2 = m_AllDataSkin.OrderBy(item => rnd.Next()).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_AllChangeDatingDress.Length; i++)
            InitButtonCall(i);


        m_PlayerSkin.SetUpdateSkinMat(StaticGlobalDel.BuffMyRoleData.DataSkinMat);
        m_ApplyBtn.AddListener(() => {

            //SelectSkin
            StaticGlobalDel.SelectSkin = m_CurSelectSkinChange;
            StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameDatingMeet);
            Debug.Log("OKOK");

        });
    }

    public void InitButtonCall(int index)
    {
        if (0 > index || index >= m_AllChangeDatingDress.Length)
            return;

        RectTransform lTempRectTransform = m_AllChangeDatingDress[index].MyRectTransform;
        m_AllChangeDatingDress[index].AddListener(() =>
        {
            m_ApplyBtn.gameObject.SetActive(true);
            Vector2 lTempVector2 = lTempRectTransform.anchoredPosition;
            lTempVector2.y -= lTempRectTransform.sizeDelta.y / 2.0f;
            m_ApplyBtn.MyRectTransform.anchoredPosition = lTempVector2;

            m_PlayerSkin.SetUpdateSkinObj(m_AllDataSkin[index]);
            m_CurSelectSkinChange = m_AllDataSkin[index];

            Debug.Log($"m_AllDataSkin[index].DataID = {m_AllDataSkin[index].DataID}");
            // Debug.Log($"InitButtonCall = {index}");
        });
    }
}
