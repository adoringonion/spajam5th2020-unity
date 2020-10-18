using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using UnityEngine.UI;

using Newtonsoft.Json;

public class VideoURLController : MonoBehaviour
{

    [JsonObject]
    class Class1
    {
        [JsonProperty("video_url")]
        public string video_url { get; set; }

    }

    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    private string url = "https://2wvmez8c6g.execute-api.ap-northeast-1.amazonaws.com/production/video/show";


    public Text text;
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
        text.text = "Load";
        //
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        
        yield return webRequest.SendWebRequest();

        //エラーが出ていないかチェック
        if (webRequest.isNetworkError)
        {
            //通信失敗
            Debug.Log(webRequest.error);

            text.text = webRequest.error;


        }
        else
        {
            var json = webRequest.downloadHandler.text;
            //通信成功
            Debug.Log(json);

            text.text = "DL OK";

            Class1 json2 = JsonConvert.DeserializeObject<Class1>(json);

            var _videoUrl = json2.video_url;

            Debug.Log(_videoUrl);

            videoPlayer.url = _videoUrl;

            //videoPlayer.Play();

            videoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;

            videoPlayer.Prepare();

            text.text = "OK";
        }
    }


    private void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        // Prepare の Completed 時にPlay を同時に呼ぶことでストリーミング再生を行う
        source.Play();
    }
}
