using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Playables;
using MYgame.Scripts.Scenes.GameScenes.Data;
using System.Linq;

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
    [SerializeField] protected Transform m_OutFoodTransform = null;
    [Header("Game Cam")]
    [SerializeField] protected GameObject m_ViewManCam = null;
    [Header("Player")]
    [SerializeField] protected CLoveUIDegreeCompletion m_UIPlayGameLove = null;
    [Header("Man")]
    [SerializeField] protected RuntimeAnimatorController m_SetManAnimator = null;
    [SerializeField] protected Transform m_ExpressionAngry = null;
    [Header("Fork")]
    [SerializeField] protected Transform m_ForkObj = null;
    [SerializeField] protected Transform m_ForkStartTrans = null;
    [SerializeField] protected Transform m_ForkMiddlePos = null;
    [SerializeField] protected Transform m_ForkEndTrans = null;
    [Header("Food")]
    [SerializeField] protected List<CDataEliteManSmallGameFood> m_DataDesiredFoodList = null;
    [Header("UI Food")]
    [SerializeField] protected CEMSGUIDesiredFood m_MyCEMSGUIDesiredFood = null;
    [Header("Time Line")]
    [SerializeField] protected PlayableDirector m_DesiredFoodTimeLine = null;

    // ==================== SerializeField ===========================================

    protected CEMSGfood[] m_AllEMSGfood = null;
    protected CEMSGfood m_PlayGameAnimation = null;
    protected GameObject m_ManMouth = null;
    protected List<CDataEliteManSmallGameFood> m_CurDataDesiredFoodList = null;
    protected int m_CurDataDesiredFoodIndex = 0;
    protected int m_MaxScore = 100;
    protected int m_CurScore = 0;
    protected CDataEliteManSmallGameFood m_CurDataDesiredFood = null;
    protected CAllLoveCtrl m_LoveCtrl = null;
    protected Animator m_ManAnimator = null;
    protected ResultUI m_ResultUI = null;

    protected override void Awake()
    {
        base.Awake();

        m_ResultUI = this.GetComponentInChildren<ResultUI>();
        m_ManAnimator = m_ManObj.GetComponent<Animator>();

        m_ManAnimator.runtimeAnimatorController = m_SetManAnimator;

        m_AllEMSGfood = this.GetComponentsInChildren<CEMSGfood>();

        foreach (var item in m_AllEMSGfood)
        {
            item.OBClickReturnVal()
                .Where(Val => OBStateVal().Value == EState.ePlayGame)
                .Subscribe(val => {
                    m_PlayGameAnimation = val;
                    OBStateVal().Value = EState.ePlayGameAnimation;
                }).AddTo(this); ;
        }

        m_ManMouth = GameObject.FindWithTag(StaticGlobalDel.TagManMouth);
        m_LoveCtrl = this.GetComponentInChildren<CAllLoveCtrl>();



        var rnd = new System.Random();
        m_CurDataDesiredFoodList = m_DataDesiredFoodList.OrderBy(item => rnd.Next()).ToList();
        Debug.Log($"m_CurDataDesiredFoodList.Count  = {m_CurDataDesiredFoodList.Count}");
    }

    private void Start()
    {


        DOTween.Sequence()
        .AppendInterval(0.5f)
        .AppendCallback(() => {
            m_ViewManCam.SetActive(true);
        })
        .AppendInterval(1.0f)
        .AppendCallback(() =>{
            OBStateVal().Value = EState.ePlayGame;
         });



        // ================= ePlayGame =======================
        OBStateVal().Where(val => val == EState.ePlayGame)
        .Subscribe(X => {

            Vector3 lTemppos = m_ManMouth.transform.position;
            m_ExpressionAngry.position = lTemppos + (m_ExpressionAngry.forward * -0.1f) + (m_ExpressionAngry.right * -0.04f) + (m_ExpressionAngry.up * 0.2f);

            m_PlayGameAnimation = null;
            m_DesiredFoodTimeLine.time = 0.0f;
            m_DesiredFoodTimeLine.Play();
            m_CurDataDesiredFood = m_CurDataDesiredFoodList[m_CurDataDesiredFoodIndex];
            m_MyCEMSGUIDesiredFood.SetData(m_CurDataDesiredFood);
           // m_CurDataDesiredFood.Remove(m_CurDataDesiredFood[0]);
        }).AddTo(this);

        // ================= ePlayGameAnimation =======================
        OBStateVal().Where(val => val == EState.ePlayGameAnimation)
       .Subscribe(X => {

           const float lTempMoveForkStartTime = 0.5f;
           const float lTempMoveForkEndTime = 1.0f;
           Sequence lTempSequence = DOTween.Sequence();

           lTempSequence.AppendCallback(() =>{
               m_ForkObj.DOMove(m_ForkMiddlePos.position, lTempMoveForkStartTime).SetEase(Ease.Linear);
               m_ForkObj.DORotateQuaternion(m_ForkMiddlePos.rotation, lTempMoveForkStartTime).SetEase(Ease.Linear);
           });
           lTempSequence.AppendInterval(lTempMoveForkStartTime);
           lTempSequence.Append(m_ForkObj.DOMove(m_PlayGameAnimation.gameObject.transform.position, lTempMoveForkStartTime));

           lTempSequence.AppendCallback(() =>{
               m_PlayGameAnimation.gameObject.transform.SetParent(m_ForkObj.transform);
               m_ForkEndTrans.transform.position = m_ManMouth.transform.position;
               m_ForkObj.DOMove(m_ForkEndTrans.position - (m_ForkEndTrans.forward * 0.05f), lTempMoveForkEndTime).SetEase(Ease.Linear);
               m_ForkObj.DORotateQuaternion(m_ForkEndTrans.rotation, lTempMoveForkEndTime).SetEase(Ease.Linear);
           });

           if (m_PlayGameAnimation.FoodData == m_CurDataDesiredFood)
           {
               lTempSequence.AppendInterval(lTempMoveForkEndTime * 0.8f);
               lTempSequence.Append(m_PlayGameAnimation.gameObject.transform.DOScale(Vector3.zero, lTempMoveForkEndTime * 0.2f)
               .OnComplete(() => {

                   m_PlayGameAnimation.gameObject.SetActive(false);
                   Transform lTempFx = StaticGlobalDel.NewFxAddParentShow(m_ManMouth.transform, CGGameSceneData.EAllFXType.eEmojiNoLoop);
                   lTempFx.localScale = Vector3.one * 0.3f;
                   AddScore(m_CurDataDesiredFood.Score);
               }));
           }
           else
           {
               lTempSequence.AppendInterval(lTempMoveForkEndTime * 0.9f);
               lTempSequence.AppendCallback(() => {
                   m_PlayGameAnimation.transform.SetParent(m_OutFoodTransform);
                   m_PlayGameAnimation.OpewRigidbody(true);
                   m_ExpressionAngry.gameObject.SetActive(true);
                   m_ExpressionAngry.DOScale(0.5f, 0.2f).SetLoops(4, LoopType.Yoyo)
                   .OnComplete(()=> { m_ExpressionAngry.gameObject.SetActive(false); });
               });
               lTempSequence.AppendInterval(lTempMoveForkEndTime * 0.1f);
           }

           lTempSequence.AppendCallback(() => {
               m_ForkObj.DOMove(m_ForkStartTrans.position, lTempMoveForkEndTime).SetEase(Ease.Linear);
               m_ForkObj.DORotateQuaternion(m_ForkStartTrans.rotation, lTempMoveForkEndTime).SetEase(Ease.Linear)
               .OnComplete(()=> {
                   int lTempFoodindex = m_CurDataDesiredFoodIndex + 1;
                  // Debug.Log($"lTempFoodindex = {lTempFoodindex}");
                   if (lTempFoodindex < m_CurDataDesiredFoodList.Count)
                   {
                       m_CurDataDesiredFoodIndex = lTempFoodindex;
                       OBStateVal().Value = EState.ePlayGame;
                   }
                   else
                   {
                       m_CurDataDesiredFoodIndex = lTempFoodindex;

                       m_ResultUI.OverButton.onClick.AddListener(() =>
                       {
                           StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
                       });

                       m_ResultUI.NextButton.onClick.AddListener(() =>
                       {
                           StaticGlobalDel.g_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
                       });

                       if (m_MaxScore <= m_CurScore)
                           m_ResultUI.ShowSuccessUI(0.5f);
                       else
                           m_ResultUI.ShowFailedUI(0.5f);
                   }
               });
           });
       }).AddTo(this);
    }

    public void AddScore(int add)
    {
        int lTempScore = m_CurScore + add;
        m_CurScore = lTempScore > m_MaxScore ? m_MaxScore : lTempScore;

        float lTempSetval = m_CurScore >= m_MaxScore ? 1.0f : (float)m_CurScore / (float)m_MaxScore;
        m_LoveCtrl.SetTargetLoveVal(lTempSetval);
    }

    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<EState> m_OBState = new ReactiveProperty<EState>(EState.eStart);

    public UniRx.ReactiveProperty<EState> OBStateVal()
    {
        return m_OBState ?? (m_OBState = new ReactiveProperty<EState>(EState.eStart));
    }

    // ===================== UniRx ======================
}
