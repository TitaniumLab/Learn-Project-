using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    public class ChooseAnswerFragment : MonoBehaviour, ILessonFragment
    {
        [SerializeField] private ChooseAnswerPreset _preset;
        [SerializeField] private AudioSource _aSource;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _animDuration = 0.35f;
        [SerializeField] private float _correctAnimAmpl = 1.3f;
        private bool _done = false;
        private List<ChooseAnswerPreset> _answers = new List<ChooseAnswerPreset>();
        private int _tryCount;

        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            _done = false;
            _tryCount = 0;
            DestroyAnswers();
            NextTry(fragment);
            while (!_done)
            {
                await Task.Yield();
            }
        }


        private async void NextTry(LessonFragmentSO fragment)
        {
            CreateAnswers(fragment);
            SetInteractableButtons(false);
            var tasks = new Task[_answers.Count];
            for (int i = 0; i < _answers.Count; i++)
            {
                // Appear animation
                tasks[i] = _answers[i].RT.DOScale(1, _animDuration).SetEase(Ease.OutBounce, 2, 1).From(Vector3.zero).AsyncWaitForCompletion();
            }
            await Task.WhenAll(tasks);
            if (_tryCount == 0)
            {
                await PlayAudioAsync(_aSource, fragment.ChooseAnswerData.IntroHost);
            }
            SetInteractableButtons(true);
        }



        private void CreateAnswers(LessonFragmentSO fragment)
        {
            var buffer = fragment.ChooseAnswerData.Answers.ToList();
            for (int i = 0; i < fragment.ChooseAnswerData.Answers.Length; i++)
            {
                int rand = Random.Range(0, buffer.Count);
                var answer = buffer[rand];
                var obj = Instantiate(_preset, transform);
                obj.Image.sprite = answer.Sprite;
                obj.Button.onClick.AddListener(OnAnswerClick);
                _answers.Add(obj);
                buffer.RemoveAt(rand);


                void OnAnswerClick()
                {
                    SetInteractableButtons(false);
                    if (answer.IsCorrect)
                    {
                        OnCorrectAnswer();
                    }
                    else
                    {
                        OnInCorrectAnswer();
                    }
                }


                async void OnCorrectAnswer()
                {
                    if (_tryCount == 0)
                    {
                        LessonManager.AddScore(fragment.ScoreToAdd);
                        LessonManager.ShowAddedScore(obj.transform.position, fragment.ScoreToAdd);
                    }

                    _particleSystem.transform.position = obj.transform.position;
                    _particleSystem.Play();
                    var tasks = new Task[]
                    {
                        obj.RT.DOScale(_correctAnimAmpl, _animDuration / 2).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion(),
                        PlayAudioAsync(_aSource,fragment.ChooseAnswerData.CorrectVFX)
                    };
                    await Task.WhenAll(tasks);
                    await PlayAudioAsync(_aSource, fragment.ChooseAnswerData.CorrectHost);
                    await DisapearAnimation();
                    _done = true;
                }


                async void OnInCorrectAnswer()
                {
                    ++_tryCount;
                    var tasks = new Task[]
                    {
                        obj.RT.DOPunchRotation(new Vector3(0, 0, 30), _animDuration, 8).AsyncWaitForCompletion(),
                        PlayAudioAsync(_aSource,fragment.ChooseAnswerData.IncorrectVFX)
                    };
                    await Task.WhenAll(tasks);
                    await PlayAudioAsync(_aSource, fragment.ChooseAnswerData.IncorrectHost);
                    await DisapearAnimation();
                    DestroyAnswers();
                    NextTry(fragment);
                }
            }
        }


        private async Task PlayAudioAsync(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
            while (audioSource.isPlaying)
            {
                await Task.Yield();
            }
        }


        private async Task DisapearAnimation()
        {
            var tasks = new Task[_answers.Count];
            for (int i = 0; i < _answers.Count; i++)
            {
                // Appear animation
                tasks[i] = _answers[i].RT.DOScale(0, _animDuration).From(Vector3.one).AsyncWaitForCompletion();
            }
            await Task.WhenAll(tasks);
        }


        private void SetInteractableButtons(bool isInteractable)
        {
            foreach (var obj in _answers)
            {
                obj.Button.interactable = isInteractable;
            }
        }



        private void DestroyAnswers()
        {
            if (_answers.Count > 0)
            {
                foreach (var item in _answers)
                {
                    Destroy(item.gameObject);
                }
                _answers.Clear();
            }
        }
    }
}
