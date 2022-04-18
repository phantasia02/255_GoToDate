using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIImage : CUIElementBase
{

    public override EUIElementType UIElementType() { return EUIElementType.eUIImage; }


    // ==================== SerializeField ===========================================

    [SerializeField] protected Image m_Image = null;


    // ==================== SerializeField ===========================================

    private void Awake()
    {
        if (m_Image == null)
            m_Image = this.GetComponentInChildren<Image>();
    }

    
}
