using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CChatroomCentrMessage : MonoBehaviour
{



    // ==================== SerializeField ===========================================

    [SerializeField] protected GameObject[] m_OriginalMessage = null;

    // ==================== SerializeField ===========================================

    protected List<CUITextImage> m_AllShowMessage = new List<CUITextImage>();

    private void Awake()
    {
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

        m_AllShowMessage.Add(lTempUITextImage);
    }
}
