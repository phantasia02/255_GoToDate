using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CDebugWindow : CSingletonMonoBehaviour<CDebugWindow>
{

    [SerializeField] GameObject m_Background    = null;
    [SerializeField] Button     m_Debug         = null;
    [SerializeField] Button     m_Close         = null;

    [SerializeField] Button m_Reset         = null;
    [SerializeField] Button m_Next          = null;
    [SerializeField] Button m_AddCoin       = null;
    [SerializeField] Text m_Version       = null;

    [SerializeField] CBtnCtrlNumber m_JupmNumber    = null;
    [SerializeField] CSliderCtrlNumber m_PlaySpeed    = null;
    public CSliderCtrlNumber PlaySpeed => m_PlaySpeed;

    CGameManager m_MyGameManager = null;

    CChangeScenes m_ChangeScenes = new CChangeScenes();

    private void Awake()
    {
        OnClickClose();
        m_JupmNumber.NumberMax = 100;
        m_JupmNumber.NumberMin = 1;
        //    m_JupmNumber.SetNumber(m_JupmNumber.NumberMin);
        m_Debug.onClick.AddListener(OnClickDebug);
        m_Close.onClick.AddListener(OnClickClose);
        m_Reset.onClick.AddListener(OnReset);
        m_Next.onClick.AddListener(OnNext);
        m_AddCoin.onClick.AddListener(()=> 
        {
            CSaveManager.m_status.Coin.Value += 100000;
            CSaveManager.Save();
        });

        m_JupmNumber.CilickBtn.onClick.AddListener(OnJupm);
        m_PlaySpeed.SetNumber(1000);
        m_PlaySpeed.ReturvalCallBack = (float Val)=> {
            m_SpeedVal.Value = Val;
        };

        m_Version.text = $"Version = { Application.version}";
    }

    // Start is called before the first frame update
    void Start()
    {

        m_JupmNumber.SetNumber(CSaveManager.m_status.m_LevelIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsShow(){return m_Background.activeSelf;}

    public void OnClickDebug()
    {
        m_Background.SetActive(true);
        m_Debug.gameObject.SetActive(false);

        if (m_MyGameManager == null)
            m_MyGameManager = GameObject.FindObjectOfType<CGameManager>();
    }

    public void OnClickClose()
    {
        m_Background.SetActive(false);
        m_Debug.gameObject.SetActive(true);
    }

    public void OnReset()
    {
        m_ChangeScenes.ResetScene();
    }

    public void OnNext()
    {
        m_ChangeScenes.SetNextLevel();
        m_ChangeScenes.LoadGameScenes();
    }

    public void OnJupm()
    {
        CSaveManager.m_status.LevelIndex.Value = m_JupmNumber.Number - 1;
        m_ChangeScenes.LoadGameScenes();
    }

    // ===================== UniRx ======================
    public UniRx.ReactiveProperty<float> m_SpeedVal = new UniRx.ReactiveProperty<float>(1000.0f);

    public UniRx.ReactiveProperty<float> ObserverSpeedVal()
    {
        return m_SpeedVal ?? (m_SpeedVal = new UniRx.ReactiveProperty<float>(1000.0f));
    }

    // ===================== UniRx ======================

}
