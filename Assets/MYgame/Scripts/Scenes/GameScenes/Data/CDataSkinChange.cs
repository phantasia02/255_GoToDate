using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{

    [CreateAssetMenu(
        menuName = "Data/Skin Change Data",
        fileName = "Skin Change")]
    public class CDataSkinChange : ScriptableObject
    {
        [SerializeField]
        protected string m_ShoesObj = null;
        [SerializeField]
        protected string m_PantsObj = null;
        [SerializeField]
        protected string m_ClothesObj = null;
        [SerializeField]
        protected bool m_bShowFoot = false;
        [SerializeField]
        protected int m_DataID = -1;

        public string ShoesObj => m_ShoesObj;
        public string PantsObj => m_PantsObj;
        public string ClothesObj => m_ClothesObj;
        public bool bShowFoot => m_bShowFoot;
        public int DataID => m_DataID;
    }
}
