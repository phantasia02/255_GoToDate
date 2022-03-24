using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class CountdownControll : MonoBehaviour
{
    [SerializeField] CUIText text;
    public bool IsDone;

    public UnityEvent onEnd = new UnityEvent();

    public void Addlistener(UnityAction _listener)
    {
        onEnd.AddListener(_listener);
    }

    public void StartCountdown()
    {
        
       

        IsDone = false;
        Invoke("Three", 0);
        
        Invoke("Two", 1);
        Invoke("One", 2);
        Invoke("Go", 3);
        
    }

    void Three()
    {
        text.SetNumber(3);
        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        lTempAudioManager.PlaySE(CSEPlayObj.ESE.eStartCountdown);
    }
    void Two()
    {
        text.SetNumber(2);
        //CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        //lTempAudioManager.PlaySE(CSEPlayObj.ESE.eStartCountdown);
    }
    void One()
    {
        text.SetNumber(1);
       // CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
       // lTempAudioManager.PlaySE(CSEPlayObj.ESE.eStartCountdown);
    }
    void Go()
    {
        CAudioManager lTempAudioManager = CAudioManager.SharedInstance;
        lTempAudioManager.PlaySE(CSEPlayObj.ESE.eStartCountdownGo);
        text.SetText("GO!");
        IsDone = true;
        OnEnd();
    }

    public void OnEnd()
    {
        if (onEnd != null)
        {
            onEnd.Invoke();
        }
    }

}
