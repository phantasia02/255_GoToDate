using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SpeedUIAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image image;
    [SerializeField] float Speed;
    [SerializeField] float Forward;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        Vector2 ForwardPos = rectTransform.anchoredPosition + new Vector2(0, Forward);
        rectTransform.DOAnchorPos(ForwardPos, Speed).SetLoops(-1, LoopType.Restart).SetId(rectTransform);
        image.DOFade(0, Speed).SetLoops(-1, LoopType.Restart).SetId(image);
    }

    private void OnDestroy()
    {
        DOTween.Kill(image);
        DOTween.Kill(rectTransform);
    }
}
