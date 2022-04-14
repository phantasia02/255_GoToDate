using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;
using UnityEngine.Timeline;
using UnityEngine.Playables;


[System.Serializable]
public class CCompleteBuilding
{
    public GameObject m_Model = null;
    public Sprite m_UISprite = null;
}


[System.Serializable]
public class DataTimeLine
{
    public GameObject m_TimelineObj = null;
    public PlayableAsset m_TimelinePlayableAsset = null;
    public PlayableDirector m_TimelinePlayableDirector = null;

    public void ChangeTrackObj(string TrackStrName, GameObject ChangObj)
    {
        var outputs = m_TimelinePlayableAsset.outputs;
        foreach (var itm in outputs)
        {
            if (itm.streamName == TrackStrName)
                m_TimelinePlayableDirector.SetGenericBinding(itm.sourceObject, ChangObj);
        }
    }
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
        eHitUIObj       = 0,
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
    [SerializeField]  public    CDataRole[]                m_AllDataRole               = null;
    [SerializeField]  public    List<CDataSkinChange>      m_AllSuitSkin               = null;

    [SerializeField]  public    StageData[]                m_AllStageData              = null;
    [SerializeField]  public    GameObject                 m_PrefabEventSystem         = null;
    [SerializeField]  public    GameObject                 m_SaveManager               = null;
    [SerializeField]  public    GameObject                 m_AudioManager              = null;
    [SerializeField]  public    GameObject                 m_PlayerModle               = null;

    public static int g_AnimatorHashReadBlend = 0;


    private void Awake()
    {
        g_AnimatorHashReadBlend = Animator.StringToHash("Blend");

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
