using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CEMSGfood : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected CDataEliteManSmallGameFood m_FoodData = null;
    public CDataEliteManSmallGameFood FoodData => m_FoodData;

    // ==================== SerializeField ===========================================

    public void OnMouseDown()
    {
        OBClickReturnVal().OnNext(this);
    }

    // ===================== UniRx ======================

    [SerializeField] UniRx.Subject<CEMSGfood> m_ClickReturn = null;

    public UniRx.Subject<CEMSGfood> OBClickReturnVal()
    {
        return m_ClickReturn ?? (m_ClickReturn = new UniRx.Subject<CEMSGfood>());
    }

    // ===================== UniRx ======================
}
