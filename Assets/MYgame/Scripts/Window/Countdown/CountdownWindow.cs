using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CountdownWindow : MonoBehaviour
{
    [SerializeField] CountdownControll CountdownControll;
    [SerializeField] GameObject Tutorial;
    

    // Start is called before the first frame update
    void Start()
    {
        //CountdownControll.StartCountdown();
        CountdownControll.Addlistener(OnEnd);
    }

    public void StartTimeOut()
    {
        CountdownControll.StartCountdown();
    }

    public void OnEnd()
    {
        print("OnEnd");
        CountdownOK.OnNext(Unit.Default);
    }

    public void ShowCountdownControll(bool show){CountdownControll.gameObject.SetActive(show);}
    public void ShowTutorial(bool show) { Tutorial.SetActive(show); }
    // ===================== UniRx ======================

    public Subject<Unit> CountdownOK = new Subject<Unit>();
    public Subject<Unit> ObserveCountdownOK() { return CountdownOK ?? (CountdownOK = new Subject<Unit>()); }

    // ===================== UniRx ======================
}
