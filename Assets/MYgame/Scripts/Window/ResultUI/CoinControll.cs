using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinControll : MonoBehaviour
{
    [SerializeField] protected int Current;
    [SerializeField] protected int Target;
    [SerializeField] protected CUICoinText CurrentCoinText;
    [SerializeField] protected CUICoinText TargetCoinText;

    public void SetTarget(int _Target)
    {
        Target = _Target;
        UpdateView();
    }

    public void SetCurrent(int _Current)
    {
        Current = _Current;
        UpdateView();
    }
    protected virtual void UpdateView()
    {
        TargetCoinText.SetNumber(Target);
        CurrentCoinText.SetNumber(Current);
        CurrentCoinText.UseSpecialEffect(IsSatisfy() ? 1 : 0);
    }
    bool IsSatisfy()
    {
        return (Current >= Target);
    }

    public void SetFailure()
    {
        CurrentCoinText.UseSpecialEffect(3);
        TargetCoinText.UseSpecialEffect(2);
    }
}
