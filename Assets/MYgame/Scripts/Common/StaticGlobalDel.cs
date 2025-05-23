﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

public static class StaticGlobalDel 
{
    public enum EBoolState
    {
        eTrue           = 0,
        eTruePlaying    = 1,
        eFlase          = 2,
        eFlasePlaying   = 3,
        eMax
    }


    public enum EMovableState
    {
        eNull           = 0,
        eWait           = 1,
        eDrag           = 2,
        eMove           = 3,
        eAtk            = 4,
        eJump           = 5,
        eJumpDown       = 6,
        eHit            = 7,
        eWin            = 8,
        eDeath          = 9,
        eFinish         = 10,
        eMax
    }

    public enum ELayerIndex
    {
        eWater              = 4,
        eUI                 = 5,
        eFloor              = 6,
        eObjHitMask         = 7,
        eMax
    }

    public enum EStyle
    {
        eNormal     = 0,
        eSlow       = 1,
        eGroupPost  = 2,
        
        eMax
    }

    public enum EMoveStyle
    {
        eNormal         = 0,
        eRailingAction  = 1,
        eMax
    }


    public enum EBrickColor
    {
        eRed        = 0,
        eYellow     = 1,
        eGreen      = 2,
        eBlue       = 3,
        eWhite      = 4,
        eMax
    }

    public enum ECompleteBuilding
    {
        eBurjKhalifa    = 0,
        eMoai           = 1,
        eKremlin        = 2,
        eBadshahiMosque = 3,
        eBurjArab       = 4,
        eHagiaSophia    = 5,
        eFerrisWheel    = 6,
        eMaryAxe        = 7,
        eStatueOfLiberty = 8,
        eHimejiCastle   = 9,
        eMax,
    };

    static public CChangeScenes g_ChangeScenes = new CChangeScenes();

    static public string g_CurSceneName             = "Boot_";

    public const string g_GameScenesName            = "GameScenes";

    public const string g_ScenesNameSelectRole      = "SelectRole";
    public const string g_ScenesNameSelectObject    = "SelectObject";
    public const string g_ScenesNameChatroom        = "Chatroom";
    public const string g_ScenesNameDatingDress     = "DatingDress";
    public const string g_ScenesNameDatingMeet      = "DatingMeet";
    public const string g_ScenesNameEliteManLove    = "EliteManLove";

    public const string g_testScenesName            = "test";
    public const string g_ShowCurLevelNamePrefix    = "STAGE ";
    public const string g_HistorvWindowScenes       = "HistorvWindowScenes";


    public const string TagBrickObj         = "BrickObj";
    public const string TagPlayer           = "Player";
    public const string TagOriginBuilding   = "OriginBuilding";
    public const string TagPlayerRoll       = "PlayerRoll";
    public const string TagCompleteBuilding = "CompleteBuilding";
    public const string TagNormalBuilding   = "NormalBuilding";
    public const string TagEventSystem      = "EventSystem";
    public const string TagGameSceneData    = "CGGameSceneData";
    public const string TagSaveManager      = "SaveManager";
    public const string TagAudioManager     = "AudioManager";
    public const string TagManSpine2        = "ManSpine2";
    public const string TagManMouth         = "ManMouth";



    public const int g_WaterMask                    = 1 << (int)ELayerIndex.eWater;
    public const int g_FloorMask                    = 1 << (int)ELayerIndex.eFloor;
    public const int g_ObjHitMask                   = 1 << (int)ELayerIndex.eObjHitMask;


    public const int g_MaxFever             = 100;
    public const int g_LeveFever            = 33;
    public const int g_ChargeDefCountCount  = 10;

    public static int g_CurSceneLevelStage = 0;

    public const float  g_fcbaseWidth                   = 1080.0f;
    public const float  g_fcbaseHeight                  = 2340.0f;
    public const float  g_fcbaseOrthographicSize        = 18.75f;
    public const float  g_fcbaseResolutionWHRatio       = g_fcbaseWidth / g_fcbaseHeight;
    public const float  g_fcbaseResolutionHWRatio       = g_fcbaseHeight / g_fcbaseWidth;
    public const float  g_TUA                           = Mathf.PI * 2.0f;
    // ============= Speed ====================
    public const float g_DefMovableTotleSpeed = 15.0f;

    private static GameObject g_PlayerObj = null;
    public static GameObject PlayerObj => g_PlayerObj;

    private static CDataRole g_BuffMyRoleData = null;
    public static CDataRole BuffMyRoleData
    {
        set => g_BuffMyRoleData = value;
        get
        {
            if (g_BuffMyRoleData == null)
                g_BuffMyRoleData =  CGGameSceneData.SharedInstance.m_AllDataRole[CSaveManager.m_status.m_MyRoleIndex];

            return g_BuffMyRoleData;
        }
    }

    private static StageData g_StageData = null;
    public static StageData StageData
    {
        set => g_StageData = value;
        get
        {
            if (g_StageData == null)
            {
                CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;
                g_StageData = lTempGameSceneData.LevelToStageData(CSaveManager.m_status.m_LevelIndex);
            }

            return g_StageData;
        }
    }

    private static CDataObjChar g_TargetDataObj = null;
    public static CDataObjChar TargetDataObj
    {
        set => g_TargetDataObj = value;
        get
        {
            if (g_TargetDataObj == null)
            {
                // ========= Debug ===========
                if (CSaveManager.m_status.m_ObjTargetIndex < 0)
                    CSaveManager.m_status.m_ObjTargetIndex = 0;
                // ========= Debug ===========

                if (0 > CSaveManager.m_status.m_ObjTargetIndex || CSaveManager.m_status.m_ObjTargetIndex >= StageData.AllDataObjChar.Count)
                    return null;

                g_TargetDataObj = StageData.AllDataObjChar[CSaveManager.m_status.m_ObjTargetIndex];
            }

            return g_TargetDataObj;
        }
    }

    private static CDataSkinChange g_SelectSkin = null;
    public static CDataSkinChange SelectSkin
    {
        get => g_SelectSkin;
        set => g_SelectSkin = value;
    }

    public static void SetAnimatorFloat(Animator AnimatorObj, int iStringHash, float fnumber){AnimatorObj.SetFloat(iStringHash, fnumber);}

    public static Transform NewFxAddParentShow(this Transform ParentTransform, CGGameSceneData.EAllFXType Fxtype, Vector3 offsetPos)
    {
        CGGameSceneData lTempGGameSceneData = CGGameSceneData.SharedInstance;
        GameObject lTempFx = GameObject.Instantiate(lTempGGameSceneData.m_AllFX[(int)Fxtype], ParentTransform);
        lTempFx.transform.position = ParentTransform.position + offsetPos;

        return lTempFx.transform;
    }

    public static Transform NewFxAddParentShow(this Transform ParentTransform, CGGameSceneData.EAllFXType Fxtype)
    {
        CGGameSceneData lTempGGameSceneData = CGGameSceneData.SharedInstance;
        GameObject lTempFx = GameObject.Instantiate(lTempGGameSceneData.m_AllFX[(int)Fxtype], ParentTransform);
        lTempFx.transform.position = ParentTransform.position;

        return lTempFx.transform;
    }


    public static void CreateSingletonObj(GameObject PrefabGameSceneData)
    {
        string[] lTempAllGlobalObj ={
            TagGameSceneData,
            TagEventSystem,
            TagSaveManager,
            TagAudioManager,
        };

        CGGameSceneData lTempCGGameSceneData = null;
        GameObject lTempObj = null;

        for (int i = 0; i < lTempAllGlobalObj.Length; i++)
        {
            lTempObj = null;
            lTempObj = GameObject.FindWithTag(lTempAllGlobalObj[i]);
            if (lTempObj == null)
            {
                switch (lTempAllGlobalObj[i])
                {
                    case TagGameSceneData:
                        lTempObj = GameObject.Instantiate(PrefabGameSceneData);
                        lTempCGGameSceneData = CGGameSceneData.SharedInstance;
                        break;
                    case TagEventSystem:
                        lTempObj = GameObject.Instantiate(lTempCGGameSceneData.m_PrefabEventSystem);
                        break;
                    case TagSaveManager:
                        lTempObj = GameObject.Instantiate(lTempCGGameSceneData.m_SaveManager);
                        break;
                    case TagAudioManager:
                        lTempObj = GameObject.Instantiate(lTempCGGameSceneData.m_AudioManager);
                        break;
                }

                Object.DontDestroyOnLoad(lTempObj);
            }
        }
    }

    public static void SetMaterialRenderingMode(this Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                //  material.("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}


public class CLerpTargetPos
{
    float m_TargetRange = 0.1f;
    public float TargetRange
    {
        get { return m_TargetRange; }
        set { m_TargetRange = value; }
    }

    bool m_bTargetOK = false;
    public bool TargetOK
    {
        get { return m_bTargetOK; }
        set { m_bTargetOK = value; }
    }

    Vector3 m_Targetv3 = Vector3.zero;
    public Vector3 Targetv3
    {
        get { return m_Targetv3; }
        set { m_Targetv3 = value; }
    }

    Transform m_CurTransform = null;
    public Transform CurTransform
    {
        get { return m_CurTransform; }
        set { m_CurTransform = value; }
    }

    public void UpdateTargetPos()
    {
        if (!TargetOK)
        {
            if (Vector3.SqrMagnitude(CurTransform.position - Targetv3) > TargetRange)
                CurTransform.position = Vector3.Lerp(CurTransform.position, Targetv3, 0.5f);
            else
            {
                CurTransform.position = Targetv3;
                TargetOK = true;
            }
        }
    }
}

public class CObjPool<T>
{

    protected List<T>  m_AllCurObj     = new List<T>();
    public List<T>  AllCurObj { get { return m_AllCurObj; } }
    public int CurAllObjCount { get { return m_AllCurObj.Count; } }

    protected Queue<T> m_AllObjPool    = new Queue<T>();

    public delegate T NewObjDelegate();
    protected NewObjDelegate m_NewObjFunc = null;
    public NewObjDelegate NewObjFunc{set { m_NewObjFunc = value; }}

    public delegate void RemoveObjDelegate(T Obj);
    protected RemoveObjDelegate m_RemoveObjFunc = null;
    public RemoveObjDelegate RemoveObjFunc { set { m_RemoveObjFunc = value; } }

    public delegate void AddListObjDelegate(T Obj, int index);
    protected AddListObjDelegate m_AddListObjFunc = null;
    public AddListObjDelegate AddListObjFunc { set { m_AddListObjFunc = value; } }

    public void InitDefPool(int InitCount)
    {
        for (int i = 0; i < InitCount; i++)
        {
            T lTempTObj = m_NewObjFunc();

            if (m_RemoveObjFunc != null)
                m_RemoveObjFunc(lTempTObj);

            m_AllObjPool.Enqueue(lTempTObj);
        }
    }

    public T AddObj()
    {
        T lTempTObj;
        if (m_AllObjPool.Count == 0)
            lTempTObj = m_NewObjFunc();
        else
            lTempTObj = m_AllObjPool.Dequeue();

        if (m_AddListObjFunc != null)
            m_AddListObjFunc(lTempTObj, CurAllObjCount);

        m_AllCurObj.Add(lTempTObj);

        return lTempTObj;
    }

    public bool RemoveObj(T removeData)
    {
        bool lbTemp = m_AllCurObj.Remove(removeData);
        if (!lbTemp)
            return lbTemp;

        if (m_RemoveObjFunc != null)
            m_RemoveObjFunc(removeData);

        m_AllObjPool.Enqueue(removeData);

        return lbTemp;
    }

    
}
