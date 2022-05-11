using UnityEngine;
using System.Collections;
using GameAnalyticsSDK;
using Facebook.Unity;

namespace UnityUtility {
	public class DontDestroyOnLoadObject : MonoBehaviour {
		void Awake () {
            GameAnalytics.Initialize();


            if (FB.IsInitialized)
                FB.ActivateApp();
            else
                FB.Init(FB.ActivateApp);

            DontDestroyOnLoad( this );

#if !DEBUGPC
            GameObject lTempGameObject = GameObject.Find("DebugWindow");
            if (lTempGameObject != null)
                 GameObject.Destroy(lTempGameObject);
#endif
        }
    }
}
