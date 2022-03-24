using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;
using MYgame.Scripts.Scenes.Building;
using MYgame.Scripts.Window;
using UniRx;
using DG.Tweening;

public delegate void CompleteBuildingValCall(BrickAmount Paramete);

public class CUICompleteBuildingInfo : MonoBehaviour
{
    // ==================== SerializeField ===========================================

    [SerializeField] protected Image m_CompleteBuildingImage = null;
    [SerializeField] protected Image m_NeedBG = null;
    [SerializeField] protected CV1BrickStatusUI[] m_GroupBrickStatus = null;


    protected CanvasGroup m_MyCanvasGroup = null;

    public Image CompleteBuildingImage => m_CompleteBuildingImage;

    private readonly
    Dictionary<StaticGlobalDel.EBrickColor, CV1BrickStatusUI> _brickColorUIMap =
        new Dictionary<StaticGlobalDel.EBrickColor, CV1BrickStatusUI>();

    // ==================== SerializeField ===========================================
    

    private void Awake()
    {
        foreach (var ui in m_GroupBrickStatus)
        {
            ui.Inactivate();
            _brickColorUIMap[ui.color] = ui;
        }

        m_MyCanvasGroup = this.GetComponent<CanvasGroup>();

        Inactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Inactivate()
    {
        gameObject.SetActive(false);
    }

    public void SetMaxBrickAmount(BrickAmount[] maxBrickAmounts)
    {
        foreach (var ui in m_GroupBrickStatus)
            ui.Inactivate();

        foreach (var brickAmount in maxBrickAmounts)
        {
            var color = brickAmount.color;
            var ui = _brickColorUIMap[color];
            ui.SetMaxNumber(brickAmount.amount);
            ui.SetNumber(0);
            ui.Activate();
        }
    }

    public void SetCallBackSetNumber(BrickStatusUI UIObj, int Number){ UIObj.SetNumber(Number);}

    public void SetNumber(StaticGlobalDel.EBrickColor color, int number)
    {
        var ui = _brickColorUIMap[color];
        if (ui.gameObject.activeSelf)
            ui.SetNumber(number);
    }

    public void ChangCompleteBuildingImage(BuildingProgress ParCompleteBuilding, bool updatenumber)
    {
        if (ParCompleteBuilding == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        this.gameObject.SetActive(true);
        m_CompleteBuildingImage.sprite = ParCompleteBuilding.buildings.buildingSprite;
        m_CompleteBuildingImage.SetNativeSize();
        SetMaxBrickAmount(ParCompleteBuilding.buildings.brickAmounts);

        if (updatenumber)
        {
            for (int i = 0; i < ParCompleteBuilding.NeedBricks.Length; i++)
                SetNumber(ParCompleteBuilding.NeedBricks[i].color, ParCompleteBuilding.NeedBricks[i].amount);
        }
       // this.enabled
        Activate();
    }


    public void ShowCompleteBuildingImage(bool show, bool update = false)
    {
        if (show)
        {
            m_CompleteBuildingImage.gameObject.SetActive(true);
            m_MyCanvasGroup.alpha = 1.0f;

            if (update)
                m_CompleteBuildingImage.color = Color.white;
            else
            {
                m_CompleteBuildingImage.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
                m_CompleteBuildingImage.DOColor(Color.white, 1.2f).SetEase(Ease.Linear);
            }
        }
        else
        {
            if (update)
                m_MyCanvasGroup.alpha = 0.0f;
            else
                m_MyCanvasGroup.DOFade(0.0f, 1.0f).SetEase(Ease.Linear);
        }
    }
}
