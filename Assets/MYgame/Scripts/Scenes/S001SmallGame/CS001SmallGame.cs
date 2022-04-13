using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CS001SmallGame : CScenesChangChar
{
    public int ReadBlendHash = 0;

    public enum EState
    {
        eStart          = 0,
        eManAct         = 1,
        ePlayGame       = 2,
        ePlayGameEnd    = 3,
        eAllEnd         = 4,
        eMax
    }

    // ==================== SerializeField ===========================================

    [SerializeField] protected RuntimeAnimatorController m_SetManAnimator = null;
    [SerializeField] protected List<CAll3DLineCtrl> m_All3DLineCtrl = new List<CAll3DLineCtrl>();

    // ==================== SerializeField ===========================================

    protected Animator m_ManAnimator = null;
    protected Animator m_GirlAnimator = null;
    protected int m_QuestionsIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        m_ManAnimator = m_ManObj.GetComponent<Animator>();
        m_GirlAnimator = m_PlayerSkin.GetComponent<Animator>();

        m_ManAnimator.runtimeAnimatorController = m_SetManAnimator;
        ReadBlendHash = Animator.StringToHash("Blend");

        foreach (var item in m_All3DLineCtrl)
            item.MyCtrlAnimator = m_GirlAnimator;
    }

    private void Start()
    {
        DOTween.Sequence()
        .AppendInterval(1.0f)
        .AppendCallback(() => {
            m_OBState.Value = EState.eManAct;
        });

       

        OBStateVal().Where(_ => _ == EState.eManAct)
        .Subscribe(X => {

        float lTempBlendVal = 0.0f;
            DOTween.To(
            () => lTempBlendVal, x => lTempBlendVal = x,
                1.0f, 1.0f)
            .SetEase(Ease.Linear)
            .OnUpdate(()=> { StaticGlobalDel.SetAnimatorFloat(m_ManAnimator, ReadBlendHash, lTempBlendVal); })
            .OnComplete(() => {
                m_OBState.Value = EState.ePlayGame;
                m_All3DLineCtrl[m_QuestionsIndex].gameObject.SetActive(true);
            });

        }).AddTo(this);
    }


    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EState> m_OBState = new ReactiveProperty<EState>(EState.eStart);

    public UniRx.ReactiveProperty<EState> OBStateVal()
    {
        return m_OBState ?? (m_OBState = new ReactiveProperty<EState>(EState.eStart));
    }

    // ===================== UniRx ======================
}
