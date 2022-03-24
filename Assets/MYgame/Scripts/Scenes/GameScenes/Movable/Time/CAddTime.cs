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

        CGGameSceneData lTempGGameSceneData = CGGameSceneData.SharedInstance;
        GameObject lTempTimeObj = GameObject.Instantiate(lTempGGameSceneData.m_AllOtherObj[(int)CGGameSceneData.EOtherObj.eTimeShow]);

        Vector3 lTempV3 = this.transform.position;
        BoxCollider lTempBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        lTempV3.y += lTempBoxCollider.size.y + 3.0f;
        lTempTimeObj.transform.position = lTempV3;

        lTempTimeObj.transform.SetParent(m_MyGameManager.transform);

        m_MyCanvasGroup = lTempTimeObj.GetComponent<CanvasGroup>();
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
