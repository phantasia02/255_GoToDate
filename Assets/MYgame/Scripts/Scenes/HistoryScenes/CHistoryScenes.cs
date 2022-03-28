using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHistoryScenes : MonoBehaviour
{

    public class CShowModel
    {
        public GameObject m_Model   = null;
        public int m_CurShowIndex   = 0;
        public int m_NextShowIndex  = -1;
        public Text m_ScoreText     = null;
        public Image m_NewImage     = null;
    }

    public enum EState
    {
        eNull = 0,
        eMove = 1,
        eMax
    }

    public enum EMoveType
    {
        eLeft = 0,
        eRight = 1,
        eMax
    }

    public const float  CFXInterval         = 100.0f;
    public const int    CNShowModelCount    = 5;
    public const float  CFMoveMaxTime       = 0.5f;

    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject       PrefabGameSceneData     = null;
    [SerializeField] protected Transform        m_AllShowModleParent    = null;
    [SerializeField] protected Material         m_ZeroMaterial          = null;
    [SerializeField] protected CHistoryWindow   m_HistoryWindow         = null;

    // ==================== All ObjData  ===========================================

    protected CShowModel[] m_AllModel = null;
    protected GameObject[] m_ShowModel = null;
    protected int m_CurCentralIndex = 0;
    protected float m_StateTime = 0.0f;

    private EState m_eCurState = EState.eNull;
    public EState CurState { get { return m_eCurState; } }
    protected int m_MoveIndex = 0;

    private void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);
        CGGameSceneData     lTempCGGameSceneData    = CGGameSceneData.SharedInstance;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_HistoryWindow.AddLeftBtnListener(LeftMove);
        m_HistoryWindow.AddRightBtnListener(RightMove);
        m_HistoryWindow.SetCompleteBuildingCount(m_AllModel[m_CurCentralIndex].m_ScoreText.text, false); 
    }

    // Update is called once per frame
    void Update()
    {
        m_StateTime += Time.deltaTime;

        switch (m_eCurState)
        {
            case EState.eNull:
                {
                    //  UsePlayTick();
                }
                break;
            case EState.eMove:
                {
                    float lTempRatio = m_StateTime / CFMoveMaxTime;

                    if (lTempRatio < 1.0f)
                    {
                        int lTempCurShowIndex = 0;
                        int lTempNextShowIndex = 0;

                        Vector3 lTemplerppos = Vector3.zero;
                        for (int i = 0; i < m_AllModel.Length; i++)
                        {
                            lTempCurShowIndex = m_AllModel[i].m_CurShowIndex;
                            lTempNextShowIndex = m_AllModel[i].m_NextShowIndex;

                            if (lTempNextShowIndex == -1)
                                continue;

                            lTemplerppos = Vector3.Lerp(m_ShowModel[lTempCurShowIndex].transform.position, m_ShowModel[lTempNextShowIndex].transform.position, lTempRatio);
                            m_AllModel[i].m_Model.transform.position = lTemplerppos;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < m_AllModel.Length; i++)
                        {
                            if (m_AllModel[i].m_NextShowIndex == -1)
                                continue;

                            m_AllModel[i].m_Model.transform.position = m_ShowModel[m_AllModel[i].m_NextShowIndex].transform.position;
                            m_AllModel[i].m_CurShowIndex = m_AllModel[i].m_NextShowIndex;
                            m_AllModel[i].m_NextShowIndex = -1;
                        }

                        SetState(EState.eNull);
                    }

                    
                }
                break;
        }
    }

    public void SetState(EState lsetState)
    {
        //if (lsetState == m_eCurState)
        //    return;

        EState lOldState = m_eCurState;
        m_StateTime = 0.0f;
        m_eCurState = lsetState;

        switch (m_eCurState)
        {
            case EState.eNull:
                {
                    m_HistoryWindow.SetCompleteBuildingCount(m_AllModel[m_CurCentralIndex].m_ScoreText.text);
                }
                break;
            case EState.eMove:
                {
                   
                }
                break;
        }
    }

    public void LeftMove()
    {
        if (m_eCurState != EState.eNull)
            return;

        for (int i = 0; i < m_AllModel.Length; i++)
        {
            m_AllModel[i].m_NextShowIndex = m_AllModel[i].m_CurShowIndex + 1;
            if (m_AllModel[i].m_NextShowIndex >= m_AllModel.Length)
            {
                m_AllModel[i].m_CurShowIndex = 0;
                m_AllModel[i].m_NextShowIndex = -1;
                m_AllModel[i].m_Model.transform.position = m_ShowModel[0].transform.position;
            }
        }

        m_CurCentralIndex--;
        if (m_CurCentralIndex < 0)
            m_CurCentralIndex = m_AllModel.Length - 1;

        SetState(EState.eMove);
    }

    public void RightMove()
    {
        if (m_eCurState != EState.eNull)
            return;


        for (int i = 0; i < m_AllModel.Length; i++)
        {
            m_AllModel[i].m_NextShowIndex = m_AllModel[i].m_CurShowIndex - 1;
            if (m_AllModel[i].m_NextShowIndex < 0)
            {
                m_AllModel[i].m_CurShowIndex = m_AllModel.Length - 1;
                m_AllModel[i].m_NextShowIndex = -1;
                m_AllModel[i].m_Model.transform.position = m_ShowModel[m_ShowModel.Length - 1].transform.position;
            }
        }

        m_CurCentralIndex++;
        if (m_CurCentralIndex >= m_AllModel.Length)
            m_CurCentralIndex = 0;

        SetState(EState.eMove);
    }


}
