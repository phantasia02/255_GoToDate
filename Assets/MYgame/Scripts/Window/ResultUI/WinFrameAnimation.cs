using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WinFrameAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

   public void StartAni(float Delay)
    {
        CancelInvoke();
        Invoke("StartAni", Delay);
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    void StartAni()
    {
               rectTransform.DOScale(0.98f, 0.3f).SetLoops(2, LoopType.Yoyo);

    }

}
