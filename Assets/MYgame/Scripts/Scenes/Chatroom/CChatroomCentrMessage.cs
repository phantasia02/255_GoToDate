using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CChatroomCentrMessage : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject[] m_OriginalMessage = null;

    // ==================== SerializeField ===========================================

    protected RectTransform m_MyRectTransform = null;
    protected VerticalLayoutGroup m_MyVerticalLayoutGroup = null;
    protected List<CUITextImage> m_AllShowMessage = new List<CUITextImage>();

    public List<CUITextImage> AllShowMessage => m_AllShowMessage;

    private void Awake()
    {
        m_MyVerticalLayoutGroup = this.GetComponent<VerticalLayoutGroup>();
        m_MyRectTransform = this.GetComponent<RectTransform>();
        m_AllShowMessage.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void AddMessage(EMessageType TypeMessage, Sprite MugShot, string Message)
    {
        GameObject lTempGameObject = GameObject.Instantiate(m_OriginalMessage[(int)TypeMessage], this.transform);

        CUITextImage lTempUITextImage = lTempGameObject.GetComponent<CUITextImage>();
        lTempUITextImage.SetSprite(MugShot);
        lTempUITextImage.SetText(Message);

        m_AllShowMessage.Add(lTempUITextImage);


        const float CHight = 1200.0f;
        const float CMessageObjHight = 285.0f;
        const float CMinusHeight = CHight - CMessageObjHight;

        float lTempTotalHight = (m_MyRectTransform.sizeDelta.y + CMessageObjHight + Mathf.Abs(m_MyVerticalLayoutGroup.padding.top));
        if (lTempTotalHight > CHight)
            m_MyVerticalLayoutGroup.padding.top = (int)((-lTempTotalHight) + CMinusHeight + CMessageObjHight);

       // Debug.Log($"m_MyVerticalLayoutGroup.padding.top = {m_MyVerticalLayoutGroup.padding.top}");
        //Debug.Log($"m_MyRectTransform.sizeDelta.y = {m_MyRectTransform.sizeDelta.y}");
        
        //StartCoroutine(UpdateVerticalLayoutGroupFram());
    }

    //public IEnumerator UpdateVerticalLayoutGroupFram()
    //{
    //    yield return new WaitForEndOfFrame();
    //}

}
