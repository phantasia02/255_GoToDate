using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;



[System.Serializable]
public class CCompleteBuilding
{
    public GameObject m_Model = null;
    public Sprite m_UISprite = null;
}


[System.Serializable]
public class TweenColorEaseCurve
{
    [SerializeField]
    [Tooltip("The ease curve")]
    private AnimationCurve _curve;

    [SerializeField]
    [Tooltip("The duration of this ease curve")]
    private float _duration;

    [SerializeField]
    [ColorUsage(true)]
    [Tooltip("The start value of the tween")]
    private Color _startValue;

    [SerializeField]
    [ColorUsage(true)]
    [Tooltip("The end value of the tween")]
    private Color _endValue;


    public AnimationCurve curve => _curve;
    public float duration => _duration;
    public Color StartValue => _startValue;
    public Color endValue => _endValue;
}

public class CGGameSceneData : CSingletonMonoBehaviour<CGGameSceneData>
{

    public enum EAllFXType
    {
        eSplash             = 0,
        eSpark              = 1,
        eMainFire           = 2,
        eChargeSkillMainFx  = 3,

        eMax,
    };

    public enum EOtherObj
    {
        eScoreTextObj       = 0,
        ePlayerObj          = 1,
        eTrailerObj         = 2,
        eTimeShow           = 3,
        eMax,
    };

    public enum EUIPrefab
    {
        eShopScenes         = 0,
        eCountdownWindow    = 1,
        eDeshTutorial       = 2,
        eMax,
    };

    public enum EPlayerTrailerType
    {
        ePotoType   = 0,
        eTruck2     = 1,
        eTruck3     = 2,
        eTruck4     = 3,
        eTruck5     = 4,
        eTruck6     = 5,
        eMax,
    };

    [SerializeField]  public    GameObject[]               m_AllFX                     = null;

    [VarRename(CGGameSceneData.EOtherObj.eMax)]
    [SerializeField]  public    GameObject[]               m_AllOtherObj               = null;
    [SerializeField]  public    GameObject[]               m_UIObj                     = null;
    [SerializeField]  public    CDataPlayerRelevant[]      m_AllDataPlayerRelevant     = null;

    [SerializeField]  public    StageData[]                m_AllStageData              = null;
    [SerializeField]  public    GameObject                 m_PrefabEventSystem         = null;
    [SerializeField]  public    GameObject                 m_SaveManager               = null;
    [SerializeField]  public    GameObject                 m_AudioManager              = null;

    [VarRename(new StaticGlobalDel.ECompleteBuilding[] 
    {
        (StaticGlobalDel.ECompleteBuilding)0,
        (StaticGlobalDel.ECompleteBuilding)1,
        (StaticGlobalDel.ECompleteBuilding)2,
        (StaticGlobalDel.ECompleteBuilding)3,
        (StaticGlobalDel.ECompleteBuilding)4,
        (StaticGlobalDel.ECompleteBuilding)5,
        (StaticGlobalDel.ECompleteBuilding)6,
        (StaticGlobalDel.ECompleteBuilding)7,
        (StaticGlobalDel.ECompleteBuilding)8,
        (StaticGlobalDel.ECompleteBuilding)9,
    })]
    [SerializeField]  public CCompleteBuilding[]    m_AllCompleteBuilding  = null;

    [VarRename(new string[] { "Red", "Yellow", "Green", "Blue", "White" })]
    [SerializeField]  public CDateBrick[]    m_AllDateBrick         = new CDateBrick[(int)StaticGlobalDel.EBrickColor.eMax];

    [SerializeField]  public BuildingRecipeData[] m_AllBuildingRecipeData = null;

    protected Dictionary<EPlayerTrailerType, CDataPlayerRelevant> MapCarsStatus = new Dictionary<EPlayerTrailerType, CDataPlayerRelevant>();
    public CDataPlayerRelevant GetPlayTypeData(EPlayerTrailerType type) { return MapCarsStatus[type]; }

    private void Awake()
    {
        for (int i = 0; i < m_AllDataPlayerRelevant.Length; i++)
            MapCarsStatus[(EPlayerTrailerType)i] = m_AllDataPlayerRelevant[i];
    }

    public StageData LevelToStageData(int levelindex)
    {
        if (levelindex < 0)
            return null;

        if (levelindex < m_AllStageData.Length)
            return m_AllStageData[levelindex];

        return m_AllStageData[(StaticGlobalDel.g_CurSceneLevelStage * 3) + levelindex % 3];
    }
}
