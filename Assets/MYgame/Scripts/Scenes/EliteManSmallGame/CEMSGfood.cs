using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CEMSGfood : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    

    // ==================== SerializeField ===========================================

    public void OnMouseDown()
    {
        ObClickReturnVal().OnNext(this);
    }

    // ===================== UniRx ======================

    [SerializeField] UniRx.Subject<CEMSGfood> m_ClickReturn = null;

    public UniRx.Subject<CEMSGfood> ObClickReturnVal()
    {
        return m_ClickReturn ?? (m_ClickReturn = new UniRx.Subject<CEMSGfood>());
    }

    // ===================== UniRx ======================
}
