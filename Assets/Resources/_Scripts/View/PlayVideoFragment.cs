using DG.Tweening;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace LearnProject
{
    public class PlayVideoFragment : MonoBehaviour, ILessonFragment
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        [SerializeField] private RectTransform _videoImage;
        [SerializeField] private string _videoFolderName = "Videos";

        private void Awake()
        {
            _videoPlayer.targetTexture.Release();
        }

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayVideo(fragment.PlayVideoData.VideoFileName);
        }

        private async Task PlayVideo(string videoName)
        {
            var path = Path.Combine(Application.streamingAssetsPath, _videoFolderName, $"{videoName}.webm");
            Debug.Log($"Read video from: {path}");
            _videoPlayer.url = path;
            _videoPlayer.time = 0;
            _videoPlayer.loopPointReached += delegate { OnVideoEnd(); };
            _videoPlayer.Play();

            bool end = false;
            while (!end)
            {
                await Task.Yield();
            }

            void OnVideoEnd()
            {
                _videoPlayer.loopPointReached -= delegate { OnVideoEnd(); };

                end = true;
            }
        }
    }
}
