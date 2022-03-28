using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CSelectObjectChar : MonoBehaviour
{
    // ==================== SerializeField ===========================================
    [SerializeField] protected GameObject PrefabGameSceneData = null;

    [SerializeField] protected CUITextImage m_SelectRoleData = null;
    [SerializeField] protected CUITextImage m_ShoObjChar = null;

    [SerializeField] protected CUIButton m_OK = null;
    [SerializeField] protected CUIButton m_No_X = null;

    // ==================== SerializeField ===========================================

    protected StageData m_CurStageData = null;
    public StageData MyCurStageData
    {
        get
        {
            if (m_CurStageData == null)
            {
                CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;
                m_CurStageData = lTempGameSceneData.LevelToStageData(CSaveManager.m_status.m_LevelIndex);
            }

            return m_CurStageData;
        }
    }

    protected CDataObjChar m_CurShowDataObjChar = null;
    protected int m_CurShowDataObjCharIndex = 0;
    public CDataObjChar CurShowDataObjChar
    {
        get
        {
            if (m_CurShowDataObjChar == null)
            {
                if (MyCurStageData == null)
                    return null;

                if (0 > m_CurShowDataObjCharIndex || m_CurShowDataObjCharIndex >= MyCurStageData.AllDataObjChar.Count)
                    return null;

                m_CurShowDataObjChar = MyCurStageData.AllDataObjChar[m_CurShowDataObjCharIndex];
            }

            return m_CurShowDataObjChar;
        }
    }

    private void Awake()
    {

        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);



        UpdateCurDataObjChar(0);
    }

    public void Start()
    {
        CDataRole lTempMyRole = CGGameSceneData.SharedInstance.m_AllDataRole[CSaveManager.m_status.m_MyRoleIndex];

        m_SelectRoleData.SetSprite(lTempMyRole.MugShot);
        m_SelectRoleData.SetText(CSaveManager.m_status.m_MyName);

        m_No_X.AddListener(() => {
            UpdateCurDataObjChar(m_CurShowDataObjCharIndex + 1);
        });

        m_OK.AddListener(() => {
            Debug.Log("OKOK");
        });
    }

    public void UpdateCurDataObjChar(int lNextIndex)
    {
        if (lNextIndex >= MyCurStageData.AllDataObjChar.Count)
            lNextIndex = 0;

        m_CurShowDataObjCharIndex = lNextIndex;
        m_CurShowDataObjChar = null;

        m_ShoObjChar.SetSprite(CurShowDataObjChar.MugShot);
        m_ShoObjChar.SetText($"#{CurShowDataObjChar.PD}");
    }
}
