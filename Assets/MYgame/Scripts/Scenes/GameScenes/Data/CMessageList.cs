using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    public enum EMessageType
    {
        eMyMessage = 0,
        eOtherMessage = 1,
        eMax
    }

    public enum ESelectType
    {
        eYes    = 0,
        eNo     = 1,
        eMax
    }

    public enum EDialogueBreakpoint
    {
        eNextMessageList    = 0,
        eQuestion           = 1,
        eLoveJudgment       = 2,
        eOver               = 3,
        eWin                = 4,
        eMax
    }

    [System.Serializable]
    public class COneMessage
    {
        public EMessageType m_Type = EMessageType.eMyMessage;
        public string m_Messagestr = "";

    }

    [CreateAssetMenu(
    menuName = "Data/Message List Data",
    fileName = "Message List")]
    public class CMessageList : ScriptableObject
    {
        [SerializeField]
        protected List<COneMessage> m_ListMessage = new List<COneMessage>();
        [SerializeField]
        protected CMessageList[] m_SelectMessageList = new CMessageList[(int)ESelectType.eMax];
        //[SerializeField]
        //protected CMessageList[] m_LoveMessageList = new CMessageList[(int)ESelectType.eMax];
        [SerializeField]
        protected CMessageList m_NextQuestion = null;
        [SerializeField]
        protected int m_LoveAdd = 0;
        [SerializeField]
        protected EDialogueBreakpoint m_Breakpoint = EDialogueBreakpoint.eMax;

        public List<COneMessage> ListMessage => m_ListMessage;
        public CMessageList[] SelectMessageList => m_SelectMessageList;
        //public CMessageList[] LoveMessageList => m_LoveMessageList;
        public int LoveAdd => m_LoveAdd;
        public CMessageList NextQuestion => m_NextQuestion;
        public EDialogueBreakpoint Breakpoint => m_Breakpoint;

    }
}
