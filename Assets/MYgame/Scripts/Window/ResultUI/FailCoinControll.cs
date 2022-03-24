using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCoinControll : CoinControll
{

    protected override void UpdateView()
    {
        TargetCoinText.SetNumber(Target);
        CurrentCoinText.SetNumber(Current);
    }
    
}
