using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScenesCtrlBase : MonoBehaviour
{
    [SerializeField] protected GameObject PrefabGameSceneData = null;
   
    protected virtual void Awake()
    {
        StaticGlobalDel.CreateSingletonObj(PrefabGameSceneData);
    }
}
