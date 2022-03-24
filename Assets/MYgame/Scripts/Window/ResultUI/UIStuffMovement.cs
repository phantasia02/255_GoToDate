using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIStuffMovement : MonoBehaviour
{
    [SerializeField] RectTransform MoveStuff;
    [SerializeField] RectTransform Loctaion;
    [SerializeField] float duration;

    public void Move()
    {
        MoveStuff.DOAnchorPos(Loctaion.anchoredPosition, duration);

    }

}
