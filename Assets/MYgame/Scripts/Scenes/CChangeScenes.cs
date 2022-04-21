using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        DOTween.PauseAll();

        SceneManager.LoadScene(lScenesName);

        StaticGlobalDel.g_CurSceneName = lScenesName;
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
        DOTween.PauseAll();
        SceneManager.LoadScene(lTempIndex);
    }


    public void SetNextLevel()
    {
        int lTempNextLevelIndex = CSaveManager.m_status.m_LevelIndex;
        lTempNextLevelIndex++;

        //if (lTempNextLevelIndex >= GlobalData.SharedInstance.LevelGameObj.Length)
        //    lTempNextLevelIndex = 0;

        CSaveManager.m_status.LevelIndex.Value = lTempNextLevelIndex;
        CSaveManager.Save();
    }


    public void ResetScene()
    {
        DOTween.PauseAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
