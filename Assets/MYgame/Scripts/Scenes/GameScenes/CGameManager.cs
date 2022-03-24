using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using UniRx;
using MYgame.Scripts.Scenes.GameScenes.Data;
using MYgame.Scripts.Window;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class CGameManager : MonoBehaviour
{
    public enum EState
    {
        eReady              = 0,
        ePlay               = 1,
        eTutorial1          = 2,
        eTutorial2          = 3,
        eTutorial3          = 4,
        eGameOver           = 5,
        eWinUI              = 6,
        eMax
    };
    
    bool m_bDown = false;

    CChangeScenes m_ChangeScenes = new CChangeScenes();
    protected ResultWindow m_MyResultUI = null;
    public ResultWindow MyResultUI { get { return m_MyResultUI; } }

    protected Camera m_Camera = null;
    public Camera MainCamera { get { return m_Camera; } }

    protected CPlayer m_Player = null;
    public CPlayer Player { get { return m_Player; } }
    // ==================== SerializeField ===========================================

    //[SerializeField] GameObject m_WinCamera = null;
    //public GameObject WinCamera { get { return m_WinCamera; } }
    [Header("Result OBJ")]
    [SerializeField] protected CinemachineVirtualCamera m_NormalPlayerCam   = null;
    [SerializeField] protected GameObject   m_WinObjAnima                   = null;
    [SerializeField] protected GameObject   m_OverObjAnima                  = null;
    [SerializeField] protected GameObject   m_CountdownCam                  = null;
    [SerializeField] protected GameObject   m_OtherUIVcm                    = null;
    [SerializeField] protected GameObject   PrefabGameSceneData             = null;

    //[Header("Other UI")]
    //[SerializeField] protected GameObject   m_HistoryWindow     = null;
    // ==================== All ObjData  ===========================================

    protected CGameObjBasListData[]     m_AllGameObjBas     = new CGameObjBasListData[(int)CGameObjBas.EObjType.eMax];
    public CGameObjBasListData GetTypeGameObjBaseListData(CGameObjBas.EObjType type) { return m_AllGameObjBas[(int)type]; }

    protected CMovableBaseListData[]    m_AllMovableBase    = new CMovableBaseListData[(int)CMovableBase.EMovableType.eMax];
    public CMovableBaseListData GetTypeMovableBaseListData(CMovableBase.EMovableType type) { return m_AllMovableBase[(int)type]; }

    protected CActorBaseListData[]      m_AllActorBase      = new CActorBaseListData[(int)CActor.EActorType.eMax];
    public CActorBaseListData GetTypeActorBaseListData(CActor.EActorType type) { return m_AllActorBase[(int)type]; }


    // ==================== All ObjData ===========================================



    protected Dictionary<int, DataLevelAllColor> m_DictionaryDataLevelAllColor = new Dictionary<int, DataLevelAllColor>();

    // ==================== SerializeField ===========================================

    protected CountdownWindow m_CountdownWindow = null;
    protected CinemachineTargetGroup m_EndCinemachineTargetGroup = null;
    protected bool isApplicationQuitting = false;
    public bool GetisApplicationQuitting { get { return isApplicationQuitting; } }

    protected StageData m_CurStageData = null;
    public StageData MyTargetBuilding
    {
        get
        {
            if (m_CurStageData == null)
            {
                CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;
                m_CurStageData = lTempGameSceneData.LevelToStageData(CSaveManager.m_status.m_LevelIndex);
            }

            return m_CurStageData;
        }
    }

    //protected float m_TotleColorRatio = 1.0f;
    //public float TotleColorRatio => m_TotleColorRatio;

    private EState m_eCurState = EState.eReady;
    public EState CurState { get { return m_eCurState; } }
    protected float m_StateTime = 0.0f;
    protected float m_StateUnscaledTime = 0.0f;
    protected int m_StateCount = 0;
    protected Vector3 m_OldInput;
    protected float m_HalfScreenWidth = 600.0f;
    public float HalfScreenWidth => m_HalfScreenWidth;

    protected Transform m_StartPosition = null;
    public Transform StartPosition => m_StartPosition;

    protected int m_BuildingProgressOKCount = -1;
    public int BuildingProgressOKCount
    {
        get
        {
            if (m_BuildingProgressOKCount == -1)
            {

//#if DEBUGPC
//                m_BuildingProgressOKCount = MyTargetBuilding.TargetBuilding + 80;
//#else
                m_BuildingProgressOKCount = MyTargetBuilding.TargetBuilding;
//#endif
            }

            return m_BuildingProgressOKCount;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        const float HWRatioPototype = StaticGlobalDel.g_fcbaseHeight / StaticGlobalDel.g_fcbaseWidth;
        float lTempNewHWRatio = ((float)Screen.height / (float)Screen.width);
        m_HalfScreenWidth = (StaticGlobalDel.g_fcbaseWidth / 2.0f) * (lTempNewHWRatio / HWRatioPototype);
        m_StartPosition = GameObject.Find("StartPosition").transform;

        m_MyResultUI = gameObject.GetComponentInChildren<ResultWindow>(true);

        if (m_MyResultUI != null)
        {
            m_MyResultUI.AddWinCallBackFunc(OnNext);
            m_MyResultUI.AddLoseCallBackFunc(OnReset);
        }

        GameObject lTempCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        if (lTempCameraObj != null)
            m_Camera = lTempCameraObj.GetComponent<Camera>();

        m_EndCinemachineTargetGroup = this.GetComponentInChildren<CinemachineTargetGroup>();
        for (int i = 0; i < m_AllGameObjBas.Length; i++)
            m_AllGameObjBas[i] = new CGameObjBasListData();

        for (int i = 0; i < m_AllMovableBase.Length; i++)
            m_AllMovableBase[i] = new CMovableBaseListData();

        for (int i = 0; i < m_AllActorBase.Length; i++)
            m_AllActorBase[i] = new CActorBaseListData();


        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);

        //for (int i = 0; i < MyTargetBuilding.BrickRandomLevelAllColor.Length; i++)
        //{
        //    m_DictionaryDataLevelAllColor.Add(MyTargetBuilding.BrickRandomLevelAllColor[i].ID, MyTargetBuilding.BrickRandomLevelAllColor[i]);
        //    MyTargetBuilding.BrickRandomLevelAllColor[i].TotleColorRatio = 0.0f;
        //    foreach (DataColor Dcolor in MyTargetBuilding.BrickRandomLevelAllColor[i]._brickColors)
        //        MyTargetBuilding.BrickRandomLevelAllColor[i].TotleColorRatio += Dcolor._Ratio;
        //}

        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        lTempAudioManager.PlayBGM(CAudioManager.EBGM.eOutGame);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }
    
    
    public void SetState(EState lsetState)
    {
        if (lsetState == m_eCurState)
            return;

        if (m_eCurState == EState.eWinUI || m_eCurState == EState.eGameOver)
            return;

        EState lOldState = m_eCurState;
        m_StateTime = 0.0f;
        m_StateCount = 0;
        m_StateUnscaledTime = 0.0f;
        m_eCurState = lsetState;

        CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;

        switch (m_eCurState)
        {
            case EState.eReady:
                {
                }
                break;
            case EState.ePlay:
                {
                    //if (lTempGameSceneWindow != null)
                    //{
                    //    lTempGameSceneWindow.ShowObj(true);
                    //    lTempGameSceneWindow.MyGameStatusUI.StartTimer(TimeOut);
                    //}

                    
                }
                break;
            case EState.eTutorial1:
                {
                    m_Player.SetCurState(CMovableStatePototype.EMovableState.eWait, 2);
                    m_Player.ObserverMoveFramCount()
                        .Where(X => X == 60)
                        .Subscribe(V => {

                            lTempGameSceneWindow.ShowSpeedTutorial(true);
                            if (m_CountdownWindow != null)
                                m_CountdownWindow.ShowTutorial(true);

                            SetState(EState.eTutorial2);
                        }).AddTo(this);
                }
                break;
            case EState.eTutorial2:
                {
                    m_Player.SetCurState(CMovableStatePototype.EMovableState.eWait, 3);
                }
                break;
            case EState.eTutorial3:
                {
                    CGGameSceneData lTempGGameSceneData = CGGameSceneData.SharedInstance;
                    GameObject lTempObj = GameObject.Instantiate(lTempGGameSceneData.m_UIObj[(int)CGGameSceneData.EUIPrefab.eDeshTutorial], this.transform);
                    CDeshTutorialWindow lTempDeshTutorialWindow = lTempObj.GetComponent<CDeshTutorialWindow>();
                    lTempDeshTutorialWindow.ObserveClose().Subscribe(X=> {
                        SetState(EState.ePlay);
                        m_Player.SetCurState(CMovableStatePototype.EMovableState.eWait, 0);
                    });
                }
                break;
            case EState.eWinUI:
                {
                    lTempGameSceneWindow.StopTimer();
                    m_MyResultUI.OnDelayWin(2.0f);
                }
                break;
            case EState.eGameOver:
                {
                    //if (lTempGameSceneWindow)
                    //{
                    //    m_MyResultUI.SetCutTagetCoin(lTempGameSceneWindow.CurCoinNumber, lTempGameSceneWindow.TargetCoinNumber);
                    //}
                    //if (lTempGameSceneWindow)
                    //    lTempGameSceneWindow.ShowObj(false);
                   
                    m_MyResultUI.OnLose();
                }
                break;
        }
    }

    public void UsePlayTick()
    {
//#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            m_bDown = true;
            m_OldInput = Input.mousePosition;
            //InputRay();
           // OKAllGroupQuestionHole(0);
        }
        else if (Input.GetMouseButton(0))
        {
            //float moveX = (Input.mousePosition.x - m_OldInput.x) / m_HalfScreenWidth;
            //m_Player.SetXMove(moveX);
            //m_OldInput = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_bDown)
            {
                m_OldInput = Vector3.zero;
                m_bDown = false;
            }
        }

    }
    //private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    //List<string> testbugtext = new List<string>();
    //private void OnGUI()
    //{
    //    string test = "";
    //    for (int i = testbugtext.Count - 1; i >= 0; i--)
    //        test += $"{testbugtext[i]}\n";

    //    guiStyle.fontSize = 60; //change the font size
    //    GUI.Label(new Rect(10, 10, 1000, 2000), test, guiStyle);
    //}

    public void InputRay()
    {
        
    }

    public void OnNext()
    {
        m_ChangeScenes.SetNextLevel();
        m_ChangeScenes.LoadGameScenes();
    }

    public void OnReset()
    {
        m_ChangeScenes.ResetScene();
    }

    void OnApplicationQuit() { isApplicationQuitting = true; }

    //public void SetWinUI()
    //{
    //    SetState(EState.eWinUI);
       
    //}

    //public void SetLoseUI()
    //{
    //    SetState(EState.eGameOver);
    //}
    

    public DataLevelAllColor GetIDToRandomColor(int ID)
    {
        DataLevelAllColor lReturnData = null;
        if (m_DictionaryDataLevelAllColor.TryGetValue(ID, out lReturnData))
            return lReturnData;

        return lReturnData;
    }


    // ==================== All ObjData  ===========================================

    public void AddGameObjBasListData(CGameObjBas addGameObjBas)
    {
        if (isApplicationQuitting)
            return;

        if (addGameObjBas == null)
            return;

        int lTempTypeIndex = (int)addGameObjBas.ObjType();

        addGameObjBas.GameObjBasIndex = m_AllGameObjBas[lTempTypeIndex].m_GameObjBasListData.Count;
        m_AllGameObjBas[lTempTypeIndex].m_GameObjBasListData.Add(addGameObjBas);
        m_AllGameObjBas[lTempTypeIndex].m_GameObjBasHashtable.Add(addGameObjBas.GetInstanceID(), addGameObjBas);
    }

    public void RemoveGameObjBasListData(CGameObjBas addGameObjBas)
    {
        if (isApplicationQuitting)
            return;

        if (addGameObjBas == null)
            return;

        int lTempTypeIndex = (int)addGameObjBas.ObjType();
        List<CGameObjBas> lTempGameObjBasList = m_AllGameObjBas[lTempTypeIndex].m_GameObjBasListData;

        lTempGameObjBasList.Remove(addGameObjBas);
        for (int i = 0; i < lTempGameObjBasList.Count; i++)
            lTempGameObjBasList[i].GameObjBasIndex = i;

        m_AllGameObjBas[lTempTypeIndex].m_GameObjBasHashtable.Remove(addGameObjBas.GetInstanceID());
    }

    public void AddMovableBaseListData(CMovableBase addMovableBase)
    {
        if (addMovableBase == null)
            return;

        int lTempTypeIndex = (int)addMovableBase.MyMovableType();
        m_AllMovableBase[lTempTypeIndex].m_MovableBaseListData.Add(addMovableBase);
    }

    public void RemoveMovableBaseListData(CMovableBase removeMovableBase)
    {
        if (isApplicationQuitting)
            return;

        if (removeMovableBase == null)
            return;

        int lTempTypeIndex = (int)removeMovableBase.MyMovableType();
        List<CMovableBase> lTempMovableBaseList = m_AllMovableBase[lTempTypeIndex].m_MovableBaseListData;
        lTempMovableBaseList.Remove(removeMovableBase);
    }

    public void AddActorBaseListData(CActor addActorBase)
    {
        if (addActorBase == null)
            return;

        int lTempTypeIndex = (int)addActorBase.MyActorType();
        m_AllActorBase[lTempTypeIndex].m_ActorBaseListData.Add(addActorBase);
    }

    public void RemoveActorBaseListData(CActor removeActorBase)
    {
        if (isApplicationQuitting)
            return;

        if (removeActorBase == null)
            return;

        int lTempTypeIndex = (int)removeActorBase.MyActorType();
        List<CActor> lTempActorBaseList = m_AllActorBase[lTempTypeIndex].m_ActorBaseListData;
        lTempActorBaseList.Remove(removeActorBase);
    }

    // ==================== All ObjData  ===========================================
}
