using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MYgame.Scripts.Scenes.GameScenes.Data;
using DG.Tweening;

public class CSelectRole : CScenesCtrlBase
{
   // CChangeScenes m_ChangeScenes = new CChangeScenes();
    
    // ==================== SerializeField ===========================================

    [SerializeField] protected Image                m_focus                 = null;
    [SerializeField] protected CRoleMugShot[]       m_AllMugShot            = null;
    [SerializeField] protected TMP_InputField       m_InputName             = null;
    //[SerializeField] protected CUIButton        m_Next                  = null;
    [SerializeField] protected CUIButton            m_Confirn                = null;
    //[SerializeField] protected GameObject           m_StartUI               = null;
    // ==================== SerializeField ===========================================
    protected int           m_CurIndexDataRole = -1;
    protected CDataRole[]   m_TempAllDataRole = null;

    protected override void Awake()
    {
        base.Awake();

        m_TempAllDataRole = CGGameSceneData.SharedInstance.m_AllDataRole;

        //DOTween.Sequence()
        //.AppendInterval(1.0f)
        //.AppendCallback(() => {
        //    m_StartUI.SetActive(false);
        //});
        //  UpdateCurShowImage(0);

        m_Confirn.EnableButton(false);

        m_InputName.onEndEdit.AddListener((string EndEdit) => {

            bool lTemp = m_InputName.text.Length != 0 && m_CurIndexDataRole != -1;

            m_Confirn.gameObject.SetActive(lTemp);
        });

        //m_Next.AddListener(()=> { UpdateCurShowImage(m_CurIndexDataRole + 1); });
        m_Confirn.AddListener(()=> {

            CSaveManager.m_status.m_MyRoleIndex = m_CurIndexDataRole;

            StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
        });
    }

    public void UpdateCurShowImage(int lSetIndex)
    {
        if (lSetIndex >= m_TempAllDataRole.Length || lSetIndex < 0)
            return;

        if (m_CurIndexDataRole == lSetIndex)
            return;

        m_CurIndexDataRole = lSetIndex;

        //foreach (var item in m_AllMugShot)
        //    item.PlayFoucsAnima(false);

        //m_AllMugShot[m_CurIndexDataRole].PlayFoucsAnima(true);

        m_Confirn.EnableButton(true);

        m_focus.gameObject.SetActive(true);
        m_focus.rectTransform.anchoredPosition = m_AllMugShot[m_CurIndexDataRole].MyRectTransform.anchoredPosition;
    }
}
