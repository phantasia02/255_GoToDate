using UnityEngine;
using LanKuDot.UnityToolBox;
using System.Collections.Generic;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    


    [System.Serializable]
    public class DataColor
    {
        public StaticGlobalDel.EBrickColor _Color = StaticGlobalDel.EBrickColor.eBlue;
        public float _Ratio = 0.0f;
    }

    [System.Serializable]
    public class DataLevelAllColor
    {
       
        public DataColor[]                      _brickColors;
        public int                              ID = 1;
        [HideInInspector]
        public float                            TotleColorRatio = 0.0f;

    }

    [CreateAssetMenu(
        menuName = "Data/Stage Data",
        fileName = "StageData")]
    public class StageData : ScriptableObject
    {
        [SerializeField]
        private BuildingRecipeData[] _buildings;
        [SerializeField]
        private TweenHDRColorEaseCurve _creatarchitecture;
        [SerializeField]
        private int _PlayerTrailerCount = 1;
        [SerializeField]
        private int _TargetBuilding = 100;
        [SerializeField]
        private DataLevelAllColor[] _brickRandomLevelAllColor;

    
        public BuildingRecipeData[] buildings => _buildings;
        public TweenHDRColorEaseCurve creatarchitecture => _creatarchitecture;
        public int PlayerTrailerCount => _PlayerTrailerCount;
        public int TargetBuilding => _TargetBuilding;
        public DataLevelAllColor[] BrickRandomLevelAllColor => _brickRandomLevelAllColor;
    }
}
