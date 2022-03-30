using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CSelectRole : CScenesCtrlBase
{
    CChangeScenes m_ChangeScenes = new CChangeScenes();
    
    // ==================== SerializeField ===========================================

    // [SerializeField] protected Image            m_MugShot               = null;
    [SerializeField] protected CRoleMugShot[]      m_AllMugShot            = null;
    [SerializeField] protected TMP_InputField   m_InputName             = null;
    //[SerializeField] protected CUIButton        m_Next                  = null;
    [SerializeField] protected CUIButton        m_Change                = null;

    // ==================== SerializeField ===========================================
    protected int           m_CurIndexDataRole = -1;
    protected CDataRole[]   m_TempAllDataRole = null;

    private void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);

        m_TempAllDataRole = CGGameSceneData.SharedInstance.m_AllDataRole;

      //  UpdateCurShowImage(0);

        m_Change.gameObject.SetActive(false);

        m_InputName.onEndEdit.AddListener((string EndEdit) => {

            bool lTemp = m_InputName.text.Length != 0 && m_CurIndexDataRole != -1;

            m_Change.gameObject.SetActive(lTemp);
        });

        //m_Next.AddListener(()=> { UpdateCurShowImage(m_CurIndexDataRole + 1); });
        m_Change.AddListener(()=> {
            if (m_InputName.text.Length == 0)
                return;

            CSaveManager.m_status.m_MyRoleIndex = m_CurIndexDataRole;
            CSaveManager.m_status.m_MyName      = m_InputName.text;

            m_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
        });
    }

    public void UpdateCurShowImage(int lSetIndex)
    {
        if (lSetIndex >= m_TempAllDataRole.Length || lSetIndex < 0)
            return;

        if (m_CurIndexDataRole == lSetIndex)
            return;

        m_CurIndexDataRole = lSetIndex;

        foreach (var item in m_AllMugShot)
            item.PlayFoucsAnima(false);

        m_AllMugShot[m_CurIndexDataRole].PlayFoucsAnima(true);

        m_Change.gameObject.SetActive(m_InputName.text.Length != 0);
    }
}
