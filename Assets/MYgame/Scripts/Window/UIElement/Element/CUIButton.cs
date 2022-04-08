using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class CUIButton : CUIElementBase
{
    public override EUIElementType UIElementType() { return EUIElementType.eUIButton; }

    // ==================== SerializeField ===========================================

    [SerializeField] protected Button           m_Button = null;
    [SerializeField] protected Image            m_Image = null;
    [SerializeField] protected TextMeshProUGUI  m_Text = null;
    public Button Button => m_Button;

    // ==================== SerializeField ===========================================


    private void Awake()
    {
        if (m_Button == null)
            m_Button = this.GetComponentInChildren<Button>(true);

        if (m_Image == null)
            m_Image = m_Button.GetComponent<Image>();

        if (m_Text == null)
            m_Text = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetSpriteState(SpriteState parSpriteState)
    {
        m_Button.spriteState = parSpriteState;

        if (m_Image == null)
            m_Image = m_Button.GetComponent<Image>();

        m_Image.sprite = m_Button.spriteState.highlightedSprite;
    }

    public void SetText(string text)
    {
        m_Text.text = text;
    }

    public void EnableButton(bool Enable)
    {
        m_Button.interactable = Enable;
    }

    public void AddListener(UnityAction call) { m_Button.onClick.AddListener(call); }
    public void RemoveListener(UnityAction call) { m_Button.onClick.RemoveListener(call); }
    public void RemoveAllListener() { m_Button.onClick.RemoveAllListeners(); }
}
