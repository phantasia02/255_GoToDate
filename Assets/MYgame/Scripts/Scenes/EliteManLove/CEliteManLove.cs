using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CEliteManLove : CScenesChangChar
{
    public enum EState
    {
        eStart = 0,

        eKissGameStart = 1,
        eKissGame = 2,
        eKissGameWin = 3,
        eMax
    }
    // ==================== SerializeField ===========================================

    [SerializeField] protected CanvasGroup m_UIKGAll = null;
    [SerializeField] protected CLoveUIDegreeCompletion m_UIKGLove = null;
    [SerializeField] protected CUIButton m_UIKGLovePressButton = null;
    [SerializeField] protected CUIImage m_UIKGShowImage = null;
    [SerializeField] protected GameObject m_LoveFxLoopObj = null;

    [SerializeField] protected DataTimeLine m_LoveDataTimeLine = null;
    // ==================== SerializeField ===========================================

    protected GameObject m_ManMouth = null;
    protected ResultUI m_ResultUI = null;

    readonly public float m_UIKGLoveMaxTime = 3.0f;
    protected float m_UIKGLoveCurTime = 0.0f;
    protected bool m_KGPress = false;

    protected override void Awake()
    {
        base.Awake();

        m_ResultUI = this.GetComponentInChildren<ResultUI>();
        m_ManMouth = GameObject.FindWithTag(StaticGlobalDel.TagManMouth);
        m_LoveDataTimeLine.ChangeTrackObj("Target_Obj", m_ManObj);
    }

    private void Start()
    {
        // ================== Update =========================
        var lTempUpdateAs = this.UpdateAsObservable();
        lTempUpdateAs.Where(X => OBStateVal().Value == EState.eKissGame)
           .Subscribe(framcount =>
           {
               float lTempUIKGLoveCurTime = m_KGPress ? m_UIKGLoveCurTime + Time.deltaTime : m_UIKGLoveCurTime - Time.deltaTime;
               if (lTempUIKGLoveCurTime > m_UIKGLoveMaxTime)
               {
                   lTempUIKGLoveCurTime = m_UIKGLoveMaxTime;
                   m_OBState.Value = EState.eKissGameWin;
               }
               else if (lTempUIKGLoveCurTime < 0.0f)
                   lTempUIKGLoveCurTime = 0.0f;

               m_UIKGLoveCurTime = lTempUIKGLoveCurTime;
               m_UIKGLove.SetLoveProgressionVal(m_UIKGLoveCurTime / m_UIKGLoveMaxTime);
           }).AddTo(this);


        // ==============================

        OBStateVal().Where(_ => _ == EState.eKissGameStart)
        .Subscribe(X => {

             Sequence lTempSequence = DOTween.Sequence()
                  .AppendCallback(() => {
                      m_UIKGAll.DOFade(1.0f, 0.4f);
                  })
                  .AppendInterval(0.5f)
                  .AppendCallback(() =>
                  {
                      Vector3 lTempUIKissPos = Camera.main.WorldToScreenPoint(m_ManMouth.transform.position);
                      m_UIKGLovePressButton.MyRectTransform.position = lTempUIKissPos;
                      m_UIKGShowImage.MyRectTransform.position = lTempUIKissPos;
                      
                      
                      m_OBState.Value = EState.eKissGame;
                  });
        }).AddTo(this);

        OBStateVal().Where(_ => _ == EState.eKissGameWin)
            .Subscribe(X => {
                m_ResultUI.NextButton.onClick.AddListener(() =>
                {
                    StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
                });
                m_UIKGLovePressButton.gameObject.SetActive(false);
                m_UIKGShowImage.gameObject.SetActive(false);
                m_LoveFxLoopObj.transform.position = Vector3.Lerp(m_ManMouth.transform.position, Camera.main.transform.position, 0.5f);
                m_LoveFxLoopObj.SetActive(true);
                m_ResultUI.ShowSuccessUI(0.5f);
                m_UIKGAll.gameObject.SetActive(false);

            }).AddTo(this);


    }

    public void KissPress(bool press)
    {
        m_KGPress = press;
        Debug.Log($"m_KGPress = {m_KGPress}");
    }


    public void KissSmallGameCallBack()
    {
        Debug.Log("KissSmallGameCallBack~~~~~~~");
        OBStateVal().Value = EState.eKissGameStart;
    }

    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EState> m_OBState = new ReactiveProperty<EState>(EState.eStart);

    public UniRx.ReactiveProperty<EState> OBStateVal()
    {
        return m_OBState ?? (m_OBState = new ReactiveProperty<EState>(EState.eStart));
    }

    // ===================== UniRx ======================
}
