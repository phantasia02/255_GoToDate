using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultControll : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;

    public GameObject LoseGameObj => LosePanel;

    private void OnEnable()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    public void SetWin()
    {
        WinPanel.SetActive(true);
        LosePanel.SetActive(false);
    }
    public void SetLose()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(true);
    }
}
