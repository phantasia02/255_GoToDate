using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CUIElementBase : MonoBehaviour
{
    public enum EUIElementType
    {
        eUIText             = 0,
        eUITextImage        = 1,
        eUIImage            = 2,
        eUIButton           = 3,
        eUITextShowCurMax   = 4,
        eMax
    }

    abstract public EUIElementType UIElementType();

    protected RectTransform m_MyRectTransform = null;
    public RectTransform MyRectTransform
    {
        get
        {
            if (m_MyRectTransform == null)
                m_MyRectTransform = this.GetComponent<RectTransform>();

            return m_MyRectTransform;
        }
    }
}
