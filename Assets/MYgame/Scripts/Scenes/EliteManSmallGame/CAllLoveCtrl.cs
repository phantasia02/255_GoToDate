using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllLoveCtrl : MonoBehaviour
{

    protected CLoveUIDegreeCompletion m_CtrlLoveIcon = null;

    protected float m_CurVal = 0.0f;
    protected float m_TargetVal = 0.0f;
    protected bool m_isupdate = false;


    private void Awake()
    {
        m_CtrlLoveIcon = this.GetComponentInChildren<CLoveUIDegreeCompletion>();
    }

    public void Update()
    {
        if (!m_isupdate)
            return;

        if (Mathf.Abs(m_TargetVal - m_CurVal) < 0.001f)
        {
            m_CurVal = m_TargetVal;
            m_isupdate = false;
        }
        else
            m_CurVal = Mathf.Lerp(m_CurVal, m_TargetVal, Time.deltaTime * 5.0f);

        m_CtrlLoveIcon.SetLoveProgressionVal(m_CurVal);
    }

    public void SetTargetLoveVal(float targetval)
    {
        m_isupdate = true;
        m_TargetVal = targetval;
    }
}
