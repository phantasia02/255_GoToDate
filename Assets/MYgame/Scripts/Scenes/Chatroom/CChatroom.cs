using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;
using DG.Tweening;

public class CChatroom : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected CChatroomCentrMessage m_MyChatroomCentrMessage = null;
    [SerializeField] protected CUIButton m_Yes  = null;
    [SerializeField] protected CUIButton m_No   = null;
    [SerializeField] protected GameObject m_BottomObj = null;
    [SerializeField] protected Image m_ObjectStickers = null;
    [SerializeField] protected GameObject m_StartUI = null;
    // ==================== SerializeField ===========================================

    protected CDataObjChar          m_TargetObj         = null;
    protected CDataRole             m_MyRoleData        = null;
    protected CMessageList          m_CurMessageList    = null;
    protected CChatroomLoveGroup    m_LoveGroup         = null;
    protected ResultUI              m_ResultUI          = null;

    CChangeScenes m_ChangeScenes = new CChangeScenes();

    protected override void Awake()
    {
        base.Awake();

        m_MyChatroomCentrMessage = this.GetComponentInChildren<CChatroomCentrMessage>();
        m_LoveGroup = this.GetComponentInChildren<CChatroomLoveGroup>();
        m_ResultUI = this.GetComponentInChildren<ResultUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MyRoleData = StaticGlobalDel.BuffMyRoleData;
        m_TargetObj = StaticGlobalDel.TargetDataObj;

        StageData lTempStageData = StaticGlobalDel.StageData;

        m_Yes.AddListener(onClickYes);
        m_No.AddListener(onClickNo);

        m_ObjectStickers.sprite = m_TargetObj.ChatMugShot;
        // m_MyChatroomCentrMessage.AddMessage( CChatroomCentrMessage.EMessageType.eOtherMessage,  );
        m_LoveGroup.AddLove(3);

        DOTween.Sequence()
        .AppendInterval(1.0f)
        .AppendCallback(() => {
            m_StartUI.SetActive(false);
            StartCoroutine(SetMessageList(lTempStageData.StartMessageList));
        });
    }

    public IEnumerator SetMessageList(CMessageList parMessageList)
    {
        m_CurMessageList = parMessageList;
        COneMessage lTempCOneMessage = null;

        for (int i = 0; i < parMessageList.ListMessage.Count; i++)
        {
            lTempCOneMessage = parMessageList.ListMessage[i];



            if (lTempCOneMessage.m_Type == EMessageType.eMyMessage)
            {
                if (i > 0)
                    yield return new WaitForSeconds(1.0f);

                m_MyChatroomCentrMessage.AddMessage(lTempCOneMessage.m_Type, m_MyRoleData.ChatMugShot, lTempCOneMessage.m_Messagestr);
            }
            else if (lTempCOneMessage.m_Type == EMessageType.eOtherMessage)
            {
                if (m_MyChatroomCentrMessage.AllShowMessage.Count != 0)
                    yield return new WaitForSeconds(1.0f);

                m_MyChatroomCentrMessage.AddMessage(lTempCOneMessage.m_Type, m_TargetObj.ChatMugShot, lTempCOneMessage.m_Messagestr);
            }
            //yield return new WaitForSeconds(1.0f);
        }

        if (parMessageList.LoveAdd != 0)
            m_LoveGroup.AddLove(parMessageList.LoveAdd);

        if (parMessageList.Breakpoint == EDialogueBreakpoint.eNextMessageList)
            StartCoroutine(SetMessageList(m_CurMessageList.NextQuestion));
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eQuestion)
        {
            yield return new WaitForSeconds(0.5f);
            ShowSeletUI();
        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eLoveJudgment)
        {
            if (m_LoveGroup.LoveCount > 3)
                StartCoroutine(SetMessageList(m_CurMessageList.SelectMessageList[(int)ESelectType.eYes]));
            else
                StartCoroutine(SetMessageList(m_CurMessageList.SelectMessageList[(int)ESelectType.eNo]));
        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eOver)
        {
            m_ResultUI.OverButton.onClick.AddListener(() =>
            {
                m_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectObject);
            });

            m_ResultUI.ShowFailedUI();
        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eWin)
        {
            m_ResultUI.NextButton.onClick.AddListener(() =>
            {
                m_ChangeScenes.ChangeScenes(StaticGlobalDel.g_ScenesNameSelectRole);
            });

            m_ResultUI.ShowSuccessUI();
        }
    }

    public void ShowSeletUI()
    {
        m_BottomObj.SetActive(true);
        m_Yes.SetText(m_CurMessageList.SelectMessageList[(int)ESelectType.eYes].ListMessage[0].m_Messagestr);
        m_No.SetText(m_CurMessageList.SelectMessageList[(int)ESelectType.eNo].ListMessage[0].m_Messagestr);
    }

    public void onClickYes()
    {
        m_BottomObj.SetActive(false);
        StartCoroutine(SetMessageList(m_CurMessageList.SelectMessageList[(int)ESelectType.eYes]));
    }

    public void onClickNo()
    {
        m_BottomObj.SetActive(false);
        StartCoroutine(SetMessageList(m_CurMessageList.SelectMessageList[(int)ESelectType.eNo]));
    }
}
