using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUITextShowCurMax : CUIText
{
    [SerializeField] protected int m_MaxNumber  = 200;
    [SerializeField] protected Color m_StartTextColor = Color.white;

    protected string m_CurHexColorStr = "FFFFFFFF";

    public int MaxNumber
    {
        set => m_MaxNumber = value;
        get => m_MaxNumber;
    }

    protected override void Awake()
    {
        m_CurHexColorStr = ColorUtility.ToHtmlStringRGBA(m_StartTextColor);

        base.Awake();
        Activate();
    }

    public void Activate()
    {
        m_CurNumber = 0;
        SetCurNumber(0);
    }

    public void SetCurNumber(int number)
    {
        m_Text.text = $"{number}/{m_MaxNumber}";
        m_Text.text = $"<color=#{m_CurHexColorStr}>{number}</color>/{m_MaxNumber}";
        m_CurNumber = number;
    }

    public void SetCurNumberColor(string colorstr)
    {
        m_Text.text = $"<color=#{colorstr}>{m_CurNumber}</color>/{m_MaxNumber}";
        m_CurHexColorStr = colorstr;
    }

    public override void AddCurNumber(int number)
    {
        m_CurNumber += number;
        m_Text.text = $"{m_CurNumber}/{m_MaxNumber}";
    }
}
