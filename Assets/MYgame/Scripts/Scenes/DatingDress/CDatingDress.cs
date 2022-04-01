using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDatingDress : CScenesCtrlBase
{
    [SerializeField] protected CUIButton[] m_AllChangeDatingDress = null;
    [SerializeField] protected CUIButton m_ApplyBtn            = null;


    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_AllChangeDatingDress.Length; i++)
            InitButtonCall(i);


        m_ApplyBtn.AddListener(() => { Debug.Log("OKOK"); });
    }

    public void InitButtonCall(int index)
    {
        if (0 > index || index >= m_AllChangeDatingDress.Length)
            return;

        RectTransform lTempRectTransform = m_AllChangeDatingDress[index].MyRectTransform;
        m_AllChangeDatingDress[index].AddListener(() =>
        {
            m_ApplyBtn.gameObject.SetActive(true);
            Vector2 lTempVector2 = lTempRectTransform.anchoredPosition;
            lTempVector2.y -= lTempRectTransform.sizeDelta.y / 2.0f;
            m_ApplyBtn.MyRectTransform.anchoredPosition = lTempVector2;
           // Debug.Log($"InitButtonCall = {index}");
        });
    }
}
