using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CAddTime : MonoBehaviour
{
    [SerializeField] protected float m_AddTime= 1.0f;

    protected CGameManager  m_MyGameManager     = null;
    protected CanvasGroup   m_MyCanvasGroup    = null;



    private void Start()
    {
        m_MyGameManager = GameObject.FindObjectOfType<CGameManager>();

    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticGlobalDel.TagPlayerRoll || other.tag == StaticGlobalDel.TagCompleteBuilding)
        {
            CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;
            if (lTempGameSceneWindow != null)
               lTempGameSceneWindow.AddTime(m_AddTime);
            
            m_MyCanvasGroup.DOFade(0.0f, 1.0f);
        }

    }
}
