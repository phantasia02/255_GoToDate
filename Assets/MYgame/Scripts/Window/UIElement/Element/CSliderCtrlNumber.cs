using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CSliderCtrlNumber : CBtnCtrlNumber
{
    [SerializeField] Slider m_SliderBtn = null;
    public Slider SliderBtn => m_SliderBtn;

    public UnityAction<float> ReturvalCallBack = null;

    protected override void Awake()
    {
        base.Awake();

        SliderBtn.maxValue = NumberMax;
        SliderBtn.minValue = NumberMin;
        SliderBtn.value = NumberMin;


        SliderBtn.onValueChanged.AddListener(SliderChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SliderChanged(float val)
    {
        SetNumber(Mathf.CeilToInt(val));
    }

    public override void SetNumber(int lpNumber)
    {
        if (lpNumber < NumberMin || lpNumber > NumberMax)
            return;

        base.SetNumber(lpNumber);
        SliderBtn.value = Number;

        if (ReturvalCallBack != null)
            ReturvalCallBack(SliderBtn.value);
    }
}
