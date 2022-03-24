using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CUITextImage : CUIText
{
    public override EUIElementType UIElementType() { return EUIElementType.eUITextImage; }
    // ==================== SerializeField ===========================================
    [SerializeField] protected Image m_Image = null;
    // ==================== SerializeField ===========================================

    protected override void Awake()
    {
        base.Awake();
        if (m_Image == null)
            m_Image = this.GetComponentInChildren<Image>(true);
    }

    public void SetImageColor(Color color)
    {
        m_Image.color = color;
    }

    public void SetSprite(Sprite setSprite)
    {
        m_Image.sprite = setSprite;
    }
}
