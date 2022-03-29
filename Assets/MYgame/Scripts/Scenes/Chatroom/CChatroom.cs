using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

public class CChatroom : CScenesCtrlBase
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected CChatroomCentrMessage m_MyChatroomCentrMessage = null;

    // ==================== SerializeField ===========================================

    private void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);

        m_MyChatroomCentrMessage = this.GetComponentInChildren<CChatroomCentrMessage>();
    }

    // Start is called before the first frame update
    void Start()
    {

        CDataObjChar lTempDataObjChar = StaticGlobalDel.TargetDataObj;

       // m_MyChatroomCentrMessage.AddMessage( CChatroomCentrMessage.EMessageType.eMyMessage,  );
    }


}
