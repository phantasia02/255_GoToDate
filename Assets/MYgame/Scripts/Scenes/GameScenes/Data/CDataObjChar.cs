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
      //  [SerializeField] protected string _PersonalityDescription = null;

        public Sprite MugShot => _MugShot;
        public Sprite ChatMugShot => _ChatMugShot;
     //   public string PD => _PersonalityDescription;

    }
}
