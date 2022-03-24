using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBtnCtrlNumber : MonoBehaviour
{
    [SerializeField] Text m_UINumberText = null;
    [SerializeField] Button m_Add = null;
    public Button Add => m_Add;
    [SerializeField] Button m_Minus = null;
    public Button Minus => m_Minus;
    [SerializeField] Button m_CilickBtn = null;
    public Button CilickBtn => m_CilickBtn;

    int m_Number = 0;
    public int Number{get { return m_Number; }}

    [SerializeField] int m_NumberMax = 100;
    public int NumberMax
    {
        set { m_NumberMax = value; }
        get { return m_NumberMax; }
    }
    [SerializeField] int m_NumberMin = 1;
    public int NumberMin
    {
        set { m_NumberMin = value; }
        get { return m_NumberMin; }
    }

    protected virtual void Awake()
    {
        m_Number = m_NumberMin;

        Add.onClick.AddListener(OnAdd);
        Minus.onClick.AddListener(OnSub);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdataText()
    {
        m_UINumberText.text = m_Number.ToString();
    }

    public virtual void SetNumber(int lpNumber)
    {
        if (lpNumber < NumberMin || lpNumber > NumberMax)
            return;

        m_Number = lpNumber;
        UpdataText();
    }

    public void OnAdd()
    {
        int lTempAddNumber = m_Number + 1;
        if (lTempAddNumber > m_NumberMax)
            m_Number = m_NumberMin;

        SetNumber(lTempAddNumber);
    }

    public void OnSub()
    {
        int lTempSubNumber = m_Number - 1;
        if (lTempSubNumber < m_NumberMin)
            m_Number = m_NumberMax;

        SetNumber(lTempSubNumber);
    }
}
