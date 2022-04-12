using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class CAll3DLineCtrl : MonoBehaviour
{
    public enum E3DLineCtrlState
    {
        eNull           = 0,
        eMouseDrag      = 1,
        eMax
    }

    public class DataPointInfo
    {
        public Vector3 m_2DPoint    = Vector3.zero;
        public Vector3 m_2DDir      = Vector3.zero;
    }


    [SerializeField] protected Material         m_2DLineMat = null;
    [SerializeField] protected LineRenderer     m_3DLine    = null;
    [SerializeField] protected GameObject       m_UIParent  = null;

    protected CCamGLDraw m_CamGLDraw = null;
    protected List<DataPointInfo> m_All2DDataPointInfo = new List<DataPointInfo>();
    protected DataPointInfo m_CurPointData = null;

    private void Awake()
    {
        m_3DLine = this.GetComponent<LineRenderer>();

        //var lTempUpdateAs = this.UpdateAsObservable();

        //lTempUpdateAs.Where(X => OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag)
        //   .Subscribe(framcount =>
        //   {

        //   }).AddTo(this);
        m_CamGLDraw = Camera.main.gameObject.GetComponent<CCamGLDraw>();

    }

    private void Start()
    {
        CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;

        GameObject lTempHitUI = null;
        Vector3[] lTempArrPos = new Vector3[20]; ;
        int lTempposCount  = m_3DLine.GetPositions(lTempArrPos);

        DataPointInfo lTempCurDataPoint   = null;
        DataPointInfo lTempPreviousDataPoint = null;

        for (int i = 0; i < lTempposCount; i++)
        {
            lTempHitUI = null;
            lTempHitUI = GameObject.Instantiate(lTempGameSceneData.m_AllOtherObj[(int)CGGameSceneData.EOtherObj.eHitUIObj], m_UIParent.transform);
            lTempHitUI.transform.position = Camera.main.WorldToScreenPoint(lTempArrPos[i]);

            lTempCurDataPoint = new DataPointInfo();
            lTempCurDataPoint.m_2DPoint = lTempArrPos[i];
          
            lTempCurDataPoint.m_2DPoint.x = lTempHitUI.transform.position.x / (float)Screen.width;
            lTempCurDataPoint.m_2DPoint.y = lTempHitUI.transform.position.y / (float)Screen.height;
            lTempCurDataPoint.m_2DPoint.z = 0.0f;
            Debug.Log($"lTempCurDataPoint.m_2DPoin = {lTempCurDataPoint.m_2DPoint}");
            //lTempCurDataPoint.m_2DPoint = lTempArrPos[i];
            m_All2DDataPointInfo.Add(lTempCurDataPoint);

            if (i != 0)
            {
                lTempPreviousDataPoint.m_2DDir = lTempCurDataPoint.m_2DPoint - lTempPreviousDataPoint.m_2DPoint;
                lTempPreviousDataPoint.m_2DDir.Normalize();
            }

            lTempPreviousDataPoint = lTempCurDataPoint;
        }

    }

    public void Update()
    {
        
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("OnTriggerEnter");
    //}

    //public void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("OnCollisionEnter");
    //}

    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");

        if (m_All2DDataPointInfo.Count == 0)
            return;
        
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eMouseDrag;
        m_CurPointData = m_All2DDataPointInfo[0];

        m_CamGLDraw.startVertex = m_CurPointData.m_2DPoint;
        m_CamGLDraw.OpenDrewGL = true;
    }

    public void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");



        if (OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag)
        {
            if (m_CurPointData == null)
                return;

            m_CamGLDraw.startVertex = m_CurPointData.m_2DPoint;
        }
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        m_CamGLDraw.OpenDrewGL = false;
    }



    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<E3DLineCtrlState> m_OBE3DLineCtrlState = new ReactiveProperty<E3DLineCtrlState>(E3DLineCtrlState.eNull);

    public UniRx.ReactiveProperty<E3DLineCtrlState> OBE3DLineCtrlStateVal()
    {
        return m_OBE3DLineCtrlState ?? (m_OBE3DLineCtrlState = new ReactiveProperty<E3DLineCtrlState>(E3DLineCtrlState.eNull));
    }

    // ===================== UniRx ======================
}
