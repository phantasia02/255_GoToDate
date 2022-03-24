using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CAudioManager : CSingletonMonoBehaviour<CAudioManager>
{
 

    public enum EBGM
    {
        eOutGame    = 0,
        eGameWin    = 1,
        eGameOver   = 2,
        eMax
    }

  

    [System.Serializable]
    public class CDataBGMAudio
    {
        public AudioClip m_BGMClips = null;
        public EBGM m_ID = EBGM.eMax;
    }


    [VarRename(CSEPlayObj.ESE.eMax)]
    [SerializeField] CSEPlayObj.CDataAudio[] m_AllSEclips = new CSEPlayObj.CDataAudio[(int)CSEPlayObj.ESE.eMax];

    [VarRename(EBGM.eMax)]
    [SerializeField] CDataBGMAudio[] m_AllBGMclips = new CDataBGMAudio[(int)EBGM.eMax];

    [SerializeField] GameObject m_PototypeSEPlayObj;
    [SerializeField] GameObject m_PototypeBGMPlayObj;

    protected Dictionary<CSEPlayObj.ESE, CSEPlayObj.CDataAudio> m_MapSEData = new Dictionary<CSEPlayObj.ESE, CSEPlayObj.CDataAudio>();
    protected Dictionary<EBGM, CDataBGMAudio> m_MapBGMData = new Dictionary<EBGM, CDataBGMAudio>();

    protected EBGM m_CurBgmIndex = EBGM.eMax;
    protected CBGMPlayObj m_CurBgmSource = null;
    protected CBGMPlayObj m_OldBgmSource = null;

    private void Awake()
    {
        foreach (var item in m_AllSEclips)
            m_MapSEData.Add(item.m_ID, item);

        foreach (var item in m_AllBGMclips)
            m_MapBGMData.Add(item.m_ID, item);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetOpenAudio(CSaveManager.m_config.m_Sound);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in m_AllSEclips)
            item.m_Play = false;
    }

    public void PlaySE(CSEPlayObj.ESE eIndex)
    {
        CSEPlayObj.CDataAudio lTempPlaySE = null;
        if (!m_MapSEData.TryGetValue(eIndex, out lTempPlaySE))
            return;

        if (lTempPlaySE.m_Play)
            return;

        GameObject lTempSEObj = GameObject.Instantiate<GameObject>(m_PototypeSEPlayObj, transform);
        lTempSEObj.transform.position = this.transform.position;
        CSEPlayObj lTempSE = lTempSEObj.GetComponent<CSEPlayObj>();
        lTempSE.Init(lTempPlaySE);
        lTempSE.Source.clip = lTempPlaySE.m_SEClips;
        lTempPlaySE.m_Play = true;
    }

    /// <summary>
    /// Play BGM
    /// </summary>
    /// <param name="Fadetime">Fadein Old BGM time</param>
    public void PlayBGM(EBGM eIndex, float Fadetime = 0.0f)
    {
        m_CurBgmIndex = eIndex;

        if (m_CurBgmSource != null && Fadetime > 0.0f)
        {
            if (m_OldBgmSource == null)
            {
                m_OldBgmSource = m_CurBgmSource;
                m_CurBgmSource.Source.DOFade(0.0f, Fadetime).SetId(m_CurBgmSource).
                    onComplete = () =>{PlayCurBgm();};
            }
        }
        else
            PlayCurBgm();
    }

    public void PlayCurBgm()
    {
        CDataBGMAudio lTempPlayBGM = null;
        if (!m_MapBGMData.TryGetValue(m_CurBgmIndex, out lTempPlayBGM))
            return;

        GameObject lTempSEObj = GameObject.Instantiate<GameObject>(m_PototypeBGMPlayObj, transform);
        lTempSEObj.transform.position = this.transform.position;
        CBGMPlayObj lTempSE = lTempSEObj.GetComponent<CBGMPlayObj>();
        lTempSE.Source.clip = lTempPlayBGM.m_BGMClips;
        m_CurBgmSource = lTempSE;

        if (m_OldBgmSource != null)
        {
            Destroy(m_OldBgmSource.gameObject);
            m_OldBgmSource = null;
        }
    }

    /// <summary>
    /// Stop BGM
    /// </summary>
    /// <param name="Fadetime">Fadein Cur BGM time</param>
    public void StopCurBgm(float Fadetime = 0.0f)
    {
        if (m_CurBgmSource == null)
            return;

        if (Fadetime > 0.0f)
        {
            DOTween.Kill(m_CurBgmSource);
            m_CurBgmSource.Source.DOFade(0.0f, Fadetime).SetId(m_CurBgmSource).onComplete
                = () =>
                {
                    if (m_CurBgmSource != null)
                    {
                        Destroy(m_CurBgmSource.gameObject);
                        m_CurBgmSource = null;
                        m_OldBgmSource = null;
                    }
                };
        }
        else
        {
            Destroy(m_CurBgmSource.gameObject);
            m_CurBgmSource = null;
            m_OldBgmSource = null;
        }
    }

    public void SetOpenAudio(int lpOpen)
    {
        if (lpOpen != 0)
            SetVolume(1.0f);
        else
            SetVolume(0.0f);
    }

    public void SetVolume(float volume)
    {
        if (volume > 1.0f)
            volume = 1.0f;
        else if (volume < 0.0f)
            volume = 0.0f;

        AudioListener.volume = volume;
    }
}
