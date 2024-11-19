using UnityEngine;
using UnityEngine.Video;
using static LearnProject.LessonFragmentSO;

namespace LearnProject
{
    public class LessonManager : MonoBehaviour
    {
        [SerializeField] private LessonPresetBundle _lessonBundlePrefab;
        [SerializeField] private LessonFragmentSO[] _lessonFragments;
        private RectTransform _currentRT;
        private int _count = 0;
        private LessonPresetUI _lessonUI;
        private VideoPlayer _videoPlayer;

        private void Awake()
        {
            _lessonUI = Instantiate(_lessonBundlePrefab.UIPreset);
            _videoPlayer = Instantiate(_lessonBundlePrefab.VPlayer, transform);
        }

        private async void Start()
        {
            if (_lessonFragments[0].Type == LessonFragmentType.PlayVideo)
            {
                _videoPlayer.clip = _lessonFragments[0].VClip;
                _lessonUI.VideoRT.localScale = Vector3.one;
            }

            await AnimationManager.PlayTransitionAsync(_lessonUI.TransitionRT, 1, 0);
            NextFragment();
        }


        private async void NextFragment()
        {

            if (_currentRT != null)
            {
                await AnimationManager.PlayTransitionAsync(_currentRT, 1, 0);
            }

            if (_count >= _lessonFragments.Length)
            {
                return;
            }


            switch (_lessonFragments[_count].Type)
            {
                case LessonFragmentType.PlayVideo:
                    PlayVideo();
                    break;
                case LessonFragmentType.ChooseCorrectAnswer:

                    break;
            }

            _count++;
        }



        private async void PlayVideo()
        {
            _currentRT = _lessonUI.VideoRT;
            if (_count != 0)
            {
                _videoPlayer.clip = _lessonFragments[_count].VClip;
                _videoPlayer.time = 0;
                await AnimationManager.PlayTransitionAsync(_currentRT, 0, 1);
            }
            _videoPlayer.loopPointReached += delegate { OnVideoEnd(); };
            _videoPlayer.Play();


            void OnVideoEnd()
            {
                _videoPlayer.loopPointReached -= delegate { OnVideoEnd(); };
                NextFragment();
            }
        }
    }
}
