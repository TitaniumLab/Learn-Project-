using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace LearnProject
{
    public class TryFixWebGLVideo : MonoBehaviour
    {
        private void Start()
        {
            //string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "TestTrim.mp4");
            //UnityWebRequest request = UnityWebRequest.Get(filePath);
            //UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            //while (!operation.isDone)
            //{
            //    await Task.Yield();
            //}

            //if (request.result == UnityWebRequest.Result.Success)
            //{

            //    var player = GetComponent<VideoPlayer>();
            //    player.url = request.url;
            //    player.Play();
            //}
            //else
            //{
            //    Debug.Log("Vlead");
            //}
            //string filePath = Path.Combine(Application.streamingAssetsPath, "TestTrim.mp4");
            //var player = GetComponent<VideoPlayer>();
            //player.url = filePath;
            //player.Play();
        }
    }
}
