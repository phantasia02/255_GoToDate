using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UniRx;

public class CReadyGameWindow : CSingletonMonoBehaviour<CReadyGameWindow>
{
    public enum EChildGroup
    {
        eTotal = 0,
        eStartPrompt = 1,
        eMax
    };


    //[SerializeField] Button m_OptionButton;
    //[SerializeField] Button m_SkinButton;
    // ==================== SerializeField ===========================================

    [SerializeField] GameObject m_ShowObj = null;
    [SerializeField] CUIText m_PassConditionText = null;
    [VarRename(EChildGroup.eMax)]
    [SerializeField] GameObject[] m_AllChildGroup = null;
    
    // ==================== SerializeField ===========================================

    bool m_CloseShowUI = false;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void ShowWindowUI()
    {
        m_ShowObj.SetActive(true);
    }


    public void SetPassCondition(int CurNumber, int ConditionNumber)
    {
        m_PassConditionText.SetText($"{CurNumber}/{ConditionNumber}");
    }

    public bool GetShow() { return m_ShowObj.activeSelf; }

    public void CloseShowUI()
    {
        if (m_CloseShowUI)
            return;

        m_CloseShowUI = true;
        m_ShowObj.SetActive(false);
    }


    public void ShowChild(EChildGroup showGroup, bool show)
    {
        m_AllChildGroup[(int)showGroup].SetActive(show);
    }

    public bool IsShowChild(EChildGroup showGroup){return m_AllChildGroup[(int)showGroup].activeSelf;}
}
