using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGMPlayObj : MonoBehaviour
{
    [SerializeField] protected AudioSource m_Source;
    public AudioSource Source => m_Source;

    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Source.Play();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    m_Source.priority += 1;

    //    if (!m_Source.isPlaying)
    //        Destroy(this.gameObject);
    //}
}
