using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;


public class CEliteManSmallGame : CScenesChangChar
{
    public enum EState
    {
        eStart              = 0,
        ePlayGame           = 1,
        ePlayGameAnimation  = 2,
        ePlayGameEnd        = 3,
        eKissGameStart      = 4,
        eKissGame           = 5,
        eKissGameWin        = 6,
        eMax
    }

    // ==================== SerializeField ===========================================
    [Header("Game Cam")]
    [SerializeField] protected GameObject m_ViewManCam = null;
    [Header("Play Game")]
    [SerializeField] protected CLoveUIDegreeCompletion m_UIPlayGameLove = null;

    // ==================== SerializeField ===========================================

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        DOTween.Sequence()
        .AppendInterval(0.5f)
        .AppendCallback(() => {
            m_ViewManCam.SetActive(true);
        })
        .AppendInterval(1.0f);


        OBStateVal().Value = EState.ePlayGame;
    }


    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EState> m_OBState = new ReactiveProperty<EState>(EState.eStart);

    public UniRx.ReactiveProperty<EState> OBStateVal()
    {
        return m_OBState ?? (m_OBState = new ReactiveProperty<EState>(EState.eStart));
    }

    // ===================== UniRx ======================
}
