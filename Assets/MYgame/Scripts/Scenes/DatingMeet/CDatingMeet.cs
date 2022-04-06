using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CDatingMeet : MonoBehaviour
{

    [SerializeField] protected GameObject m_WinTimeline = null;
    [SerializeField] protected GameObject m_OverTimeline = null;
    [SerializeField] protected SignalReceiver m_MyEndEvent = null;

    private void Awake()
    {
        

    }

    public void EndFunc()
    {
        Debug.Log("EndFunc OK");
    }

}
