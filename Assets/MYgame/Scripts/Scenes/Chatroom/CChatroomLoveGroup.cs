using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CChatroomLoveGroup : MonoBehaviour
{

    public enum ELoveSpriteType
    {
        eNull       = 0,
        eNarmal     = 1,
        eSpecial    = 2,
        eChargeSkillMainFx = 3,

        eMax,
    };

    // ==================== SerializeField ===========================================

    [SerializeField] protected Image[]      m_AllLoveImage      = null;
    [SerializeField] protected Sprite[]     m_LoveImageSprite   = null;

    // ==================== SerializeField ===========================================
    
    protected int m_LoveCount = 0;
    public int LoveCount => m_LoveCount;

    private void Awake()
    {
        
    }

    public void AddLove(int addcount)
    {
        int lTempCount = m_LoveCount + addcount;

        if (lTempCount >= m_AllLoveImage.Length)
            lTempCount = m_AllLoveImage.Length;
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
        {
            if (i >= 3)
                m_AllLoveImage[i].sprite = m_LoveImageSprite[(int)ELoveSpriteType.eSpecial];
            else
                m_AllLoveImage[i].sprite = m_LoveImageSprite[(int)ELoveSpriteType.eNarmal];
        }
           

        for (; i < m_AllLoveImage.Length; i++)
            m_AllLoveImage[i].sprite = m_LoveImageSprite[(int)ELoveSpriteType.eNull];
    }
}
