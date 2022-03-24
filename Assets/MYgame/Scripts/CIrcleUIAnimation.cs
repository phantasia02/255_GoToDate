using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CIrcleUIAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image image;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        rectTransform.DOScale(new Vector3(0, 0, 0), Speed).SetLoops(-1, LoopType.Restart);
        image.DOFade(0, Speed).SetLoops(-1, LoopType.Restart);

    }
}
