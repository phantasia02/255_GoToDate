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
        [System.Serializable]
        public class DataSelectRole
        {
            public Sprite _FullBigPicture   = null;
            public Sprite _MugShot          = null;
        }

        [SerializeField] protected DataSelectRole[] m_AllDataSelectRole;
        public DataSelectRole[] AllDataSelectRole => m_AllDataSelectRole;
    }
}
