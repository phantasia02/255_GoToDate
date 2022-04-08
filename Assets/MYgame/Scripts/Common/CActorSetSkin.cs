using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActorSetSkin : MonoBehaviour
{
    public enum ESlinType
    {
        eHair       = 0,
        eBody       = 1,
        eFoot       = 2,
        eHead       = 3,
        eEyes       = 4,
        eMax
    }

    [System.Serializable]
    public class DataMatSet
    {
        [VarRename(ESlinType.eMax)]
        public Material[] m_AllSkinMat = new Material[(int)ESlinType.eMax];
    }

    [VarRename(ESlinType.eMax)]
    [SerializeField] protected Renderer[] m_AllSkin = null;

    [SerializeField] protected DataMatSet m_AllMat = null;

    private void Awake()
    {
    }

    public void SetUpdateSkinMat(DataMatSet skinmatdata)
    {
        for (int i = 0; i < m_AllSkin.Length; i++)
            m_AllSkin[i].material = skinmatdata.m_AllSkinMat[i];
    }
}
