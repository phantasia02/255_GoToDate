using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // ===================== UniRx ======================
    public Subject<Unit> CloseWindow = new Subject<Unit>();
    // ===================== UniRx ======================

    const string NAME_FORMAT = "{0} Singleton";

    static T sharedInstance;

    public static T SharedInstance
    {
        get
        {
            if (sharedInstance == null)
            {
                sharedInstance = FindOrCreateInstance();
            }
            return sharedInstance;
        }
    }

    public static T FindOrCreateInstance()
    {
        var instanceType = typeof(T);

        var instances = (T[])FindObjectsOfType(instanceType);
        if (instances.Length > 1)
        {
            Debug.LogError("Error");
        }

        T instance;
        if (instances.Length == 0)
        {
            //var name = string.Format(NAME_FORMAT, instanceType);
            //instance = new GameObject(name, instanceType).GetComponent<T>();
            instance = null;
        }
        else
        {
            instance = instances[0];
        }

        sharedInstance = instance;

        return instance;
    }


    public virtual void OnDestroy()
    {
        CloseWindow.OnNext(Unit.Default);
    }
    // ===================== UniRx ======================

    public Subject<Unit> ObserveDestroy() { return CloseWindow ?? (CloseWindow = new Subject<Unit>()); }

    // ===================== UniRx ======================
}
