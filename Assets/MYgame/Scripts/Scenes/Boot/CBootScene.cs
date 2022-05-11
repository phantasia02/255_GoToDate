using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CBootScene : MonoBehaviour
{
    [SerializeField] protected int m_InitLevelIndex = 0;
    CChangeScenes m_ChangeScenes = new CChangeScenes();

    // Start is called before the first frame update
    void Start()
    {
       

        if (CSaveManager.m_status.LevelIndex.Value == 0)
            CSaveManager.m_status.LevelIndex.Value = 0;

#if UNITY_EDITOR

        if (m_InitLevelIndex > 0)
            CSaveManager.m_status.LevelIndex.Value = m_InitLevelIndex - 1;
#endif
        CSaveManager.Save();

        m_ChangeScenes.LoadGameScenes();
    }

}
