using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MYgame.Scripts.Scenes.GameScenes.Data;
using DG.Tweening;

public class CRoleMugShot : MonoBehaviour
{

    [SerializeField] protected Image m_MugShot = null;
    [SerializeField] protected CUIButton m_MyButton = null;
    [SerializeField] protected int m_Index = 0;

     protected CSelectRole m_MySelectRole = null;
     protected RectTransform m_MyRectTransform = null;
     public RectTransform MyRectTransform => m_MyRectTransform;

    private void Awake()
    {
        m_MugShot = this.GetComponent<Image>();
        m_MyButton = this.GetComponent<CUIButton>();
        m_MyRectTransform = this.GetComponent<RectTransform>();
        m_MySelectRole = this.GetComponentInParent<CSelectRole>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CDataRole[] TempAllDataRole = CGGameSceneData.SharedInstance.m_AllDataRole;
        m_MugShot.sprite = TempAllDataRole[m_Index].MugShot;

        m_MyButton.AddListener(() => {
            m_MySelectRole.UpdateCurShowImage(m_Index);
        });
    }


    public void PlayFoucsAnima(bool play)
    {
        if (play)
        {
            m_MyRectTransform.localScale = Vector3.one;
            Tween lTempTween = m_MyRectTransform.DOShakeScale(1.0f, 0.2f, 1, 2, false).SetEase( Ease.Linear);
            //Tween lTempTween = m_MyRectTransform.DOPunchScale(Vector3.one * 0.2f, 1.0f, 1, 1.0f).SetEase(Ease.Linear); ;
            lTempTween.SetLoops(-1);
            lTempTween.SetId(m_MyRectTransform);
        }
        else
        {
            DOTween.Kill(m_MyRectTransform);
            m_MyRectTransform.localScale = Vector3.one;
        }
    }
}
