using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CTouchBox : MonoBehaviour
{
    public class CDragAnimation
    {
        public Vector3 m_StartPos   = Vector3.zero;
        public Vector3 m_EndPos     = Vector3.zero;
        public Vector3 m_Direction  = Vector3.zero;

        public void ClearInit(){m_Direction = m_EndPos = m_StartPos = Vector3.zero;}
        public void Down(Vector3 pos){m_StartPos = pos; }
        public void Move(Vector3 pos){m_Direction = pos - m_StartPos;}
        public void Up(){ClearInit();}
    }

    protected CDragAnimation m_DragAnimation = new CDragAnimation();
}
