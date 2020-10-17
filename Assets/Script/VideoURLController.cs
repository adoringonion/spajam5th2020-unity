using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;

using Utf8Json;

public class VideoURLController : MonoBehaviour
{

    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    private string url = "https://2wvmez8c6g.execute-api.ap-northeast-1.amazonaws.com/production/video/show";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        //コルーチンで
        StartCoroutine("OnSend");
    }

    //コルーチン
    IEnumerator OnSend()
    {
        //
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        
        yield return webRequest.SendWebRequest();

        //エラーが出ていないかチェック
        if (webRequest.isNetworkError)
        {
            //通信失敗
            Debug.Log(webRequest.error);
            
        }
        else
        {
            var json = webRequest.downloadHandler.text;
            //通信成功
            Debug.Log(json);

            var json2 = JsonSerializer.Deserialize<dynamic>(json);

            var _videoUrl = json2["video_url"];

            Debug.Log(_videoUrl);

            videoPlayer.url = _videoUrl;

            videoPlayer.Play();
        }
    }
}
