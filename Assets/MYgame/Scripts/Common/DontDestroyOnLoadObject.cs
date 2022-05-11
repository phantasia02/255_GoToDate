using UnityEngine;
using System.Collections;
using GameAnalyticsSDK;

namespace UnityUtility {
	public class DontDestroyOnLoadObject : MonoBehaviour {
		void Awake () {
            GameAnalytics.Initialize();
            DontDestroyOnLoad( this );

#if !DEBUGPC
            GameObject lTempGameObject = GameObject.Find("DebugWindow");
            if (lTempGameObject != null)
                 GameObject.Destroy(lTempGameObject);
#endif
        }
    }
}
