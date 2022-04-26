using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    [CreateAssetMenu(
    menuName = "Data/Object Char Data",
    fileName = "Object Char Data")]
    public class CDataObjChar : ScriptableObject
    {
        [SerializeField] protected CMessageList _StartMessageList = null;
        [SerializeField] protected float _Scale = 1.55f;
        [SerializeField] protected Sprite _MugShot = null;
        [SerializeField] protected Sprite _ChatMugShot = null;
        [SerializeField] protected string _PersonalityDescription = null;
        [SerializeField] protected GameObject _Model = null;
        [SerializeField] protected CDataSkinChange _LoveSkin = null;
        [SerializeField] protected List<CDataSkinChange> _AllSelectSkin = null;
        [SerializeField] protected string _DatingMeetScreenStr = null;
        [SerializeField] protected string _EndSmallGameScreenStr = null;
        [SerializeField] protected string _DatingDressScreenStr = null;
  

        public CMessageList StartMessageList => _StartMessageList;
        public float Scale => _Scale;
        public Sprite MugShot => _MugShot;
        public Sprite ChatMugShot => _ChatMugShot;
        public string PD => _PersonalityDescription;
        public GameObject Model => _Model;
        public CDataSkinChange LoveSkin => _LoveSkin;
        public List<CDataSkinChange> AllSelectSkin => _AllSelectSkin;
        public string DatingMeetScreenStr => _DatingMeetScreenStr;
        public string EndSmallGameScreenStr => _EndSmallGameScreenStr;
        public string DatingDressScreenStr => _DatingDressScreenStr;

    }
}
