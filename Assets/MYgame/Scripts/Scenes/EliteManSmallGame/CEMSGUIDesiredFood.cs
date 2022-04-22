using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MYgame.Scripts.Scenes.GameScenes.Data;
using UnityEngine.UI;

public class CEMSGUIDesiredFood : MonoBehaviour
{

    // ==================== SerializeField ===========================================

    [SerializeField] protected Image m_FoodIcon = null;
    // ==================== SerializeField ===========================================

    protected CDataEliteManSmallGameFood m_CurFoodData = null;
    

    private void Awake()
    {
        
    }

    public void SetData(CDataEliteManSmallGameFood parmFood)
    {
        m_FoodIcon.sprite = parmFood.MugShot;
        m_CurFoodData = parmFood;
    }

}
