using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLoveUIDegreeCompletion : MonoBehaviour
{
    [SerializeField] protected Image m_LoveProgression = null;
    [SerializeField] protected CUIText m_LoveText = null;


    public void SetLoveProgressionVal(float val)
    {
        if (val < 0.0f)
            val = 0.0f;
        else if (val > 1.0f)
            val = 1.0f;

        m_LoveProgression.fillAmount = val;
        m_LoveText.SetText($"{(int)(val * 100.0f)}%");
    }
}
