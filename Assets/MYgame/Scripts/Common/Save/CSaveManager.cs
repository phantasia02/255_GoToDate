using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MYgame.Scripts.Scenes.GameScenes.Data;

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
        public CDataRole                            m_MyRole                        = null;
        public string                               m_MyName                        = "nINA";
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




    private void Awake()
    {
        string lTempDataStr;

        m_status = new Status();
        lTempDataStr = PlayerPrefs.GetString(SaveKey_status);
        if (lTempDataStr.Length != 0)
            m_status = JsonUtility.FromJson<Status>(lTempDataStr);

        m_config = new Config();
        lTempDataStr = PlayerPrefs.GetString(SaveKey_Config);
        if (lTempDataStr.Length != 0)
            m_config = JsonUtility.FromJson<Config>(lTempDataStr);



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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Debug.Log($"Save!!!!!!!!");

            CSaveManager.Save();
        }
    }

    public static void Save()
    {
        //string lTemp = JsonUtility.ToJson(m_status);
        //Debug.Log(lTemp);
        //PlayerPrefs.SetString(SaveKey_status, JsonUtility.ToJson(m_status));
        //PlayerPrefs.SetString(SaveKey_Config, JsonUtility.ToJson(m_config));
        //PrefsSerialize.Save("savedata_status", status);
        //PrefsSerialize.Save("savedata_config", config);
        //PrefsSerialize.Save("savedata_design", design);
    }
}
