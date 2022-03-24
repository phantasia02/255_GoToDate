using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CChangeScenes
{
    public enum EOtherScenes
    {
        eBoot             = 0,
        eHistorvWindow    = 1,
        eMax,
    };


    public void ChangeScenes(string lScenesName)
    {
        //SceneManager.LoadScene(lScenesName);

        //GlobalData.g_CurSceneName = lScenesName;

        //string[] sArray = lScenesName.Split(new string[] { GlobalData.g_scLevelPrefix }, StringSplitOptions.RemoveEmptyEntries);

        //if (sArray.Length == 1)
        //    GlobalData.g_LevelIndex = int.Parse(sArray[0]);
    }

    public void LoadGameScenes()
    {
        int lTempLevelindex = CSaveManager.m_status.LevelIndex.Value;

        int lTempLevelStage = lTempLevelindex / 3;
        int lTempDegreeDifficulty = lTempLevelindex % 3;

        if (lTempLevelStage > 3)
            lTempLevelStage = UnityEngine.Random.Range(0, 4);
        
        StaticGlobalDel.g_CurSceneLevelStage = lTempLevelStage;

        int lTempIndex = lTempLevelStage + 2;
        //if (LevelIndex != -1)
        //    lTempIndex = LevelIndex;
        //else
        //{
        //    if (lTempIndex >= SceneManager.sceneCountInBuildSettings)
        //        lTempIndex = 1;
        //}

        StaticGlobalDel.g_CurSceneName = StaticGlobalDel.g_GameScenesName;

        SceneManager.LoadScene(lTempIndex);
    }

    public void LoadTestScenes()
    {
        StaticGlobalDel.g_CurSceneName = StaticGlobalDel.g_testScenesName;
        SceneManager.LoadScene(StaticGlobalDel.g_CurSceneName);
    }

    public void SetNextLevel()
    {
        int lTempNextLevelIndex = CSaveManager.m_status.m_LevelIndex;
        lTempNextLevelIndex++;

        //if (lTempNextLevelIndex >= GlobalData.SharedInstance.LevelGameObj.Length)
        //    lTempNextLevelIndex = 0;

        CSaveManager.m_status.LevelIndex.Value = lTempNextLevelIndex;
        CSaveManager.HistoryCompleteOldModel();
        CSaveManager.Save();
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CSaveManager.HistoryCompleteOldModel();
        CSaveManager.Save();
    }

    public void AdditiveLoadScenes(String Scenestext, Action onSceneLoadedOK = null)
    {
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (onSceneLoadedOK != null)
                onSceneLoadedOK();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(Scenestext, LoadSceneMode.Additive);
    }

    public void RemoveScenes(String Scenestext)
    {
        Scene[] lTempCurrentSceneList = SceneManager.GetAllScenes();
        for (int i = 0; i < lTempCurrentSceneList.Length; i++)
        {
            if (Scenestext == lTempCurrentSceneList[i].name && lTempCurrentSceneList[i].isLoaded)
                SceneManager.UnloadSceneAsync(lTempCurrentSceneList[i].name);
        }
    }

    //public int NameToIndex(string lpScenesName)
    //{
    //    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    //    {
    //        if ( SceneManager.GetSceneByBuildIndex(i).name == lpScenesName)
    //            return i;
    //    }

    //    return -1;
    //}
}
