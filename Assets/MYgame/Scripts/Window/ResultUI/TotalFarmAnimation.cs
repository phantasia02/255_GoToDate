using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TotalFarmAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup group;

    void Start()
    {
    }

    public void StartAnimation()
    {
        group.DOKill();
        group.alpha = 0;
        group.DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
