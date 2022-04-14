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

    [System.Serializable]
    public class DataPlayPose
    {
        public CAll3DLineCtrl m_PlayLineCtrl = null;
        public GameObject m_CamObj = null;
        public int m_PoseHashID = 0;
    }


    // ==================== SerializeField ===========================================

    [SerializeField] protected RuntimeAnimatorController m_SetManAnimator = null;
    [SerializeField] protected List<DataPlayPose> m_All3DLineCtrl = new List<DataPlayPose>();

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
            item.m_PlayLineCtrl.MyCtrlAnimator = m_GirlAnimator;

        m_All3DLineCtrl[0].m_PoseHashID = Animator.StringToHash("Pose1");
        m_All3DLineCtrl[1].m_PoseHashID = Animator.StringToHash("Pose2");
        m_All3DLineCtrl[2].m_PoseHashID = Animator.StringToHash("Pose3");
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
            DOTween.To(() => lTempBlendVal, x => lTempBlendVal = x, 1.0f, 1.0f)
            .SetEase(Ease.Linear)
            .OnUpdate(()=> { StaticGlobalDel.SetAnimatorFloat(m_ManAnimator, ReadBlendHash, lTempBlendVal); })
            .OnComplete(() => {
                m_OBState.Value = EState.ePlayGame;
                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.gameObject.SetActive(true);

                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.OBE3DLineCtrlStateVal().Where(Val => Val == CAll3DLineCtrl.E3DLineCtrlState.eEnd)
                .Subscribe(Val => {m_OBState.Value = EState.ePlayGameEnd;});
            });

        }).AddTo(this);

        OBStateVal().Where(_ => _ == EState.ePlayGameEnd)
            .Subscribe(X =>{

                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.gameObject.SetActive(false);

                float lTempBlendVal2 = m_GirlAnimator.GetFloat(ReadBlendHash);
                float lTempBlendVal = 1.0f;

                Tween lTempActorTween = DOTween.To(() => lTempBlendVal, x => lTempBlendVal = x, 0.0f, 1.0f)
                .SetEase(Ease.Linear)
                .OnUpdate(() => {
                    StaticGlobalDel.SetAnimatorFloat(m_ManAnimator, ReadBlendHash, lTempBlendVal);
                    StaticGlobalDel.SetAnimatorFloat(m_GirlAnimator, ReadBlendHash, lTempBlendVal * lTempBlendVal2);
                });
                lTempActorTween.Pause();


                Sequence lTempSequence = DOTween.Sequence()
                 .AppendInterval(0.5f)
                 .Append(lTempActorTween)
                 .AppendInterval(0.5f)
                 .AppendCallback(() =>
                 {
                     int lTempQuestionsIndex = m_QuestionsIndex + 1;
                     if (lTempQuestionsIndex == m_All3DLineCtrl.Count)
                     {
                         m_QuestionsIndex = lTempQuestionsIndex;
                         m_OBState.Value = EState.ePlayGameEnd;
                         return;
                     }
                     m_QuestionsIndex = lTempQuestionsIndex;
                     m_All3DLineCtrl[m_QuestionsIndex].m_CamObj.SetActive(true);

                     m_ManAnimator.SetTrigger(m_All3DLineCtrl[m_QuestionsIndex].m_PoseHashID);
                     m_GirlAnimator.SetTrigger(m_All3DLineCtrl[m_QuestionsIndex].m_PoseHashID);
                     m_OBState.Value = EState.eManAct;
                 });
                 //.Append(lTempActorTween2)
                 //.AppendCallback(() => {

                 //    //int lTempQuestionsIndex = m_QuestionsIndex + 1;
                 //    //if (lTempQuestionsIndex == m_All3DLineCtrl.Count)
                 //    //    m_OBState.Value = EState.ePlayGameEnd;
                 //    //else
                 //    //   m_OBState.Value = EState.eManAct;
                 //})
                 
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
