using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NextButtonAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    public void StartAni(float Delay)
    {
        CancelInvoke();
        Invoke("StartAni", Delay);
        canvasGroup.alpha = 0;
    }

    void StartAni()
    {
               canvasGroup.DOFade(1, 1);

    }

}
