using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenesChangChar : CScenesCtrlBase
{
    [SerializeField] protected Transform m_TargetManObj = null;

    protected GameObject m_ManObj = null;
    protected CActorSetSkin m_PlayerSkin = null;

    protected override void Awake()
    {
        base.Awake();

        ChangeSkin();
    }

    public virtual void ChangeSkin()
    {
        GameObject lTempPlayerObject = GameObject.FindWithTag(StaticGlobalDel.TagPlayer);
        m_PlayerSkin = lTempPlayerObject.GetComponent<CActorSetSkin>();

        m_PlayerSkin.SetUpdateSkinMat(StaticGlobalDel.BuffMyRoleData.DataSkinMat);

        if (StaticGlobalDel.SelectSkin != null)
            m_PlayerSkin.SetUpdateSkinObj(StaticGlobalDel.SelectSkin);

        m_ManObj = GameObject.Instantiate(StaticGlobalDel.TargetDataObj.Model, m_TargetManObj);
        m_ManObj.transform.localPosition = Vector3.zero;
        m_ManObj.transform.localScale = Vector3.one * 1.55f;
    }
}
