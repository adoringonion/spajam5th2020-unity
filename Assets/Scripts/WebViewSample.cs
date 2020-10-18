using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebViewSample : MonoBehaviour
{
    WebViewObject webViewObject;

    void Start()
    {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init(
            ld: (msg) => Debug.Log(string.Format("CallOnLoaded[{0}]", msg)),
            enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
        // お好きなMarginにしてください
        webViewObject.SetMargins(100, 100, 100, 100);
        webViewObject.SetVisibility(true);
        // お好きなURLにしてください
        webViewObject.LoadURL("https://2wvmez8c6g.execute-api.ap-northeast-1.amazonaws.com/production/google/auth");

        webViewObject.Init((msg) => {

            this.gameObject.SetActive(false) ;
            //次のシーンへ
            SceneManager.LoadScene("Scenes/Main");
        });
    }
}