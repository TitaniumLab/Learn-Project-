using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace LearnProject
{
    public class PlayVideoFragment : MonoBehaviour, ILessonFragment
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        [SerializeField] private RectTransform _videoImage;
        [SerializeField] private float _apperDuration = 0.5f;

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayVideo(fragment.VPlayer);
        }

        private async Task PlayVideo(VideoPlayer videoPlayer)
        {
            //_videoPlayer = Instantiate(videoPlayer, transform);
            //_videoPlayer.targetTexture.Release();
            ////_videoPlayer.targetTexture.width = (int)_videoPlayer.clip.width;
            ////_videoPlayer.targetTexture.height = (int)_videoPlayer.clip.height;
            //_videoPlayer.targetTexture.Create();

            //_videoPlayer.clip = videoClip;
            _videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "TestTrim.mp4");

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
