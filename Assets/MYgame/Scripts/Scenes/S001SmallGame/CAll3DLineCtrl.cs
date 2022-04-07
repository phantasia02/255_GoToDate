using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAll3DLineCtrl : MonoBehaviour
{
    [SerializeField] protected LineRenderer m_3DLine = null;
    [SerializeField] protected GameObject m_UIParent = null;

    //protected LineRenderer m_3DLine = null;

    private void Awake()
    {
        m_3DLine = this.GetComponent<LineRenderer>();
        //UICanvas

        //m_3DLine.GetPosition
        //m_3DLine.positionCount

        //Vector3 lTempv3 = m_MyGameManager.MainCamera.WorldToScreenPoint(lTempMyTransformPosition);

        // GameObject

        CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;

        //GameObject lTempHitUI = GameObject.Instantiate(lTempGameSceneData.m_AllOtherObj[(int)CGGameSceneData.EOtherObj.eHitUIObj], m_UIParent.transform);

        //lTempHitUI
    }
}
