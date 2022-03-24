using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CUIButton : CUIElementBase, IPointerDownHandler
{
    public override EUIElementType UIElementType() { return EUIElementType.eUIButton; }

    // ==================== SerializeField ===========================================

    [SerializeField] protected Button m_Button = null;
    [SerializeField] protected Image m_Image = null;
    public Button Button => m_Button;

    // ==================== SerializeField ===========================================


    private void Awake()
    {
        if (m_Button == null)
            m_Button = this.GetComponentInChildren<Button>(true);

        if (m_Image == null)
            m_Image = m_Button.GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!m_Button.interactable)
            return;

        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        lTempAudioManager.PlaySE(CSEPlayObj.ESE.eButton);
    }

    public void SetSpriteState(SpriteState parSpriteState)
    {
        m_Button.spriteState = parSpriteState;

        if (m_Image == null)
            m_Image = m_Button.GetComponent<Image>();

        m_Image.sprite = m_Button.spriteState.highlightedSprite;
    }

    public void AddListener(UnityAction call) { m_Button.onClick.AddListener(call); }
    public void RemoveListener(UnityAction call) { m_Button.onClick.RemoveListener(call); }
    public void RemoveAllListener() { m_Button.onClick.RemoveAllListeners(); }
}
