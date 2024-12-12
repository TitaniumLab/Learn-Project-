using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LearnProject.LessonFragmentSO;

namespace LearnProject
{
    public class LessonManager : MonoBehaviour
    {
        [SerializeField] private LessonPresetUI _lessonUIprefab;
        [SerializeField] private LessonFragmentSO[] _lessonFragments;
        [SerializeField] private float _showScoreDuration = 3;
        private ShowAddedScoreData _showAddedScore;
        private static LessonManager _instance;
        private int _maxScore = 0;
        private int _currentScore = 0;
        private TextMeshProUGUI _scoreText;
        private int _count = 0;

        private ILessonFragment _videoF, _chooseAnswerF, _audioFragment;


        private void Awake()
        {
            _instance = this;
            var lessonUI = Instantiate(_lessonUIprefab);

            _videoF = lessonUI.VideoFragmentManager;
            _chooseAnswerF = lessonUI.ChooseAnswerManager;
            _audioFragment = lessonUI.PlayAudioFragment;

            _scoreText = lessonUI.ScoreText;
            _showAddedScore = lessonUI.AddedScore;
        }


        private void Start()
        {
            NextFragment();
            SceneTransitionAnimator.PlayTransitionAsync(1, 0);
        }


        private void OnDestroy()
        {
            _instance = null;
        }


        private async void NextFragment()
        {
            if (_count >= _lessonFragments.Length)
            {
                OnLessonEnd();
                return;
            }

            _maxScore += _lessonFragments[_count].ScoreToAdd;

            switch (_lessonFragments[_count].Type)
            {
                case LessonFragmentType.PlayVideo:
                    await _videoF.PlayFragment(_lessonFragments[_count]);
                    break;
                case LessonFragmentType.ChooseCorrectAnswer:
                    await _chooseAnswerF.PlayFragment(_lessonFragments[_count]);
                    break;
                case LessonFragmentType.PlayAudio:
                    await _audioFragment.PlayFragment(_lessonFragments[_count]);
                    break;
            }


            Debug.Log($"Fragment played {_count}");
            _count++;
            NextFragment();
        }

        private async void OnLessonEnd()
        {
            var isDone = Convert.ToBoolean(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
            Debug.Log(SceneManager.GetActiveScene().name);
            if (!isDone && _maxScore == _currentScore)
            {
                Debug.Log(SceneManager.GetActiveScene().name);
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            }
            await SceneTransitionAnimator.PlayTransitionAsync(0, 1);

            await SceneManager.LoadSceneAsync(0);
        }


        public static async void ShowAddedScore(Vector3 position, int addedScore)
        {
            _instance._showAddedScore.ScoreCanvasGroup.transform.position = position;
            _instance._showAddedScore.AddedScoreText.text = $"+{addedScore}";
            _instance._showAddedScore.ScoreCanvasGroup.alpha = 1;
            var halfDuration = _instance._showScoreDuration / 2;
            await Awaitable.WaitForSecondsAsync(halfDuration);
            _instance._showAddedScore.ScoreCanvasGroup.DOFade(0, halfDuration).SetLink(_instance.gameObject);
        }


        public static void AddScore(int scoreToAdd)
        {
            _instance._currentScore += scoreToAdd;
            _instance._scoreText.text = _instance._currentScore.ToString();
        }
    }
}
