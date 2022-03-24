using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanelControll : MonoBehaviour
{
    int Coin;
    [SerializeField] CUIText text;
  
    public void SetCoin(int _Coin)
    {
        Coin = _Coin;
        text.SetNumber(Coin);
    }
}
