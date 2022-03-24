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
        eUIButton           = 2,
        eUITextShowCurMax   = 3,
        eMax
    }

    abstract public EUIElementType UIElementType();


}
