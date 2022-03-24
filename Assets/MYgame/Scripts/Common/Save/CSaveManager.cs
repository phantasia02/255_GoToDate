using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class DataBrickObj
{
    public void Add(int addcount) { count += addcount; }
    public int count = 0;
}

public class CSaveManager : CSingletonMonoBehaviour<CSaveManager>
{
    const int CHistoryModelMaxCount = 200;
    public static readonly Vector3 CHistoryModelMaxSize = Vector3.one * 40.0f; 

    public enum EHistoryMultiplier
    {
        eZero = 0,
        e1000 = 1,
        e2000 = 2,
        e3000 = 3,
        e4000 = 4,
        eMax = 5,
    };

    [System.Serializable]
    public class SeveDataCompleteBuilding
    {
        public int count = 0;
        public int NewObj = 0;
    }

    [System.Serializable]
    public class Status
    {
        public int                                  m_LevelIndex                    = 0;
        public int                                  m_Money                         = 0;
        public int                                  m_SceneIndex                    = 0;
        public int                                  m_Coin                          = 0;
        public SeveDataCompleteBuilding[]           m_AllSeveDataCompleteBuilding   = new SeveDataCompleteBuilding[(int)StaticGlobalDel.ECompleteBuilding.eMax];
        public PlayerOwnCar[]                       m_AllPlayerOwnCar               = new PlayerOwnCar[(int)CGGameSceneData.EPlayerTrailerType.eMax];
        public CGGameSceneData.EPlayerTrailerType   m_CurPlayerTrailer              = CGGameSceneData.EPlayerTrailerType.ePotoType;

        public DataBrickObj[]                       m_AllBrickColorObj              = new DataBrickObj[(int)StaticGlobalDel.EBrickColor.eMax];
        public bool m_InitGameOK = false;
        // =================== UniRx =====================

        public ReactiveProperty<int> Coin = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> LevelIndex = new ReactiveProperty<int>(0);
        public ReactiveProperty<CGGameSceneData.EPlayerTrailerType> CurPlayerTrailer = new ReactiveProperty<CGGameSceneData.EPlayerTrailerType>(CGGameSceneData.EPlayerTrailerType.ePotoType);

        // =================== UniRx =====================
    }

    [System.Serializable]
    public class Config
    {
        public int m_Sound = 1;
        public int m_Vibrate = 1;
    }


    const string SaveKey_status = "GameData.Status";
    const string SaveKey_Config = "GameData.Config";

    public static Status m_status;
    public static Config m_config;

    public static CHistoryScenes.DataHistoryCompleteBuildingShow[] m_HistoryCompleteBuildSize = new CHistoryScenes.DataHistoryCompleteBuildingShow[(int)EHistoryMultiplier.eMax];



    private void Awake()
    {
// PlayerPrefs.DeleteAll();

        m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.eZero] = new CHistoryScenes.DataHistoryCompleteBuildingShow(Vector3.one , 0, EHistoryMultiplier.eZero);
        m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.e1000] = new CHistoryScenes.DataHistoryCompleteBuildingShow(Vector3.one , 1000, EHistoryMultiplier.e1000);
        m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.e2000] = new CHistoryScenes.DataHistoryCompleteBuildingShow(new Vector3(1.5f, 2.0f, 1.5f), 2000, EHistoryMultiplier.e2000);
        m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.e3000] = new CHistoryScenes.DataHistoryCompleteBuildingShow(new Vector3(2.0f, 3.0f, 2.0f), 3000, EHistoryMultiplier.e3000);
        m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.e4000] = new CHistoryScenes.DataHistoryCompleteBuildingShow(new Vector3(2.5f, 4.0f, 2.5f), 4000, EHistoryMultiplier.e4000);


        string lTempDataStr;

        m_status = new Status();
        lTempDataStr = PlayerPrefs.GetString(SaveKey_status);
        if (lTempDataStr.Length != 0)
            m_status = JsonUtility.FromJson<Status>(lTempDataStr);

        m_config = new Config();
        lTempDataStr = PlayerPrefs.GetString(SaveKey_Config);
        if (lTempDataStr.Length != 0)
            m_config = JsonUtility.FromJson<Config>(lTempDataStr);


        // =================== UniRx init =====================

        m_status.Coin.Value = m_status.m_Coin;
        m_status.LevelIndex.Value = m_status.m_LevelIndex;
        m_status.CurPlayerTrailer.Value = m_status.m_CurPlayerTrailer;

        m_status.Coin.Subscribe(V => { m_status.m_Coin = V; }).AddTo(this);
        m_status.LevelIndex.Subscribe(V => { m_status.m_LevelIndex = V; }).AddTo(this);
        m_status.CurPlayerTrailer.Subscribe(V => { m_status.m_CurPlayerTrailer = V; }).AddTo(this);

        // =================== UniRx init =====================

        if (!m_status.m_InitGameOK)
        {
            InitData();
            m_status.m_InitGameOK = true;
        }


        //PlayerPrefs.GetString("GameData");
        // PrefsSerialize.Load("savedata_status", status, false);
       // print(CSaveManager.m_status.m_AllPlayerOwnCar.Length);

        Save();
    }

    public void Start()
    {
//#if DEBUGPC
//        foreach (var item in m_status.m_AllPlayerOwnCar)
//        {
//            if (item.Status == CarStatus.Lock)
//                item.Status = CarStatus.Unlock;
//        }
//#endif
    }

    public void InitData()
    {
        for (int i = 0; i < (int)CGGameSceneData.EPlayerTrailerType.eMax; i++)
            m_status.m_AllPlayerOwnCar[i] = new PlayerOwnCar((CGGameSceneData.EPlayerTrailerType)i, CarStatus.Lock, true);

        m_status.m_AllPlayerOwnCar[(int)CGGameSceneData.EPlayerTrailerType.ePotoType].SetData(CarStatus.Bought, true);
        m_status.m_AllPlayerOwnCar[(int)CGGameSceneData.EPlayerTrailerType.eTruck2].SetData(CarStatus.Unlock, true);
        m_status.m_AllPlayerOwnCar[(int)CGGameSceneData.EPlayerTrailerType.eTruck3].SetData(CarStatus.Unlock, true);

        for (int i = 0; i < m_status.m_AllSeveDataCompleteBuilding.Length; i++)
        {
            if (m_status.m_AllSeveDataCompleteBuilding[i] == null)
                m_status.m_AllSeveDataCompleteBuilding[i] = new SeveDataCompleteBuilding();
        }

        for (int i = 0; i < m_status.m_AllBrickColorObj.Length; i++)
        {
            if (m_status.m_AllBrickColorObj[i] == null)
                m_status.m_AllBrickColorObj[i] = new DataBrickObj();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Debug.Log($"Save!!!!!!!!");
            for (int i = 0; i < m_status.m_AllSeveDataCompleteBuilding.Length; i++)
                m_status.m_AllSeveDataCompleteBuilding[i].count += (i + i) * 3;


            m_status.m_AllSeveDataCompleteBuilding[5].count = 0;

            CSaveManager.Save();
        }
    }

    public static void Save()
    {
        string lTemp = JsonUtility.ToJson(m_status);
        Debug.Log(lTemp);
        PlayerPrefs.SetString(SaveKey_status, JsonUtility.ToJson(m_status));
        PlayerPrefs.SetString(SaveKey_Config, JsonUtility.ToJson(m_config));
        //PrefsSerialize.Save("savedata_status", status);
        //PrefsSerialize.Save("savedata_config", config);
        //PrefsSerialize.Save("savedata_design", design);
    }

    public static CHistoryScenes.DataHistoryCompleteBuildingShow IndextoHistoryMultiplier(int index)
    {
        if (index < 0 || index >= m_status.m_AllSeveDataCompleteBuilding.Length)
            return null;

        int lTempCompleteBuildingCount = m_status.m_AllSeveDataCompleteBuilding[index].count;

        CHistoryScenes.DataHistoryCompleteBuildingShow ReturnHistoryComplete = null;

        if (lTempCompleteBuildingCount == 0)
            ReturnHistoryComplete = m_HistoryCompleteBuildSize[(int)EHistoryMultiplier.eZero];
        else
        {
            float lTempRatio = (float)lTempCompleteBuildingCount / (float)CHistoryModelMaxCount;
            lTempRatio = Mathf.Min(1.0f, lTempRatio);

            //if (index == (int)StaticGlobalDel.ECompleteBuilding.eFerrisWheel)
            ReturnHistoryComplete = new CHistoryScenes.DataHistoryCompleteBuildingShow(Vector3.Lerp(Vector3.one, CHistoryModelMaxSize, lTempRatio), 1000, EHistoryMultiplier.e1000);
            //else
            //   ReturnHistoryComplete = new CHistoryScenes.DataHistoryCompleteBuildingShow(Vector3.Lerp(Vector3.one, new Vector3(2.5f, 4.0f, 2.5f), lTempRatio), 1000, EHistoryMultiplier.e1000);
        }

        //int i = 0;
        //for (i = 0; i < m_HistoryCompleteBuildSize.Length; i++)
        //{
        //    if (lTempCompleteBuildingCount <= m_HistoryCompleteBuildSize[i].m_MinDemand)
        //        break;
        //}

        //if (i == m_HistoryCompleteBuildSize.Length)
        //    i = (int)EHistoryMultiplier.e4000;

        return ReturnHistoryComplete;
    }

    public static bool CheckNewCar()
    {
        foreach (var data in m_status.m_AllPlayerOwnCar)
        {
            if (!data.IsSeen)
                return true;
        }

        return false;
    }

    public static void ShopSkinOldCar()
    {
        foreach (var data in m_status.m_AllPlayerOwnCar)
        {
            if (data.IsSeen)
                data.IsSeen = true;
        }
    }

    public static bool HistoryCompleteNewModel()
    {
        for (int i = 0; i < m_status.m_AllSeveDataCompleteBuilding.Length; i++)
        {
            if (m_status.m_AllSeveDataCompleteBuilding[i].NewObj == 1 || m_status.m_AllSeveDataCompleteBuilding[i].NewObj == 2)
                return true;
        }

        return false;
    }

    public static void HistoryCompleteOldModel()
    {
        for (int i = 0; i < m_status.m_AllSeveDataCompleteBuilding.Length; i++)
        {
            if (m_status.m_AllSeveDataCompleteBuilding[i].NewObj == 2)
                m_status.m_AllSeveDataCompleteBuilding[i].NewObj = 3;
        }

    }

    public static int CompleteBuildingOKCount()
    {
        int OKCount = 0;
        for (int i = 0; i < m_status.m_AllSeveDataCompleteBuilding.Length; i++)
        {
            if (m_status.m_AllSeveDataCompleteBuilding[i].count > 0)
                OKCount++;
        }

        return OKCount;
    }
}
