using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class TextFlashing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] float animSpeedInSec = 1f;
    [SerializeField] Color FlashColor;
    [SerializeField] Color OrlColor;

    private void OnEnable()
    {
        startTextMeshAnimation();
    }

    private void OnDisable()
    {
        stopTextMeshAnimation();
    }

    void startTextMeshAnimation()
    {
        textMesh.DOColor(FlashColor, animSpeedInSec).SetLoops(-1, LoopType.Yoyo);
    }

    void stopTextMeshAnimation()
    {
        textMesh.DOColor(OrlColor, animSpeedInSec);
    }
}
