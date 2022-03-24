using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using UniRx.Triggers;
using System;

public class CUIMoveTarget : MonoBehaviour
{
    public enum EMoveTotalImageState
    {
        eNull = 0,
        eOpenStart = 1,
        eMoveUpdate = 2,
        eMax
    }


    [SerializeField] protected Vector2 m_MoveTotalImageStartPivot   = new Vector3(0.5f, 0.0f);
    [SerializeField] protected Vector3 m_MoveTotalImageStartPos     = new Vector3(75.0f, 805.0f, 0.0f);

    [SerializeField] protected Vector2 m_MoveTotalImageMovePivot    = new Vector3(0.5f, 0.5f);
    [SerializeField] protected Vector3 m_MoveTotalImageEndPos       = new Vector3(-330.0f, 789.0f, 0.0f);

    [SerializeField] protected float m_MoveTotalImageZoomOutDis     = 200.0f;
    [SerializeField] protected float m_MoveTotalImageSpeed          = 1000.0f;

    protected Vector3 m_CurDir = Vector3.one;
    protected Vector3 m_MoveTotalImageScaleSize = Vector3.one;
    protected Quaternion m_DefMoveTotalQuat = Quaternion.identity;
    protected float m_OldDisRatio = 1.0f;

    protected RectTransform m_ThisRectTransform = null;

    protected int m_ReturnInt = 0;
    public int ReturnInt
    {
        set => m_ReturnInt = value;
        get => m_ReturnInt;
    }

    private void Awake()
    {
        m_ThisRectTransform = this.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;

        m_DefMoveTotalQuat = m_ThisRectTransform.rotation;

        var lTempUpdateAs = this.UpdateAsObservable();

        lTempUpdateAs.Where(X => m_OBCurMoveTotalImageState.Value == EMoveTotalImageState.eMoveUpdate)
            .Subscribe(framcount =>
            {
                RectTransform lTempRectTransform = m_ThisRectTransform;
                Vector3 lTempDir = m_MoveTotalImageEndPos - lTempRectTransform.anchoredPosition3D;
                lTempDir.z = 0.0f;
                float TempDis = lTempDir.magnitude;

                if (TempDis < 15.0f)
                {
                    lTempRectTransform.localScale = Vector3.zero;
                    m_OBCurMoveTotalImageState.Value = EMoveTotalImageState.eNull;
                    Destroy(this.gameObject);
                }
                else
                {

                    float lTempDisRatio = Mathf.Min(TempDis / m_MoveTotalImageZoomOutDis, 1.0f);
                    float lTempCountdownDisRatio = (1 - lTempDisRatio) * 0.5f + 0.5f;
                    lTempDir.Normalize();
                    m_CurDir = Vector3.RotateTowards(m_CurDir, lTempDir, Time.deltaTime * 4.0f * lTempCountdownDisRatio, 2.0f);
                    m_CurDir.Normalize();

                    if (lTempDisRatio < 0.6f && m_OldDisRatio >= 0.6f)
                        lTempAudioManager.PlaySE(CSEPlayObj.ESE.eIngameMoveToTotleIn);

                    lTempRectTransform.anchoredPosition3D = lTempRectTransform.anchoredPosition3D + (m_CurDir * m_MoveTotalImageSpeed * Time.deltaTime * lTempDisRatio);

                    m_ThisRectTransform.localScale = m_MoveTotalImageScaleSize * lTempDisRatio;
                    m_ThisRectTransform.rotation *= Quaternion.Euler(0.0f, 0.0f, 360.0f * Time.deltaTime * (1.0f - lTempDisRatio));
                    m_OldDisRatio = lTempDisRatio;
                }
            }).AddTo(this);


        ObserverMoveTotalImageStateVal().Where(_ => _ == EMoveTotalImageState.eOpenStart)
            .Subscribe(X => {
                m_CurDir.x =1.0f;
                m_CurDir.z = 0.0f;
                m_CurDir.y = -1.0f;
                gameObject.SetActive(true);

                lTempAudioManager.PlaySE(CSEPlayObj.ESE.eIngameScale);

                m_ThisRectTransform.DOPivot(m_MoveTotalImageMovePivot, 0.5f);
                Tween lTempTween = m_ThisRectTransform.DOScale(UnityEngine.Random.Range(1.3f, 1.6f), 0.5f).SetEase( Ease.Linear);
                lTempTween.onComplete = () => {
                    
                    m_MoveTotalImageScaleSize = m_ThisRectTransform.localScale;
                };
                m_OBCurMoveTotalImageState.Value = EMoveTotalImageState.eMoveUpdate;
            }).AddTo(this);

        //ObserverMoveTotalImageStateVal().Where(_ => _ == EMoveTotalImageState.eMoveUpdate)
        //    .Subscribe(X => {
        //        m_MoveTotalImageScaleSize = m_ThisRectTransform.localScale;
        //        Tween lTempTween = m_ThisRectTransform.DOPivot(m_MoveTotalImageMovePivot, 0.5f);
        //    }).AddTo(this);

        m_OBCurMoveTotalImageState.Value = EMoveTotalImageState.eOpenStart;
    }

    private void OnDestroy()
    {
        m_DestroyUniRx.OnNext(Unit.Default);
    }

    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EMoveTotalImageState> m_OBCurMoveTotalImageState = new ReactiveProperty<EMoveTotalImageState>(EMoveTotalImageState.eNull);
    public UniRx.Subject<Unit> m_DestroyUniRx = new Subject<Unit>();

    public UniRx.ReactiveProperty<EMoveTotalImageState> ObserverMoveTotalImageStateVal()
    {
        return m_OBCurMoveTotalImageState ?? (m_OBCurMoveTotalImageState = new ReactiveProperty<EMoveTotalImageState>(EMoveTotalImageState.eNull));
    }

    public UniRx.Subject<Unit> ObserverDestroyUniRx()
    {
        return m_DestroyUniRx ?? (m_DestroyUniRx = new UniRx.Subject<Unit>());
    }
    // ===================== UniRx ======================
}
