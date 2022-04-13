using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CAll3DLineCtrl : MonoBehaviour
{
    public int ReadBlendHash = 0;

    public enum E3DLineCtrlState
    {
        eNull           = 0,
        eReady          = 1,
        eMouseDrag      = 2,
        eEnd            = 3,
        eMax
    }

    public class DataPointInfo
    {
        public Vector3 m_ScreenPoint    = Vector3.zero;
        public Vector3 m_2DPoint        = Vector3.zero;
        public Vector3 m_2DDir          = Vector3.zero;
        public float   m_Dis            = 0.0f;
    }

    // ==================== SerializeField ===========================================

    [SerializeField] protected Material         m_2DLineMat = null;
    [SerializeField] protected GameObject       m_UIParent  = null;
    [SerializeField] protected float            m_MaxBlendVal = 1.0f;

    // ==================== SerializeField ===========================================

    protected LineRenderer     m_3DLine    = null;
    protected CCamGLDraw m_CamGLDraw = null;
    protected List<DataPointInfo> m_All2DDataPointInfo = new List<DataPointInfo>();
    protected DataPointInfo m_CurPointData = null;
    protected DataPointInfo m_NextPointData = null;
    protected int m_CurIndex = 0;
    protected float m_NextOKRange = 0.0f;
    protected float m_TotalDis = 1.0f;
    protected float m_CurDis = 0.0f;


    protected Animator m_MyCtrlAnimator = null;
    public Animator MyCtrlAnimator
    {
        set => m_MyCtrlAnimator = value;
        get => m_MyCtrlAnimator;
    }

    private void Awake()
    {
        m_3DLine = this.GetComponentInChildren<LineRenderer>();

        //var lTempUpdateAs = this.UpdateAsObservable();

        //lTempUpdateAs.Where(X => OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag)
        //   .Subscribe(framcount =>
        //   {

        //   }).AddTo(this);
        m_CamGLDraw = Camera.main.gameObject.GetComponent<CCamGLDraw>();
        m_NextOKRange = Screen.dpi * 0.5f;
        ReadBlendHash = Animator.StringToHash("Blend");
    }

    private void Start()
    {
        CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;

        Vector3[] lTempArrPos = new Vector3[20];
        Vector3 lTempHitPos = Vector3.zero;
        int lTempposCount  = m_3DLine.GetPositions(lTempArrPos);

        DataPointInfo lTempCurDataPoint   = null;
        DataPointInfo lTempPreviousDataPoint = null;
        m_TotalDis = 0.0f;

        for (int i = 0; i < lTempposCount; i++)
        {
            lTempHitPos = Camera.main.WorldToScreenPoint(lTempArrPos[i]);

            //GameObject lTempHitUI = null;
            //lTempHitUI = GameObject.Instantiate(lTempGameSceneData.m_AllOtherObj[(int)CGGameSceneData.EOtherObj.eHitUIObj], m_UIParent.transform);
            //lTempHitUI.transform.position = lTempHitPos;

            lTempCurDataPoint = new DataPointInfo();
          
            lTempCurDataPoint.m_2DPoint.x = lTempHitPos.x;
            lTempCurDataPoint.m_2DPoint.y = lTempHitPos.y;
            lTempCurDataPoint.m_ScreenPoint.x = lTempHitPos.x / (float)Screen.width;
            lTempCurDataPoint.m_ScreenPoint.y = lTempHitPos.y / (float)Screen.height;
            lTempCurDataPoint.m_2DPoint.z = lTempHitPos.z = 0.0f;
           // Debug.Log($"lTempCurDataPoint.m_2DPoin = {lTempCurDataPoint.m_2DPoint}");
            m_All2DDataPointInfo.Add(lTempCurDataPoint);

            if (i != 0)
            {
                lTempPreviousDataPoint.m_2DDir = lTempCurDataPoint.m_ScreenPoint - lTempPreviousDataPoint.m_ScreenPoint;
                lTempPreviousDataPoint.m_Dis = (lTempCurDataPoint.m_2DPoint - lTempPreviousDataPoint.m_2DPoint).magnitude;
                m_TotalDis += lTempPreviousDataPoint.m_Dis;
                lTempPreviousDataPoint.m_2DDir.Normalize();
            }

            lTempPreviousDataPoint = lTempCurDataPoint;
        }

        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eReady;
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

    public void UpdateNextCurPointData()
    {
        //if (m_NextPointData != null)
        //{
        //    Debug.Log("Win!!!!!!!!!!");
        //}

        m_CurDis += m_NextPointData.m_Dis;
        m_CurIndex = m_CurIndex + 1;
        m_CurPointData = m_NextPointData;
        UpdateNextPointData();
    }

    public void UpdateNextPointData()
    {
        if (m_CurIndex + 1 >= m_All2DDataPointInfo.Count)
        {
            m_NextPointData = null;
            Debug.Log("Win!!!!!!!!!!");
            m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eEnd;
        }
        else
            m_NextPointData = m_All2DDataPointInfo[m_CurIndex + 1];
    }

    public void OnMouseDown()
    {
        if (m_OBE3DLineCtrlState.Value != E3DLineCtrlState.eReady)
            return;

        Debug.Log("OnMouseDown");

        if (m_All2DDataPointInfo.Count <= m_CurIndex)
            return;
        
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eMouseDrag;
        m_CurPointData = m_All2DDataPointInfo[m_CurIndex];
        UpdateNextPointData();

        if (m_CamGLDraw != null)
        {
            m_CamGLDraw.startVertex = m_CurPointData.m_ScreenPoint;
            m_CamGLDraw.OpenDrewGL = true;
        }
    }


    public void OnMouseDrag()
    {
        if (OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag)
        {
            if (m_CurPointData == null)
                return;

            Vector3 lInputMousePoint = Input.mousePosition;
            Vector3 lScreenInputMousePos = Vector3.zero;
            Vector3 lScreenInputMouseNormal = Input.mousePosition;
            lScreenInputMouseNormal.x = lScreenInputMouseNormal.x / Screen.width;
            lScreenInputMouseNormal.y = lScreenInputMouseNormal.y / Screen.height;
            lScreenInputMousePos = lScreenInputMouseNormal;
            lScreenInputMouseNormal = lScreenInputMouseNormal - m_CurPointData.m_ScreenPoint;
            lScreenInputMousePos.z = lInputMousePoint.z = lScreenInputMouseNormal.z = 0.0f;

            Vector3 lTempCross = Vector3.Cross(m_CurPointData.m_2DDir, lScreenInputMouseNormal);
            
           // Debug.Log($"lTempCross = {lTempCross}");

            float lTempDis = Vector3.Distance(lInputMousePoint, m_NextPointData.m_2DPoint);

            if (lTempCross.z > 0.15f)
                m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eEnd;


            if (lTempDis < m_NextOKRange)
                UpdateNextCurPointData();

            float lTempCurDis = m_CurDis;

            if (m_CurPointData != null)
            {
                float lTempMouseDisVal = Vector3.Distance(lInputMousePoint, m_CurPointData.m_2DPoint);
                lTempMouseDisVal = Mathf.Min(m_CurPointData.m_Dis * 0.9f, lTempMouseDisVal);
                
                float lTempDotVal = Vector3.Dot(m_CurPointData.m_2DDir, lScreenInputMouseNormal);
                if (lTempDotVal > 0.0f)
                    lTempCurDis = m_CurDis + lTempMouseDisVal;
                else
                    lTempCurDis = m_CurDis - lTempMouseDisVal;
            }
            else
                lTempCurDis = m_TotalDis;

            StaticGlobalDel.SetAnimatorFloat(MyCtrlAnimator, ReadBlendHash, m_MaxBlendVal * (lTempCurDis / m_TotalDis));


            //
            // m_CurPointData.m_2DDir

            if (m_CamGLDraw != null && m_CurPointData != null)
                m_CamGLDraw.startVertex = m_CurPointData.m_ScreenPoint;
        }
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eEnd;
        if (m_CamGLDraw != null)
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
