using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChatroomLoveGroup : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject[] m_AllLoveImage = null;

    protected int m_LoveCount = 0;
    public int LoveCount => m_LoveCount;

    // ==================== SerializeField ===========================================

    private void Awake()
    {
        
    }

    public void AddLove(int addcount)
    {
        int lTempCount = m_LoveCount + addcount;

        if (lTempCount >= m_AllLoveImage.Length)
            lTempCount = m_AllLoveImage.Length - 1;
        else if (lTempCount < 0)
            lTempCount = 0;

        m_LoveCount = lTempCount;
        UpdateLoveImageShow();
    }

    //public void RemoveLove(int removecount)
    //{
    //    int lTempCount = m_LoveCount - removecount;

    //    if (lTempCount < 0)
    //        lTempCount = 0;

    //    m_LoveCount = lTempCount;
    //    UpdateLoveImageShow();
    //}

    public void UpdateLoveImageShow()
    {
        int i = 0;
        for (i = 0; i < m_LoveCount; i++)
            m_AllLoveImage[i].SetActive(true);

        for (; i < m_AllLoveImage.Length; i++)
            m_AllLoveImage[i].SetActive(false);
    }
}
