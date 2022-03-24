using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Window;
using UnityEngine.UI;

public class CV1BrickStatusUI : BrickStatusUI
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected Image m_CompleteBar = null;

    // ==================== SerializeField ===========================================

    public override void SetNumber(int number)
    {
        float lTempVal = Mathf.Clamp((float)number / (float)_maxNumber, 0.0f, 1.0f);
        m_CompleteBar.fillAmount = lTempVal;
        base.SetNumber(number);
    }
}
