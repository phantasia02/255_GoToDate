using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CHistoryWindow : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    [SerializeField] GameObject m_ShowObj = null;

    [Header("RLButton")]
    [SerializeField] protected CUIButton    m_LeftBtn                       = null;
    [SerializeField] protected CUIButton    m_RightBtn                      = null;
    [SerializeField] protected CUIButton    m_Close                         = null;
    [SerializeField] protected CUIText      m_HistoryHighScore              = null;
    [SerializeField] protected CanvasGroup  m_CompleteBuildingCountGroup    = null;
    [SerializeField] protected CUIText      m_CompleteBuildingCount         = null;

    // ==================== SerializeField ===========================================


    protected Tween m_CompleteBuildingCountGroupTween = null;
    CChangeScenes m_ChangeScenes = new CChangeScenes();

    // ===================== UniRx ======================
    public Subject<Unit> CloseWindow = new Subject<Unit>();
    // ===================== UniRx ======================

    public readonly float m_Animatime = CHistoryScenes.CFMoveMaxTime / 2.0f;

    private void Awake()
    {
        m_ShowObj.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Close.AddListener(CloseShowUI);
    }

    //public void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;
    //}

    public void CloseShowUI()
    {
        //if (m_CloseShowUI)
        //    return;

        //m_CloseShowUI = true;
        //m_ShowObj.SetActive(false);
        m_ChangeScenes.RemoveScenes(StaticGlobalDel.g_HistorvWindowScenes);
    }

    public void AddLeftBtnListener(UnityAction call){m_LeftBtn.AddListener(call);}
    public void AddRightBtnListener(UnityAction call){ m_RightBtn.AddListener(call);}

    public void SetHistoryHighScore(int Score){m_HistoryHighScore.SetNumber(Score);}

    public void OnDestroy()
    {
        CloseWindow.OnNext(Unit.Default);
    }

    public void SetCompleteBuildingCount(string strtext, bool anima = true)
    {
        //if (!anima)
        //{
            m_CompleteBuildingCount.SetText(strtext);
        //    return;
        //}

        //m_CompleteBuildingCountGroupTween = m_CompleteBuildingCountGroup.DOFade(0.0f, m_Animatime).SetEase(Ease.Linear);
        //m_CompleteBuildingCountGroupTween.onComplete = () => { }
    }

    // ===================== UniRx ======================

    public Subject<Unit> ObserveClose(){return CloseWindow ?? (CloseWindow = new Subject<Unit>());}

    // ===================== UniRx ======================
}
