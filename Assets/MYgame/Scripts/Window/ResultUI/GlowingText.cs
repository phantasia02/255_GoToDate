using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GlowingText : MonoBehaviour
{
    [SerializeField] Color GlowColor;
    [SerializeField] float Power;
    [SerializeField] TextMeshProUGUI Text;

    private void OnEnable()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.fontMaterial.SetColor("_GlowColor", GlowColor);
        Text.fontMaterial.SetFloat("_GlowPower", Power);
        DOTween.To(() => Power, x => Power = x, 0, 1).SetLoops(-1, LoopType.Yoyo).OnUpdate(Callback);
    }

    private void OnDisable()
    {
        Text.fontMaterial.SetFloat("_GlowPower", 0);
    }

    private void Callback()
    {
        Text.fontMaterial.SetFloat("_GlowPower", Power);
    }

}
