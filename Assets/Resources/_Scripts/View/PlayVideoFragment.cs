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

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayVideo(fragment.VClip);
        }

        private async Task PlayVideo(VideoClip videoClip)
        {
            _videoPlayer.clip = videoClip;
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
                end = true;
            }
        }
    }
}
