using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    [CreateAssetMenu(
    menuName = "Data/Object Char Data",
    fileName = "Object Char Data")]
    public class CDataObjChar : ScriptableObject
    {

        [SerializeField] protected Sprite _MugShot = null;
        [SerializeField] protected Sprite _ChatMugShot = null;
        [SerializeField] protected string _PersonalityDescription = null;
        [SerializeField] protected GameObject _Model = null;
        [SerializeField] protected CDataSkinChange _LoveSkin = null;
        [SerializeField] protected List<CDataSkinChange> _AllSelectSkin = null;

        public Sprite MugShot => _MugShot;
        public Sprite ChatMugShot => _ChatMugShot;
        public string PD => _PersonalityDescription;
        public GameObject Model => _Model;
        public CDataSkinChange LoveSkin => _LoveSkin;
        public List<CDataSkinChange> AllSelectSkin => _AllSelectSkin;

    }
}
