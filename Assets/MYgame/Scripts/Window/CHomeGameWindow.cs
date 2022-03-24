using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.Events;
using MYgame.Scripts.Scenes.GameScenes.Data;
using UnityEngine.UI;

public class CHomeGameWindow : CSingletonMonoBehaviour<CHomeGameWindow>
{
    [System.Serializable]
    public class DataUIColorAccumulationNumberBricks
    {
        public CUITextImage m_AccumulationNumberBricks = null;
        public Sprite m_Sprite = null;
        [HideInInspector] public int m_Coumt = 0;
    }

    // ==================== SerializeField ===========================================

    [SerializeField] GameObject m_ShowObj = null;

    [Header("Top UI")]
    [SerializeField] protected CUITextImage m_TotalBricks = null;
    [SerializeField] protected CUIButton m_BuySkinBtn = null;
    [SerializeField] protected CUIButton m_HistoryBtn = null;
    [SerializeField] protected Image m_NewImage = null;
    [SerializeField] protected Image m_SkinNewImage = null;
    [SerializeField] protected CUIText m_HistoryNumber = null;
    [Header("Center UI")]
    [SerializeField] protected CUITextImage m_StageCurLevelText = null;
    [SerializeField] protected CUIButton m_StartBtn         = null;
    [SerializeField] protected CUITextImage m_TargetCoin    = null;
    [Header("Down UI")]
    [VarRename(new string[] { "Red", "Yellow", "Green", "Blue", "White" })]
    [SerializeField] protected List<DataUIColorAccumulationNumberBricks> m_AllAccumulationNumberBricks = null;

    //[SerializeField] protected List<CUITextImage> m_AllAccumulationNumberBricks = null;
    // ==================== SerializeField ===========================================

    bool m_CloseShowUI = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        m_StartBtn.AddListener(CloseShowUI);

        m_BuySkinBtn.AddListener(ShowBuySkinWindow);

        m_HistoryBtn.AddListener(HistoryWindow);

        m_StageCurLevelText.SetNumber(CSaveManager.m_status.m_LevelIndex + 1);

        int lTempCount = 0;
        for (int i = 0; i < (int)StaticGlobalDel.EBrickColor.eMax; i++)
        {
            m_AllAccumulationNumberBricks[i].m_AccumulationNumberBricks.SetSprite(m_AllAccumulationNumberBricks[i].m_Sprite);
            lTempCount = CSaveManager.m_status.m_AllBrickColorObj[i].count;
            m_AllAccumulationNumberBricks[i].m_Coumt = lTempCount;
            m_AllAccumulationNumberBricks[i].m_AccumulationNumberBricks.SetNumber(lTempCount);
        }
    }

    public void ShowWindow()
    {
        m_CloseShowUI = false;
        m_ShowObj.SetActive(true);
    }

    public bool GetShow() { return m_ShowObj.activeSelf; }

    public void CloseShowUI()
    {
        if (m_CloseShowUI)
            return;

        m_CloseShowUI = true;
        m_ShowObj.SetActive(false);
    }

    public void ShowBuySkinWindow()
    {

    }

    public void HistoryWindow()
    {

    }

    public void SetTargetCoin(int coin)
    {
        m_TargetCoin.SetNumber(coin);
    }

    public void StartBtnClickAddListener(UnityAction call){m_StartBtn.AddListener(call);}
    public void BuySkinBtnClickAddListener(UnityAction call){ m_BuySkinBtn.AddListener(call);}
    public void HistoryBtnClickAddListener(UnityAction call){ m_HistoryBtn.AddListener(call);}

    public void SetStageData(StageData setStageData)
    {
        //BuildingRecipeData[] lTempAllBuildingRecipeData = setStageData.buildings;
        //BrickAmount lTempBrickAmount = null;
        //DataUIColorAccumulationNumberBricks lTempDataUIColorAccumulationNumberBricks = null;

        //for (int i = 0; i < lTempAllBuildingRecipeData.Length; i++)
        //{
        //    for (int x = 0; x < lTempAllBuildingRecipeData[i].brickAmounts.Length; x++)
        //    {
        //        lTempBrickAmount = lTempAllBuildingRecipeData[i].brickAmounts[x];
        //        lTempDataUIColorAccumulationNumberBricks = m_AllAccumulationNumberBricks[(int)lTempBrickAmount.color];
        //        lTempDataUIColorAccumulationNumberBricks.m_Coumt += lTempBrickAmount.amount;
        //    }
        //}
    }

    public void UpdateAllAccumulationNumberBricks()
    {
        //foreach (DataUIColorAccumulationNumberBricks ANB in m_AllAccumulationNumberBricks)
        //    ANB.m_AccumulationNumberBricks.SetNumber(ANB.m_Coumt);
    }

    public void ShowSkinNewImage(bool show){ m_SkinNewImage.gameObject.SetActive(show);}
}
