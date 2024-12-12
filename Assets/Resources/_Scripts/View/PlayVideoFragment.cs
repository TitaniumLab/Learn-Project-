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
        [SerializeField] private float _apperDuration = 0.5f;
        [SerializeField] private string _videoFolderName = "Videos";

        //private void Awake()
        //{
        //    _videoImage.localScale = Vector3.zero;
        //}

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayVideo(fragment.PlayVideoData.VideoFileName);
        }

        private async Task PlayVideo(string videoName)
        {
            //_videoPlayer.targetTexture.Release();// Prevents the first incorrect frame from appearing
            var path = Path.Combine(Application.streamingAssetsPath, _videoFolderName, $"{videoName}.webm");
            Debug.Log($"Read video from: {path}");
            _videoPlayer.url = path;
            _videoPlayer.time = 0;
            _videoPlayer.loopPointReached += delegate { OnVideoEnd(); };
            _videoPlayer.Play();

            //await _videoImage.DOScale(1, _apperDuration).From(Vector3.zero).AsyncWaitForCompletion();
            bool end = false;
            while (!end)
            {
                await Task.Yield();
            }

            void OnVideoEnd()
            {
                _videoPlayer.loopPointReached -= delegate { OnVideoEnd(); };
                _videoPlayer.targetTexture.Release();
                //await _videoImage.DOScale(0, _apperDuration).From(Vector3.one).AsyncWaitForCompletion();
                end = true;
            }
        }
    }
}
