using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] CoinControll coinControll;
    [SerializeField] ResultControll ResultControll;
    [SerializeField] ResultPanelControll SuccessControll;
    [SerializeField] ResultPanelControll failControll;
    [SerializeField] CUIButton WinButton = null;
    [SerializeField] CUIButton LoseButton = null;
    [SerializeField] int CurCoinBuff = 0;
    [SerializeField] int TarCoinBuff = 0;

    public GameObject LoseGameObj => ResultControll.LoseGameObj;

    private void Start()
    {
        // SetCutTagetCoin(200, 300);
        // SetCutTagetCoin(700, 300);
        // Invoke("OnLose", 1.5f);

        // Invoke("OnGetCoin", 1f);
        // Invoke("OnLose", 2f);
        // Invoke("OnWin", 2f);
    }

    public void OnWin()
    {
        print("OnWin");
        ResultControll.SetWin();
        SuccessControll.SetCoin(CurCoinBuff);
    }

    public void OnLose()
    {
        print("OnLose");
        ResultControll.SetLose();
        failControll.SetCoin(CurCoinBuff);
        coinControll.SetFailure();
    }

    public void SetCutTagetCoin(int CurCoin, int TargetCoin)
    {
        CurCoinBuff = CurCoin;
        TarCoinBuff = TargetCoin;
       // coinControll.SetTarget(TargetCoin);
      //  coinControll.SetCurrent(CurCoin);
    }

    public void OnDelayWin(float DelayTime = 0.0f) { Invoke("OnWin", DelayTime); }
    public void OnDelayLose(float DelayTime = 0.0f) { Invoke("OnLose", DelayTime); }

    public void AddWinCallBackFunc(UnityAction call) { WinButton.AddListener(call); }
    public void AddLoseCallBackFunc(UnityAction call) { LoseButton.AddListener(call); }
}
