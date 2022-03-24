using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Date PlayerRelevant",
    menuName = "Data/Date PlayerRelevant")]
public class CDataPlayerRelevant : Car
{
    public Material                             m_PlayerMat             = null;
    public Material                             m_TrailerMat            = null;
    public Material                             m_FxMat                 = null;
    public TweenColorEaseCurve                  m_FxChargeCurve         = null;
    public int                                  m_OpenLevel             = 0;
}