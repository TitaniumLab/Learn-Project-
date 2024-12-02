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
        [SerializeField] private float _apperDuration = 0.5f;
        [SerializeField] private string _videoFolderName = "Videos";

        private void Awake()
        {
            _videoImage.localScale = Vector3.zero;
        }

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayVideo(fragment.PlayVideoData.VideoFileName);
        }

        private async Task PlayVideo(string videoName)
        {
            _videoPlayer.targetTexture.Release();// Prevents the first incorrect frame from appearing
            var path = Path.Combine(Application.streamingAssetsPath, _videoFolderName, $"{videoName}.mp4");
            Debug.Log($"Read video from: {path}");
            _videoPlayer.url = path;
            _videoPlayer.time = 0;
            _videoPlayer.loopPointReached += delegate { OnVideoEnd(); };
            _videoPlayer.Play();

            await AnimationManager.PlayScaleTransition(_videoImage, 0, 1, _apperDuration);
            bool end = false;
            while (!end)
            {
                await Task.Yield();
            }

            async void OnVideoEnd()
            {
                _videoPlayer.loopPointReached -= delegate { OnVideoEnd(); };
                await AnimationManager.PlayScaleTransition(_videoImage, 1, 0, _apperDuration);
                Destroy(_videoPlayer.gameObject);
                end = true;
            }
        }
    }
}
