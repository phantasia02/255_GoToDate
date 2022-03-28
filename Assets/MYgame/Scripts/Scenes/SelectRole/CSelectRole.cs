using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CSelectRole : MonoBehaviour
{
    // ==================== SerializeField ===========================================
    [SerializeField] protected GameObject PrefabGameSceneData = null;

    [SerializeField] protected Image            m_MugShot               = null;
    [SerializeField] protected Image            m_FullBigPicture        = null;
    [SerializeField] protected TMP_InputField   m_InputName             = null;
    [SerializeField] protected CUIButton        m_Next                  = null;
    [SerializeField] protected CUIButton        m_Change                = null;

    // ==================== SerializeField ===========================================
    protected int           m_CurIndexDataRole = 0;
    protected CDataRole[]   m_TempAllDataRole = null;

    private void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);

        m_TempAllDataRole = CGGameSceneData.SharedInstance.m_AllDataRole;

        UpdateCurShowImage(0);

        m_Change.gameObject.SetActive(false);

        m_InputName.onEndEdit.AddListener((string EndEdit) => {
            m_Change.gameObject.SetActive(m_InputName.text.Length != 0);
        });

        m_Next.AddListener(()=> { UpdateCurShowImage(m_CurIndexDataRole + 1); });
        m_Change.AddListener(()=> {
            if (m_InputName.text.Length == 0)
                return;

            CSaveManager.m_status.m_MyRole = m_TempAllDataRole[m_CurIndexDataRole];
            CSaveManager.m_status.m_MyName = m_InputName.text;
        });
    }

    public void UpdateCurShowImage(int lNextIndex)
    {
        if (lNextIndex >= m_TempAllDataRole.Length)
            lNextIndex = 0;

        CDataRole lTempDataRole = m_TempAllDataRole[lNextIndex];
        m_MugShot.sprite = lTempDataRole.MugShot;
        m_FullBigPicture.sprite = lTempDataRole.FullBigPicture;

        m_CurIndexDataRole = lNextIndex;
    }
}
