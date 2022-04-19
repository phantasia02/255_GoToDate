using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using Dreamteck.Splines;

public class CAll3DLineCtrl : MonoBehaviour
{

    public enum E3DLineCtrlState
    {
        eNull           = 0,
        eReady          = 1,
        eMouseDrag      = 2,
        eReadyEnd       = 3,
        eEnd            = 4,
        eMax
    }

    public class DataPointInfo
    {
        public Vector3 m_ScreenPoint        = Vector3.zero;
        public Vector3 m_2DPoint            = Vector3.zero;
        public Vector3 m_2DDir              = Vector3.zero;
        public float   m_CurAccumulationDis = 0.0f; 
        public float   m_Dis                = 0.0f;
    }

    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject       m_UIParent  = null;
    [SerializeField] protected float            m_MaxBlendVal = 1.0f;
    [SerializeField] protected float            m_AddLoveRefVal = 1.0f;
    [SerializeField] protected float            m_MaxLoveVal = 100.0f;
    public float MaxLoveVal => m_MaxLoveVal;

    [SerializeField] protected SplineFollower m_CurRatioLoveVal = null;
    [SerializeField] protected GameObject       m_ShowLineCollider = null;

    

    // ==================== SerializeField ===========================================

    protected CCamGLDraw m_CamGLDraw = null;
    protected List<DataPointInfo> m_All2DDataPointInfo = new List<DataPointInfo>();
    protected DataPointInfo m_CurPointData = null;
    protected DataPointInfo m_NextPointData = null;
    protected int m_CurIndex = 0;
    protected float m_NextOKRange = 0.0f;
    protected float m_TotalDis = 1.0f;
    protected float m_CurDis = 0.0f;
    protected float m_TargetDis = 0.0f;
    protected float m_CrossConfirm = 1.0f;

    protected Animator m_MyCtrlAnimator = null;
    public Animator MyCtrlAnimator
    {
        set => m_MyCtrlAnimator = value;
        get => m_MyCtrlAnimator;
    }

    private void Awake()
    {

        var lTempUpdateAs = this.UpdateAsObservable();
    
        float CMinPix = Screen.dpi * 0.05f;

        lTempUpdateAs.Where(X => OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag || OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eReadyEnd)
           .Subscribe(framcount =>
           {
               bool lTempb = Mathf.Abs(m_TargetDis - m_CurDis) < CMinPix;

               if (lTempb)
                   m_CurDis = m_TargetDis;
               else
                   m_CurDis = Mathf.Lerp(m_CurDis, m_TargetDis, Time.deltaTime * 10.0f);


               float lTempBlendRatio = 0.0f;
               if (lTempb && OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eReadyEnd)
               {
                   lTempBlendRatio = (Mathf.Abs(m_TargetDis) / m_TotalDis);
                   StaticGlobalDel.SetAnimatorFloat(MyCtrlAnimator, CGGameSceneData.g_AnimatorHashReadBlend, m_MaxBlendVal * lTempBlendRatio);
                   OBE3DLineCtrlStateVal().Value = E3DLineCtrlState.eEnd;
               }
               else
               {
                   lTempBlendRatio = (Mathf.Abs(m_CurDis) / m_TotalDis);
                   StaticGlobalDel.SetAnimatorFloat(MyCtrlAnimator, CGGameSceneData.g_AnimatorHashReadBlend, m_MaxBlendVal * lTempBlendRatio);
               }


               OBCurLoveVal().Value = Mathf.CeilToInt((float)m_MaxLoveVal * lTempBlendRatio);
           }).AddTo(this);

        m_CamGLDraw = Camera.main.gameObject.GetComponent<CCamGLDraw>();
        m_NextOKRange = Screen.dpi * 0.5f;
        m_NextOKRange = m_NextOKRange * m_NextOKRange;
        m_CrossConfirm = Screen.dpi * 1.0f;
    }

    private void Start()
    {
      
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eReady;
       // m_All2DDataPointInfo[m_All2DDataPointInfo.Count - 1].m_CurAccumulationDis = m_TotalDis;
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

    public void UpdateNextPointData()
    {
        if (m_CurIndex + 1 >= m_All2DDataPointInfo.Count)
        {
            m_NextPointData = null;
            Debug.Log("Win!!!!!!!!!!");
            m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eReadyEnd;
        }
        else
            m_NextPointData = m_All2DDataPointInfo[m_CurIndex + 1];
    }

    public void OnMouseDown()
    {


        if (m_OBE3DLineCtrlState.Value != E3DLineCtrlState.eReady)
            return;

        //Debug.Log("OnMouseDown");

        //if (m_All2DDataPointInfo.Count <= m_CurIndex)
        //    return;
        
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eMouseDrag;


        m_ShowLineCollider.gameObject.SetActive(true);

        //m_CurPointData = m_All2DDataPointInfo[m_CurIndex];
        //UpdateNextPointData();

        //if (m_CamGLDraw != null)
        //{
        //    m_CamGLDraw.startVertex = m_CurPointData.m_ScreenPoint;
        //    m_CamGLDraw.OpenDrewGL = true;
        //}

        SplineSample lTempSplineSample = m_CurRatioLoveVal.spline.Project(this.transform.position);
        m_CurRatioLoveVal.SetPercent(0.0f);
    }


   

    public void OnMouseDrag()
    {
        if (OBE3DLineCtrlStateVal().Value == E3DLineCtrlState.eMouseDrag)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SplineSample lTempSplineSample = m_CurRatioLoveVal.spline.Project(hit.point);

                m_CurRatioLoveVal.SetPercent(lTempSplineSample.percent);
                m_TargetDis = (float)lTempSplineSample.percent;
            }
            else
                m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eReadyEnd;
        }
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        m_OBE3DLineCtrlState.Value = E3DLineCtrlState.eReadyEnd;
    }

    // ===================== UniRx ======================

    protected UniRx.ReactiveProperty<E3DLineCtrlState> m_OBE3DLineCtrlState = new ReactiveProperty<E3DLineCtrlState>(E3DLineCtrlState.eNull);
    protected UniRx.ReactiveProperty<int> m_CurLoveVal = new ReactiveProperty<int>(0);

    public UniRx.ReactiveProperty<E3DLineCtrlState> OBE3DLineCtrlStateVal()
    {
        return m_OBE3DLineCtrlState ?? (m_OBE3DLineCtrlState = new ReactiveProperty<E3DLineCtrlState>(E3DLineCtrlState.eNull));
    }

    public UniRx.ReactiveProperty<int> OBCurLoveVal()
    {
        return m_CurLoveVal ?? (m_CurLoveVal = new ReactiveProperty<int>(0));
    }

    // ===================== UniRx ======================
}
