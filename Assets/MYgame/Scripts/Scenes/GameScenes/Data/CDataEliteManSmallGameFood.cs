using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    [CreateAssetMenu(
    menuName = "Data/Elite Man Small Game Food Data",
    fileName = "Elite Man Small Game Food Data")]
    public class CDataEliteManSmallGameFood : ScriptableObject
    {
        [SerializeField] protected Sprite _MugShot = null;
        [SerializeField] protected GameObject _FoodModel = null;
        [SerializeField] protected int _Score = 33;

        public Sprite MugShot => _MugShot;
        public GameObject FoodModel => _FoodModel;
        public int Score => _Score;
    }
}
