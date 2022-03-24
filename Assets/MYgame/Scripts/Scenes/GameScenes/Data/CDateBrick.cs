using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Date Brick",
    menuName = "Data/Date Brick")]
public class CDateBrick : ScriptableObject
{
    public Material                     m_ColorMat  = null;
    public Color                        m_Color     = new Color();
    public StaticGlobalDel.EBrickColor  m_Type      = StaticGlobalDel.EBrickColor.eRed;
}