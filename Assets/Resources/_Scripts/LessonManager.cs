using UnityEngine;
using static LearnProject.LessonFragmentSO;

namespace LearnProject
{
    public class LessonManager : MonoBehaviour
    {
        [SerializeField] private LessonPresetUI _lessonUIprefab;
        [SerializeField] private LessonFragmentSO[] _lessonFragments;
        private int _count = 0;

        private ILessonFragment _videoF, _chooseAnswerF;

        private void Awake()
        {
            var lessonUI = Instantiate(_lessonUIprefab);

            _videoF = lessonUI.VideoFragmentManager;
            _chooseAnswerF = lessonUI.ChooseAnswerManager;
        }

        private void Start()
        {
            NextFragment();
            SceneTransitionAnimator.PlayTransitionAsync(1, 0);
        }


        private async void NextFragment()
        {
            if (_count >= _lessonFragments.Length)
            {
                return;
            }

            switch (_lessonFragments[_count].Type)
            {
                case LessonFragmentType.PlayVideo:
                    await _videoF.PlayFragment(_lessonFragments[_count]);
                    break;
                case LessonFragmentType.ChooseCorrectAnswer:
                    await _chooseAnswerF.PlayFragment(_lessonFragments[_count]);
                    break;
            }
            Debug.Log($"Fragment played {_count}");
            _count++;
            NextFragment();
        }
    }
}
