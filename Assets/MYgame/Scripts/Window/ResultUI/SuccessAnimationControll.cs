using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessAnimationControll : MonoBehaviour
{
    [SerializeField] WinFrameAnimation WinFrameAnimation;
    [SerializeField] SuccessMarkAnimation MarkAnimation;
    [SerializeField] NextButtonAnimation NextButtonAnimation;

    void OnEnable()
    {
        MarkAnimation.StartAni(0);
        WinFrameAnimation.StartAni(0.6f);
        NextButtonAnimation.StartAni(1.5f);

    }


}
