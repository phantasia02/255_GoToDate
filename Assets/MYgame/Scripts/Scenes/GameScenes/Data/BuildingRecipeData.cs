using System;
using UnityEngine;
using UniRx;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    [CreateAssetMenu(
        fileName = "BuildingRecipe",
        menuName = "Data/Building Recipe")]
    public class BuildingRecipeData : ScriptableObject
    {
        [SerializeField]
        private Sprite _buildingSprite;
        [SerializeField]
        private BrickAmount[] _brickAmounts;
        [SerializeField]
        private GameObject _Prefab3DMode;
        [SerializeField]
        private int _Score = 100;

        public Sprite buildingSprite => _buildingSprite;
        public BrickAmount[] brickAmounts => _brickAmounts;
        public GameObject Prefab3DMode => _Prefab3DMode;
        public int Score => _Score;
    }

    [Serializable]
    public class BrickAmount
    {
        [SerializeField]
        private StaticGlobalDel.EBrickColor _color;
        [SerializeField]
        private int _amount;

        //public BrickAmount()
        //{
        //    _AmountCallBack.Value = _amount;
        //    AmountCallBackCallBack.Subscribe(V => { _amount = V; });
        //}

        public StaticGlobalDel.EBrickColor color
        {
            set => _color = value;
            get => _color;
        }

        public int amount
        {
            set
            {
                _amount = value;
                //_AmountCallBack.Value = _amount;
            }
            get => _amount;
        }

    }
}
