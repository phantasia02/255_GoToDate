using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CS001SmallGame : CScenesChangChar
{
    //public int ReadBlendHash = 0;

    public enum EState
    {
        eStart          = 0,
        eManAct         = 1,
        ePlayGame       = 2,
        ePlayGameEnd    = 3,
        eKissGameStart  = 4,
        eKissGame       = 5,
        eKissGameWin    = 6,
        eMax
    }

    [System.Serializable]
    public class DataPlayPose
    {
        public CAll3DLineCtrl m_PlayLineCtrl = null;
        public GameObject m_CamObj = null;
        [HideInInspector] public int m_PoseHashID = 0;
        public Transform m_ManRefTransform = null;
        public Transform m_PlayerRefTransform = null;
    }


    // ==================== SerializeField ===========================================

    [SerializeField] protected RuntimeAnimatorController    m_SetManAnimator = null;
    [SerializeField] protected List<DataPlayPose>           m_All3DLineCtrl = new List<DataPlayPose>();
    [SerializeField] protected Transform                    m_ManNormalTransform = null;
    [SerializeField] protected Transform                    m_PlayerNormalTransform = null;
    [SerializeField] protected CLoveUIDegreeCompletion      m_UIPlayGameLove = null;
    [SerializeField] protected Transform                    m_ManHeadTransform = null;
    [SerializeField] protected GameObject                   m_LoveFxLoopObj = null;


    [Header("Love Game Ctrl Obj")]
    [SerializeField] protected GameObject                   m_KissPos   = null;
    [SerializeField] protected CLoveUIDegreeCompletion      m_UIKGLove  = null;
    [SerializeField] protected CanvasGroup                  m_UIKGAll   = null;
    [SerializeField] protected CUIButton                    m_UIKGLovePressButton = null;
    [SerializeField] protected CUIImage                     m_UIKGShowImage = null;


    // ==================== SerializeField ===========================================

    protected ResultUI m_ResultUI = null;
    protected Animator m_ManAnimator = null;
    protected Animator m_GirlAnimator = null;
    protected int m_QuestionsIndex = 0;
    protected int m_BuffAccumulationLoveVal = 0;
    protected int m_CurLoveVal = 0;
    protected int m_MaxLoveVal = 300;

    readonly public float m_UIKGLoveMaxTime = 3.0f;
    protected float m_UIKGLoveCurTime = 0.0f;
    protected bool m_KGPress= false;
   

    protected override void Awake()
    {
        base.Awake();

        m_ResultUI = this.GetComponentInChildren<ResultUI>();
        m_ManAnimator = m_ManObj.GetComponent<Animator>();
        m_GirlAnimator = m_PlayerSkin.GetComponent<Animator>();

        m_ManAnimator.runtimeAnimatorController = m_SetManAnimator;

        
        foreach (var item in m_All3DLineCtrl)
            item.m_PlayLineCtrl.MyCtrlAnimator = m_GirlAnimator;

        m_All3DLineCtrl[0].m_PoseHashID = Animator.StringToHash("Pose1");
        m_All3DLineCtrl[1].m_PoseHashID = Animator.StringToHash("Pose2");
        m_All3DLineCtrl[2].m_PoseHashID = Animator.StringToHash("Pose3");

        SetTasRefPos(m_TargetManObj, m_ManNormalTransform, true);
        SetTasRefPos(m_PlayerObj, m_PlayerNormalTransform, true);
        //m_TargetManObj.position = m_ManNormalTransform.transform.position;
        //m_TargetManObj.rotation = m_ManNormalTransform.transform.rotation;
        //m_PlayerObj = null;


    }

    private void Start()
    {

        GameObject lTempGameObject = GameObject.FindWithTag(StaticGlobalDel.TagManSpine2);
        m_ManHeadTransform = lTempGameObject.transform;

        if (m_ResultUI != null)
        {
            m_ResultUI.OverButton.onClick.AddListener(() =>
            {
                StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });

            m_ResultUI.NextButton.onClick.AddListener(() =>
            {
                StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });
        }
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
        DOTween.Sequence()
        .AppendInterval(1.0f)
        .AppendCallback(() => {
            m_OBState.Value = EState.eManAct;
        });

        OBStateVal().Where(_ => _ == EState.eManAct)
        .Subscribe(X => {

            SetTasRefPos(m_TargetManObj, m_All3DLineCtrl[m_QuestionsIndex].m_ManRefTransform);
            SetTasRefPos(m_PlayerObj, m_All3DLineCtrl[m_QuestionsIndex].m_PlayerRefTransform);

            float lTempBlendVal = 0.0f;
            DOTween.To(() => lTempBlendVal, x => lTempBlendVal = x, 1.0f, 1.0f)
            .SetEase(Ease.Linear)
            .OnUpdate(()=> { StaticGlobalDel.SetAnimatorFloat(m_ManAnimator, CGGameSceneData.g_AnimatorHashReadBlend, lTempBlendVal); })
            .OnComplete(() => {
                m_OBState.Value = EState.ePlayGame;
                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.gameObject.SetActive(true);

                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.OBE3DLineCtrlStateVal().Where(Val => Val == CAll3DLineCtrl.E3DLineCtrlState.eEnd)
                .Subscribe(Val => {
                    m_OBState.Value = EState.ePlayGameEnd;
                });

                float lTenoMaxVal = m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.MaxLoveVal;
                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.OBCurLoveVal()
                .Subscribe(Val => 
                {
                    m_BuffAccumulationLoveVal = Val;
                    if (m_BuffAccumulationLoveVal >= (int)lTenoMaxVal)
                    {
                        CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;
                        StaticGlobalDel.NewFxAddParentShow(m_ManHeadTransform, CGGameSceneData.EAllFXType.eEmojiNoLoop);
                    }

                    int lTempval = m_BuffAccumulationLoveVal + m_CurLoveVal;
                    m_UIPlayGameLove.SetLoveProgressionVal((float)lTempval / (float)m_MaxLoveVal);
                });
            });

        }).AddTo(this);

        OBStateVal().Where(_ => _ == EState.ePlayGameEnd)
            .Subscribe(X =>{

                float lTempBlendVal = 1.0f;
                m_CurLoveVal += m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.OBCurLoveVal().Value;
                m_All3DLineCtrl[m_QuestionsIndex].m_PlayLineCtrl.gameObject.SetActive(false);
                int lTempQuestionsIndex = m_QuestionsIndex + 1;
                if (lTempQuestionsIndex == m_All3DLineCtrl.Count)
                {
                    

                    //m_All3DLineCtrl[m_QuestionsIndex].

                    m_QuestionsIndex = lTempQuestionsIndex;
                    m_OBState.Value = EState.eKissGameStart;
                    return;
                }

                float lTempBlendVal2 = m_GirlAnimator.GetFloat(CGGameSceneData.g_AnimatorHashReadBlend);
                lTempBlendVal = 1.0f;

                Tween lTempActorTween = DOTween.To(() => lTempBlendVal, x => lTempBlendVal = x, 0.0f, 1.0f)
                .SetEase(Ease.Linear)
                .OnUpdate(() => {
                    StaticGlobalDel.SetAnimatorFloat(m_ManAnimator, CGGameSceneData.g_AnimatorHashReadBlend, lTempBlendVal);
                    StaticGlobalDel.SetAnimatorFloat(m_GirlAnimator, CGGameSceneData.g_AnimatorHashReadBlend, lTempBlendVal * lTempBlendVal2);
                });
                lTempActorTween.Pause();


                Sequence lTempSequence = DOTween.Sequence()
                 .AppendInterval(0.5f)
                 .AppendCallback(() => {
                     SetTasRefPos(m_TargetManObj, m_ManNormalTransform);
                     SetTasRefPos(m_PlayerObj, m_PlayerNormalTransform);
                     
                 })
                 .Append(lTempActorTween)
                 .AppendInterval(0.5f)
                 .AppendCallback(() =>
                 {
                     m_QuestionsIndex = lTempQuestionsIndex;
                     m_All3DLineCtrl[m_QuestionsIndex].m_CamObj.SetActive(true);

                     m_ManAnimator.SetTrigger(m_All3DLineCtrl[m_QuestionsIndex].m_PoseHashID);
                     m_GirlAnimator.SetTrigger(m_All3DLineCtrl[m_QuestionsIndex].m_PoseHashID);
                     
                     m_OBState.Value = EState.eManAct;
                 });
                 
            }).AddTo(this);


        OBStateVal().Where(_ => _ == EState.eKissGameStart)
        .Subscribe(X => {
            // On Nyo!!!!show
            if (m_CurLoveVal < m_MaxLoveVal)
            {
                m_ResultUI.ShowFailedUI(0.5f);
                return;
            }
            else
            {
                float lTempBlendVal = m_GirlAnimator.GetFloat(CGGameSceneData.g_AnimatorHashReadBlend);
                if (lTempBlendVal < 1.0f)
                {
                    DOTween.To(() => lTempBlendVal, x => lTempBlendVal = x, 1.0f, 0.5f)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() => { StaticGlobalDel.SetAnimatorFloat(m_GirlAnimator, CGGameSceneData.g_AnimatorHashReadBlend, lTempBlendVal); });
                }


                m_UIPlayGameLove.gameObject.SetActive(false);
                Sequence lTempSequence = DOTween.Sequence()
                     .AppendCallback(() =>{
                         m_UIKGAll.DOFade(1.0f, 0.4f);
                     })
                     .AppendInterval(0.7f)
                     .AppendCallback(() =>
                     {
                         Vector3 lTempUIKissPos = Camera.main.WorldToScreenPoint(m_KissPos.transform.position);
                         m_UIKGLovePressButton.MyRectTransform.position = lTempUIKissPos;
                         m_UIKGShowImage.MyRectTransform.position = lTempUIKissPos;
                         m_LoveFxLoopObj.transform.position = m_KissPos.transform.position;
                         m_OBState.Value = EState.eKissGame;
                     });
            }

        }).AddTo(this);

        OBStateVal().Where(_ => _ == EState.eKissGameWin)
       .Subscribe(X => {

           m_UIKGAll.gameObject.SetActive(false);
           m_LoveFxLoopObj.SetActive(true);
           m_UIKGLovePressButton.gameObject.SetActive(false);
           m_UIKGShowImage.gameObject.SetActive(false);
           m_ResultUI.ShowSuccessUI(0.5f);
       }).AddTo(this);

    }
    

    public void SetTasRefPos(Transform SetTas, Transform RefTas, bool update = false, float duration = 0.45f)
    {
        if (update)
        {
            SetTas.position = RefTas.position;
            SetTas.rotation = RefTas.rotation;
        }
        else
        {
            SetTas.DOMove(RefTas.position, duration);
            SetTas.DORotateQuaternion(RefTas.rotation, duration);
        }
    }


    public void KissPress(bool press)
    {
        m_KGPress = press;
        Debug.Log($"m_KGPress = {m_KGPress}");
    }


    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EState> m_OBState = new ReactiveProperty<EState>(EState.eStart);

    public UniRx.ReactiveProperty<EState> OBStateVal()
    {
        return m_OBState ?? (m_OBState = new ReactiveProperty<EState>(EState.eStart));
    }

    // ===================== UniRx ======================
}
