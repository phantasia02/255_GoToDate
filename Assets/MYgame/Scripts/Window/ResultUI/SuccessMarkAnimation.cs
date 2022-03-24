using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SuccessMarkAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup canvasGroup;
    public void StartAni(float Delay)
    {
        CancelInvoke();
        Invoke("StartAni", Delay);
        rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    void StartAni()
    {
        rectTransform.DOScale(1, 1).SetEase(Ease.OutBack);

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 1);
    }

}
