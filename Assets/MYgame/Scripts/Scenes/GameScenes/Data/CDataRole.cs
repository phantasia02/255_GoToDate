using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYgame.Scripts.Scenes.GameScenes.Data
{
    [CreateAssetMenu(
        menuName = "Data/Role Data",
        fileName = "Role Data")]
    public class CDataRole : ScriptableObject
    {
        [SerializeField] protected Sprite _MugShot          = null;
        [SerializeField] protected Sprite _ChatMugShot      = null;
        [SerializeField] protected CActorSetSkin.DataMatSet _DataSkinMat = null;
        
        public Sprite MugShot => _MugShot;
        public Sprite ChatMugShot => _ChatMugShot;
        public CActorSetSkin.DataMatSet DataSkinMat => _DataSkinMat;
    }
}
