using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;

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

    public enum EClothesType
    {
        ePajama     = 0,
        eCoat       = 1,
        esuit       = 2,
        eTankTop    = 3,
        eShirt      = 4,
        eDress      = 5,
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

    [SerializeField] protected GameObject m_FootObj = null;

    [SerializeField] protected GameObject[] m_AllShoesObj = null;
    [SerializeField] protected GameObject[] m_AllPantsObj = null;
    [SerializeField] protected GameObject[] m_AllClothesObj = null;

    private void Awake()
    {
        //m_AllDataSkinChange[m_SetIndex]

    }

    public void SetUpdateSkinMat(DataMatSet skinmatdata)
    {
        for (int i = 0; i < m_AllSkin.Length; i++)
            m_AllSkin[i].material = skinmatdata.m_AllSkinMat[i];
    }

    public void SetUpdateSkinObj(CDataSkinChange data)
    {
        foreach (var item in m_AllShoesObj)
            item.SetActive(data.ShoesObj == item.name);

        foreach (var item in m_AllPantsObj)
            item.SetActive(data.PantsObj == item.name);

        foreach (var item in m_AllClothesObj)
            item.SetActive(data.ClothesObj == item.name);

        m_FootObj.SetActive(data.bShowFoot);
    }
}
