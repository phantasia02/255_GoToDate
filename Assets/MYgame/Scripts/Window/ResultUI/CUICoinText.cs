using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextFlashing))]
[RequireComponent(typeof(GlowingText))]
[RequireComponent(typeof(FailText))]
public class CUICoinText : CUIText
{
    TextFlashing Flashing;
    GlowingText glowing;
    FailText Fail;

    private void OnEnable()
    {
        Flashing = GetComponent<TextFlashing>();
        glowing = GetComponent<GlowingText>();
        Fail = GetComponent<FailText>();
    }

    private void OnDisable()
    {
        Flashing.enabled = false;
        glowing.enabled = false;
        Fail.enabled = false;
    }

    public void UseSpecialEffect(int Index)
    {
        Flashing.enabled = Index == 1;
        glowing.enabled = Index == 2;
        Fail.enabled = Index == 3;
    }
}
