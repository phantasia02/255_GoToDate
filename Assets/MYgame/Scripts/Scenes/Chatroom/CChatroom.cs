using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CChatroom : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected CChatroomCentrMessage m_MyChatroomCentrMessage = null;

    // ==================== SerializeField ===========================================

    protected CDataObjChar  m_TargetObj         = null;
    protected CDataRole     m_MyRoleData        = null;
    protected CMessageList  m_CurMessageList    = null;

    private void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);

        m_MyChatroomCentrMessage = this.GetComponentInChildren<CChatroomCentrMessage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MyRoleData = StaticGlobalDel.BuffMyRoleData;
        m_TargetObj = StaticGlobalDel.TargetDataObj;

        StageData lTempStageData = StaticGlobalDel.StageData;
        SetMessageList(lTempStageData.StartMessageList);

       // m_MyChatroomCentrMessage.AddMessage( CChatroomCentrMessage.EMessageType.eOtherMessage,  );
    }

    public void SetMessageList(CMessageList parMessageList)
    {
        m_CurMessageList = parMessageList;
        COneMessage lTempCOneMessage = null;

        for (int i = 0; i < parMessageList.ListMessage.Count; i++)
        {
            lTempCOneMessage = parMessageList.ListMessage[i];
            if (lTempCOneMessage.m_Type == EMessageType.eMyMessage)
                m_MyChatroomCentrMessage.AddMessage(lTempCOneMessage.m_Type, m_MyRoleData.MugShot, lTempCOneMessage.m_Messagestr);
            else if (lTempCOneMessage.m_Type == EMessageType.eOtherMessage)
                m_MyChatroomCentrMessage.AddMessage(lTempCOneMessage.m_Type, m_TargetObj.MugShot, lTempCOneMessage.m_Messagestr);
        }

        if (parMessageList.Breakpoint == EDialogueBreakpoint.eNextMessageList)
        {
            SetMessageList(m_CurMessageList.NextQuestion);
        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eQuestion)
        {

        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eLoveJudgment)
        {

        }
        else if (parMessageList.Breakpoint == EDialogueBreakpoint.eEnd)
        {

        }
    }


}
