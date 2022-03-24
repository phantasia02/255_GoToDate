using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UniRx;
using MYgame.Scripts.Scenes.GameScenes.Data;


public class DataPathNode
{
    public DataPathNode(Vector3 pos)
    {
        m_Postion = pos;
    }

    public Vector3 m_Postion = Vector3.zero;
}

/// <summary>
/// Player Memory Share Data
/// </summary>
public class CPlayerMemoryShare : CActorMemoryShare
{
    public CPlayer                              m_MyPlayer                  = null;
    public bool                                 m_bDown                     = false;
    public Vector3                              m_OldMouseDownPos           = Vector3.zero;
    public Vector3                              m_DownMouseDownPos          = Vector3.zero;
    public float                                m_DownTime                  = -1.0f;
    public Vector3                              m_OldMouseDragDirNormal     = Vector3.zero;

    public Vector3                              m_CtrlSkillBuffDir          = Vector3.zero;
    public Vector3                              m_SkillTargetpos            = Vector3.zero;

    public UniRx.ReactiveProperty<float>        m_AnimationVal              = new ReactiveProperty<float>(0.5f);
    public float                                m_valSpeed                  = 1700.0f;

    public int                                  m_EndIndex                  = 0;
    public StageData                            m_CurStageData              = null;

    public int                                  m_BuildingRecipeDataIndex       = 0;
    public int                                  m_BuildingRecipeDataNextIndex   = 0;

    public Transform                            m_RecycleBrickObj           = null;
    public Transform                            m_BuildingPos               = null;
    public Transform                            m_TailEnd                   = null;
    public Transform                            m_StartePos                 = null;


    public Vector3                              m_HitWaterPoint             = Vector3.zero;
    public GameObject                           m_HitObj                    = null;
    public int                                  m_MaxPathNodeCount          = 0;

    public DataBrickObj[]                       m_AllBrickColorObj          = new DataBrickObj[(int)StaticGlobalDel.EBrickColor.eMax];
    public CDataAllSkill                        m_AllSkill                  = new CDataAllSkill();
    public UniRx.ReactiveProperty<int>          m_ChargeCount               = new ReactiveProperty<int>(StaticGlobalDel.g_ChargeDefCountCount);
    public AfterImageEffects[]                  m_AllAfterImageEffects      = null;
    public UniRx.Subject<UniRx.Unit>            m_FritPlay                  = new UniRx.Subject<UniRx.Unit>();
    public UniRx.ReactiveProperty<int>          m_MoveFramCount             = null;

    public Transform[]                          m_AllWheel                  = null;
    public GameObject[]                         m_AllFxSpark                = null;
};

/// <summary>
/// Player Actor
/// </summary>
public class CPlayer : CActor
{
    public override EMovableType MyMovableType() { return EMovableType.ePlayer; }
    public override EObjType ObjType() { return EObjType.ePlayer; }
    public override EActorType MyActorType() { return EActorType.ePlayer; }

    protected float m_MaxMoveDirSize = 5.0f;
    public float MaxMoveDirSize => m_MaxMoveDirSize;

    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;

    // ==================== SerializeField ===========================================

    //[SerializeField] protected Transform m_RecycleBrickObj  = null;
    //[SerializeField] protected Transform m_BuildingPos      = null;
    //[SerializeField] protected Transform m_TailEnd          = null;
    [SerializeField] protected Transform[] m_AllWheel = null;
    [SerializeField] protected GameObject[] m_AllFxSpark = null;
    // ==================== SerializeField ===========================================

    public Transform            RecycleBrickObj     => m_MyPlayerMemoryShare.m_RecycleBrickObj;
    public Transform            BuildingPos         => m_MyPlayerMemoryShare.m_BuildingPos;
    public Transform            TailEnd             => m_MyPlayerMemoryShare.m_TailEnd;

    public int MaxPathNodeCount
    {
        set => m_MyPlayerMemoryShare.m_MaxPathNodeCount = value;
        get => m_MyPlayerMemoryShare.m_MaxPathNodeCount;
    }

    public float AnimationVal
    {
        set {
                float lTempValue = Mathf.Clamp(value, 0.0f, 1.0f);
                m_MyPlayerMemoryShare.m_AnimationVal.Value = lTempValue;
            }
        get { return m_MyPlayerMemoryShare.m_AnimationVal.Value; }
    }

    public Transform StartePos
    {
        set => m_MyPlayerMemoryShare.m_StartePos = value;
        get => m_MyPlayerMemoryShare.m_StartePos;
    }

    //public float SkillRadius
    //{
    //    set => m_MyPlayerMemoryShare.m_SkillRadius = value;
    //    get => m_MyPlayerMemoryShare.m_SkillRadius;
    //}

    protected Vector3 m_OldMouseDragDir = Vector3.zero;

    public override float DefSpeed { get { return m_MyPlayerMemoryShare.m_valSpeed; } }
    public float ValSpeed
    {
        set => m_MyPlayerMemoryShare.m_valSpeed = value;
        get => m_MyPlayerMemoryShare.m_valSpeed;
    }
                    
    int m_MoveingHash = 0;


    protected override void AddInitState()
    {
        m_AllState[(int)StaticGlobalDel.EMovableState.eWait].AllThisState.Add(new CWaitStatePlayer(this));
  
    }

    protected override void CreateMemoryShare()
    {
        m_MyPlayerMemoryShare = new CPlayerMemoryShare();
        m_MyMemoryShare = m_MyPlayerMemoryShare;

        m_MyPlayerMemoryShare.m_MyPlayer = this;
        m_MyPlayerMemoryShare.m_CurStageData    = m_MyGameManager.MyTargetBuilding;
        m_MyPlayerMemoryShare.m_AllWheel        = m_AllWheel;
        m_MyPlayerMemoryShare.m_AllFxSpark      = m_AllFxSpark;
        
        base.CreateMemoryShare();

        m_MaxMoveDirSize = Screen.width > Screen.height ? (float)Screen.width : (float)Screen.height;
        m_MaxMoveDirSize = m_MaxMoveDirSize / 5.0f;

        this.transform.position = m_MyGameManager.StartPosition.position;
        this.transform.rotation = m_MyGameManager.StartPosition.rotation;

        m_MyActorMemoryShare.m_MyActorCollider = this.transform.Find("Coillder").GetComponentsInChildren<Collider>(true);

        foreach (Collider cr in m_MyActorMemoryShare.m_MyActorCollider)
            cr.isTrigger = false;
        
        m_MyPlayerMemoryShare.m_AllAfterImageEffects = this.GetComponentsInChildren<AfterImageEffects>();

        for (int i = 0; i < m_MyPlayerMemoryShare.m_AllBrickColorObj.Length; i++)
        {
            if (m_MyPlayerMemoryShare.m_AllBrickColorObj[i] == null)
                m_MyPlayerMemoryShare.m_AllBrickColorObj[i] = new DataBrickObj();
        }

        // ============ Skill ==================
        m_MyPlayerMemoryShare.m_AllSkill.ListAllSkill.Add(new CPlayerChargeSkill(this));
    }
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //UpdateAnimationVal().Subscribe(_ => {
        //    UpdateAnimationChangVal();
        //}).AddTo(this.gameObject);

#if DEBUGPC
        CDebugWindow lTempDebugWindow = CDebugWindow.SharedInstance;
        if (lTempDebugWindow != null)
        {
            lTempDebugWindow.PlaySpeed.SetNumber((int)m_MyPlayerMemoryShare.m_valSpeed);
            lTempDebugWindow.ObserverSpeedVal().Subscribe(V => { m_MyPlayerMemoryShare.m_valSpeed = V; });
        }
#endif

    }

    public void InitGameStart()
    {
        //CGameSceneWindow lTempCGameSceneWindow = CGameSceneWindow.SharedInstance;
        //lTempCGameSceneWindow.SetData(m_MyPlayerMemoryShare.m_AllArchitecturalTopics, false);
    }

    public void UpdateAnimationChangVal()
    {
       // if (m_MyPlayerMemoryShare.m_isupdateAnimation)
            m_AnimatorStateCtl.SetFloat(m_MoveingHash, m_MyPlayerMemoryShare.m_AnimationVal.Value);
    }

    protected override void Update()
    {
        base.Update();

        InputUpdata();

        m_MyPlayerMemoryShare.m_AllSkill.UpdateSkill(Time.deltaTime);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void InputUpdata()
    {
#if DEBUGPC
        CDebugWindow lTempDebugWindow = CDebugWindow.SharedInstance;
        if (lTempDebugWindow != null)
        {
            if (lTempDebugWindow.IsShow())
                return;
        }
#endif

        if ((int)m_MyGameManager.CurState < (int)CGameManager.EState.ePlay || (int)m_MyGameManager.CurState > (int)CGameManager.EState.eTutorial2)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            PlayerMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerMouseUp();
        }

        if (Input.GetMouseButton(0))
        {
            PlayerMouseDrag();
        }
    }

    public void PlayerMouseDown()
    {
        DataState lTempDataState = m_AllState[(int)CurState];
        if (m_CurState != CMovableStatePototype.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
            lTempDataState.AllThisState[lTempDataState.index].MouseDown();

        if (!m_MyPlayerMemoryShare.m_bDown)
        {
            m_MyPlayerMemoryShare.m_bDown = true;
            m_MyPlayerMemoryShare.m_OldMouseDownPos = Input.mousePosition;
            m_MyPlayerMemoryShare.m_DownMouseDownPos = Input.mousePosition;
            m_MyPlayerMemoryShare.m_DownTime = Time.realtimeSinceStartup;
            m_MyPlayerMemoryShare.m_AllSkill.ListAllSkill[1].SaveBuffCtrl();
        }
    }

    public void PlayerMouseDrag()
    {
        if (!m_MyPlayerMemoryShare.m_bDown)
            return;

        DataState lTempDataState = m_AllState[(int)CurState];
        if (m_CurState != CMovableStatePototype.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
            lTempDataState.AllThisState[lTempDataState.index].MouseDrag();

        m_MyPlayerMemoryShare.m_OldMouseDownPos = Input.mousePosition;
    }

    public void PlayerMouseUp()
    {
        if (m_MyPlayerMemoryShare.m_bDown)
        {
            DataState lTempDataState = m_AllState[(int)CurState];
            if (m_CurState != CMovableStatePototype.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
                lTempDataState.AllThisState[lTempDataState.index].MouseUp();

            m_MyPlayerMemoryShare.m_bDown = false;
            m_MyPlayerMemoryShare.m_OldMouseDownPos = Vector3.zero;
            m_MyPlayerMemoryShare.m_DownTime = -1.0f;
        }
    }

    public DataBrickObj EnumGetDataBrickObj(StaticGlobalDel.EBrickColor lTempenum)
    {
        if (lTempenum == StaticGlobalDel.EBrickColor.eMax)
            return null;

        return m_MyPlayerMemoryShare.m_AllBrickColorObj[(int)lTempenum];
    }

    //public void UpdateBrickAmount()
    //{

    //}

    // ===================== UniRx ======================
    public UniRx.ReactiveProperty<int> ObserverChargeCountVal()
    {
        return m_MyPlayerMemoryShare.m_ChargeCount ?? (m_MyPlayerMemoryShare.m_ChargeCount = new ReactiveProperty<int>(StaticGlobalDel.g_ChargeDefCountCount));
    }

    public UniRx.Subject<UniRx.Unit> ObserverFritPlay()
    {
        return m_MyPlayerMemoryShare.m_FritPlay ?? (m_MyPlayerMemoryShare.m_FritPlay = new UniRx.Subject<UniRx.Unit>());
    }

    public UniRx.ReactiveProperty<int> ObserverMoveFramCount()
    {
        return m_MyPlayerMemoryShare.m_MoveFramCount ?? (m_MyPlayerMemoryShare.m_MoveFramCount = new ReactiveProperty<int>(0));
    }

    // ===================== UniRx ======================
}
