using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CDeshTutorialWindow : MonoBehaviour
{

    [System.Serializable]
    public class DataShowObj
    {
        public GameObject m_Animat = null;
        public string m_Title = "";
        public GameObject m_Button = null;
    }
    // ==================== SerializeField ===========================================

    [SerializeField] protected CUIButton m_NextBtn  = null;
    [SerializeField] protected CUIButton m_CloseBtn = null;
    [SerializeField] protected CUIText  m_FramTitleText = null;
    [SerializeField]  protected DataShowObj[] m_AllShowObj = null;

    // ==================== SerializeField ===========================================
    protected int m_CurShowObjIndex = 0;

    private void Awake()
    {
        m_NextBtn.AddListener(() => {SetShowCurIndex(m_CurShowObjIndex + 1);});
        m_CloseBtn.AddListener(() => { Destroy(this.gameObject); });
        //this.transform.childCount
        SetShowCurIndex(m_CurShowObjIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetShowCurIndex(int index)
    {
        if (index < 0 || index >= m_AllShowObj.Length)
            return;

        foreach (var item in m_AllShowObj)
        {
            item.m_Animat.SetActive(false);
            item.m_Button.SetActive(false);
        }

        m_AllShowObj[index].m_Animat.SetActive(true);
        m_AllShowObj[index].m_Button.SetActive(true);
        m_FramTitleText.SetText(m_AllShowObj[index].m_Title);
        m_CurShowObjIndex = index;
    }

    public void OnDestroy()
    {
        CloseWindow.OnNext(Unit.Default);
    }

    // ===================== UniRx ======================

    public Subject<Unit> CloseWindow = new Subject<Unit>();
    public Subject<Unit> ObserveClose() { return CloseWindow ?? (CloseWindow = new Subject<Unit>()); }

    // ===================== UniRx ======================
}
