using UnityEngine;
using LanKuDot.UnityToolBox;
using System.Collections.Generic;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{



    [CreateAssetMenu(
        menuName = "Data/Stage Data",
        fileName = "StageData")]
    public class StageData : ScriptableObject
    {

        [SerializeField]
        protected List<CDataObjChar> _AllDataObjChar = null;
        [SerializeField]
        protected string _PersonalityDescription = null;

        public List<CDataObjChar> AllDataObjChar => _AllDataObjChar;
        public string PD => _PersonalityDescription;
    }
}
