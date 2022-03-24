using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CSEPlayObj : MonoBehaviour
{
    public enum ESE
    {
        eCollect                = 0,
        eButton                 = 1,
        eBuySkin                = 2,
        eIngameSampleShow       = 3,
        eStartCountdown         = 4,
        eStartCountdownGo       = 5,
        eIngameFallWater        = 6,
        eIngameDash             = 7,
        eIngamePut              = 8,
        eIngameScale            = 9,
        eIngameMoveToTotleIn    = 10,
        eIngameTimePlus         = 11,
        eIngameTargetClear      = 12,
        eIngameTimeLeft         = 13,
        eIngameTimeOver         = 14,
        eMax
    }


    public enum EUpdateFuncType
    {
        ePriorityAdd        = 0,
        ePriorityReduce     = 1,
        eMax                   ,
    }

    [System.Serializable]
    public class CDataAudio
    {
        public AudioClip m_SEClips = null;
        public ESE m_ID = ESE.eMax;
        public float m_Volume = 1.0f;
        public int m_DEFPriority = 128;

        [VarRename(EUpdateFuncType.eMax)]
        public EUpdateFuncType[] m_UpdateFunc = null;

        [HideInInspector]
        public bool m_Play = false;
    }

    [SerializeField] protected AudioSource m_Source;
    public AudioSource Source => m_Source;

    public List<UnityAction> m_UpdateFunc = new List<UnityAction>();

    private void Awake()
    {
        
    }

    public void Init(CDataAudio initdata)
    {
        if (initdata.m_UpdateFunc != null)
        {
            m_UpdateFunc.Clear();

            void UpdateFuncPriorityAdd() { m_Source.priority += 1; }
            void UpdateFuncPriorityReduce() { m_Source.priority -= 1; }
            UnityAction[] lTempUpdateFunc = {
            UpdateFuncPriorityAdd,          //ePriorityAdd        = 0,
            UpdateFuncPriorityReduce        //ePriorityReduce     = 1,
            };

            for (int i = 0; i < initdata.m_UpdateFunc.Length; i++)
                m_UpdateFunc.Add(lTempUpdateFunc[(int)initdata.m_UpdateFunc[i]]);
        }

        Source.volume = initdata.m_Volume;
        Source.priority = initdata.m_DEFPriority;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var itam in m_UpdateFunc)
            itam();

        if (!m_Source.isPlaying)
            Destroy(this.gameObject);
    }



}
