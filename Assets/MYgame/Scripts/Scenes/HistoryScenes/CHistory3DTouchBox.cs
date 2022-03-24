using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CHistory3DTouchBox : CTouchBox
{
    protected UnityAction[] m_MoveCall = new UnityAction[(int)CHistoryScenes.EMoveType.eMax];
    protected bool m_moveOK = false;
    protected bool m_MouseDown = false;

    private void Awake()
    {
        CHistoryScenes lTempHistoryScenes = this.GetComponentInParent<CHistoryScenes>();

        if (lTempHistoryScenes != null)
        {
            m_MoveCall[(int)CHistoryScenes.EMoveType.eRight] = lTempHistoryScenes.LeftMove;
            m_MoveCall[(int)CHistoryScenes.EMoveType.eLeft] = lTempHistoryScenes.RightMove;
        }
    }

    private void OnMouseDown()
    {
        m_DragAnimation.Down(Input.mousePosition);
        m_moveOK = false;
        m_MouseDown = true;
    }

    private void OnMouseDrag()
    {
        if (m_moveOK)
            return;

        if (!m_MouseDown)
            return;

        m_DragAnimation.Move(Input.mousePosition);

        float lTempX = m_DragAnimation.m_Direction.x;
        if (Mathf.Abs(lTempX) > 20.0f)
        {
            if (lTempX > 0.0f)
                m_MoveCall[(int)CHistoryScenes.EMoveType.eRight]();
            else
                m_MoveCall[(int)CHistoryScenes.EMoveType.eLeft]();


            m_DragAnimation.ClearInit();
            m_DragAnimation.m_StartPos = Input.mousePosition;
            m_moveOK = true;
        }
    }

    private void OnMouseUp()
    {
        m_DragAnimation.Up();
        m_MouseDown = m_moveOK = false;
    }
}
