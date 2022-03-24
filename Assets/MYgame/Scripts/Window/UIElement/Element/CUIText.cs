using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CUIText : CUIElementBase
{
    public override EUIElementType UIElementType() { return EUIElementType.eUIText; }

    // ==================== SerializeField ===========================================

    [SerializeField] protected TextMeshProUGUI m_Text = null;
    protected RectTransform m_TextRectTransform = null;
    public TextMeshProUGUI Text => m_Text;
    public RectTransform TextRectTransform
    {
        get
        {
            if (m_TextRectTransform == null)
                m_TextRectTransform = m_Text.GetComponent<RectTransform>();

            return m_TextRectTransform;
        }
    }
        

    // ==================== SerializeField ===========================================
    protected int m_CurNumber = 0;
    public int CurNumber => m_CurNumber;

    protected virtual void Awake()
    {
        if (m_Text == null)
            m_Text = this.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void SetNumber(int number)
    {
        m_Text.text = number.ToString();
    }

    public void SetTextColor(Color pacolor)
    {
        m_Text.color = pacolor;
    }


    public void SetTextColor(string pacolor)
    {
        if (ColorUtility.TryParseHtmlString(pacolor, out Color lTempsetcolor))
            m_Text.color = lTempsetcolor;
    }

    public void SetText(string Text) {m_Text.text = Text; }
    public virtual void AddCurNumber(int number)
    {
        m_CurNumber += number;
        m_Text.text = $"{m_CurNumber}";
    }

    public void OutlineColor(Color pacolor)
    {
        m_Text.fontSharedMaterial.SetColor("_OutlineColor", pacolor);
        m_Text.ForceMeshUpdate();
    }
}
