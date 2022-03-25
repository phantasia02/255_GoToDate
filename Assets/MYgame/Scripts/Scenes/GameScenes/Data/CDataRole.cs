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
        [SerializeField] protected Sprite _FullBigPicture   = null;
        [SerializeField] protected Sprite _MugShot          = null;
        
        public Sprite FullBigPicture => _FullBigPicture;
        public Sprite MugShot => _MugShot;
    }
}
