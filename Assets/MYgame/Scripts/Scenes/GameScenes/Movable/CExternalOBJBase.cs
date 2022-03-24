using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CExternalOBJBase
{
    protected CMovableBase m_MyMovable = null;
    protected CGameManager m_MyGameManager = null;
    protected CMemoryShareBase m_MyMemoryShare = null;

    public CExternalOBJBase(CMovableBase pamMovableBase)
    {
        if (pamMovableBase == null)
            return;

        m_MyMovable = pamMovableBase;
        m_MyGameManager = m_MyMovable.GetComponentInParent<CGameManager>();
        if (m_MyGameManager == null)
            m_MyGameManager = GameObject.FindObjectOfType<CGameManager>();

        m_MyMemoryShare = pamMovableBase.MyMemoryShare;
    }
}
