using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using MYgame.Scripts.Window;
using System;

public class CGameSceneWindow : CSingletonMonoBehaviour<CGameSceneWindow>
{
    public enum EMoveTotalImageState
    {
        eNull = 0,
        eOpenStart = 1,
        eMoveUpdate = 2,
        eMax
    }

    CChangeScenes m_ChangeScenes = new CChangeScenes();
    [SerializeField] protected GameObject m_ShowObj = null;
    public GameObject _ShowObj => m_ShowObj;
    [SerializeField] protected Button m_ResetButton = null;
    [SerializeField] protected Timer _timer = null;
    [SerializeField] protected CUIText m_Coin = null;
    [SerializeField] protected CUIText m_TotalMaxText = null;
    [SerializeField] protected CUIText m_TotalCurText = null;
    [SerializeField] [ColorUsage(true, true)] protected Color m_TCNTOutLineColor;
    [SerializeField] [ColorUsage(true, true)] protected Color m_TCPTOutLineColor;
    [SerializeField] protected CUITextImage m_SpeedIcon = null;
    [SerializeField] protected CUIText m_SpeedAddText = null;
    [SerializeField] protected GameObject m_SpeedTutorial = null;
    [SerializeField] protected Image m_CompleteBuildingMoveTotalImage = null;
    [SerializeField] protected CUIText m_BigTimeText = null;
    [SerializeField] protected Image m_TotalFramLight = null;
    [SerializeField] protected GameObject m_TotalGroupObj = null;
    [SerializeField] protected GameObject m_TutorialGroupObj = null;

    [SerializeField]
    [Tooltip("Total Fram Light curve")]
    protected AnimationCurve m_TotalFramLightCurve;

    protected int m_CurCoin = 0;
    public int CurCoinNumber => m_CurCoin;
    public GameObject TotalGroupObj => m_TotalGroupObj;
    public Timer timer => _timer;

    protected float m_CurFever = 0.0f;
    protected float m_TargetFever = 0.0f;
    protected int m_TempTotalCount = 0;
    protected RectTransform m_BigTimeRectTransform = null;
    protected RectTransform m_TotalCurTextRectTransform = null;
    protected Transform m_SpeedIconParent = null;
    protected readonly Color m_TotalFramLightStartColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    protected readonly Color m_TotalFramLightEndColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    protected Vector2 m_TotalCurRectTransformV2 = Vector3.zero;
    protected Tween m_TotalFramLightTween = null;
    protected Tween m_TotalCurTextAnima = null;
    protected Tween m_TotalCurTextScalAnima = null;
    protected Tween m_AddSpeedTextRectTransform = null;

    private void OnValidate()
    {
    }

    public void SetTemptextPos(Vector3 pos)
    {
    }

    private void Awake()
    {
        m_SpeedIconParent = m_SpeedIcon.transform.parent;

        m_ResetButton.onClick.AddListener(() =>
        {
            m_ChangeScenes.ResetScene();
        });

        m_BigTimeRectTransform = m_BigTimeText.GetComponent<RectTransform>();
        m_TotalCurTextRectTransform = m_TotalCurText.GetComponent<RectTransform>();
        m_TotalCurRectTransformV2 = m_TotalCurTextRectTransform.anchoredPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 lTempDefpos = m_BigTimeRectTransform.anchoredPosition;
        _timer.ObserverDecreaseTimeUniRx().
            Where(X=> X <= 5).
            Subscribe(X =>
            {
                m_BigTimeText.SetNumber(X);
                m_BigTimeText.gameObject.SetActive(true);
                m_BigTimeRectTransform.DOShakeAnchorPos(0.5f, 10, 50, 90);

            }).AddTo(this);

        _timer.ObserverLiftCallBack().
            Subscribe(X =>
            {
                m_BigTimeRectTransform.anchoredPosition = lTempDefpos;
                m_BigTimeText.gameObject.SetActive(false);
            }).AddTo(this);
    }

    public bool GetShow() { return m_ShowObj.activeSelf; }
    public void ShowObj(bool showObj)
    {
        if (showObj)
        {
        }
        m_ShowObj.SetActive(showObj);
        // CGameSceneWindow.SharedInstance.SetCurState(CGameSceneWindow.EState.eEndStop);
    }

    /// <summary>
    /// Start the timer
    /// </summary>
    /// <param name="onTimesUp">The callback when the times is up</param>
    public void StartTimer(Action onTimesUp) { _timer.StartTimer(onTimesUp); }

    public void AddTime(float time) { _timer.AddTime(time); }

    public void StopTimer() { _timer.StopTimer(); }
    

    public void SetCurNumberCoin(int NumberCoin) { m_Coin.SetNumber(NumberCoin); }

    public void AddCurNumberCoin(int NumberCoin) { m_Coin.AddCurNumber(NumberCoin); }

    public void ShowCoin(bool show) { m_Coin.gameObject.SetActive(show); }

    public void ShowSpeedIcon(bool show) { m_SpeedIcon.gameObject.SetActive(show); }

    public void PlayAddSpeedCountAnimation(int addcount)
    {
        m_SpeedAddText.SetText($"+{addcount}");
        m_SpeedAddText.gameObject.SetActive(true);

        CanvasGroup lTempCanvasGroup = m_SpeedAddText.GetComponent<CanvasGroup> ();
        lTempCanvasGroup.alpha = 1.0f;
        lTempCanvasGroup.DOFade(0.0f, 2.0f).
            OnComplete(()=> { m_SpeedAddText.gameObject.SetActive(false); });


        if (m_AddSpeedTextRectTransform != null)
            m_AddSpeedTextRectTransform.Kill();

        RectTransform lTempTextRectTransform = m_SpeedIcon.TextRectTransform;
        lTempTextRectTransform.localScale = Vector3.one;
        m_AddSpeedTextRectTransform = lTempTextRectTransform.DOShakeScale(1.0f);
        m_AddSpeedTextRectTransform.onKill = () => {
            lTempTextRectTransform.localScale = Vector3.one;
            m_AddSpeedTextRectTransform = null;
        };
    }

    public void ShowSpeedTutorial(bool show)
    {
        m_SpeedTutorial.SetActive(show);
        m_TutorialGroupObj.SetActive(show);

        if (show)
            m_SpeedIcon.transform.SetParent(_ShowObj.transform);
        else
            m_SpeedIcon.transform.SetParent(m_SpeedIconParent);
    }

    public void SetSpeedIconCount(int count)
    {
        m_SpeedIcon.SetNumber(count);
    }

    public void SetSpeedColor(Color pacolor)
    {
        m_SpeedIcon.SetImageColor(pacolor);
        m_SpeedIcon.SetTextColor(pacolor);
    }

    public void SetTargetTotal(int TargetTotal)
    {
        m_TotalMaxText.SetText($"/{TargetTotal}");
        m_TotalCurText.SetNumber(0);
    }

    public void SetTotal(int Number)
    {
        // m_Total.SetCurNumber(Number);
        m_TotalCurText.SetNumber(Number);
    }

    public void PlayTotalAnimaFX()
    {

        if (m_TotalCurTextAnima != null)
            m_TotalCurTextAnima.Kill();

        if (m_TotalCurTextScalAnima != null)
            m_TotalCurTextScalAnima.Kill();


        m_TotalCurText.OutlineColor(m_TCPTOutLineColor);
        m_TotalCurTextAnima = m_TotalCurTextRectTransform.DOShakeAnchorPos(0.5f, 10, 10, 180.0f, fadeOut : false);

        m_TotalCurTextRectTransform.localScale = Vector3.one * 2.0f;
        m_TotalCurTextScalAnima = m_TotalCurTextRectTransform.DOScale(1.0f, 0.4f).SetEase( Ease.Linear);

        m_TotalCurTextScalAnima.onKill = () => {
            m_TotalCurTextScalAnima = null;
            m_TotalCurTextRectTransform.localScale = Vector3.one;
        };

        m_TotalCurTextAnima.onKill = () => {
            m_TotalCurTextAnima = null;
            m_TotalCurText.OutlineColor(m_TCNTOutLineColor);
            m_TotalCurTextRectTransform.anchoredPosition = m_TotalCurRectTransformV2;
        };
    }

    public void SetCurNumberColor(string colorstr)
    {
        m_TotalCurText.SetTextColor(colorstr);
    }
    
    public void SetGameOverTotalFram()
    {
        m_TotalFramLight.gameObject.SetActive(true);
        m_TotalFramLight.color = Color.white;
        m_TotalFramLight.gameObject.GetComponent<TotalFarmAnimation>().StartAnimation();
    }

}
