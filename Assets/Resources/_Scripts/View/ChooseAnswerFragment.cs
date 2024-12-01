using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    public class ChooseAnswerFragment : MonoBehaviour, ILessonFragment
    {
        [SerializeField] private ChooseAnswerPreset _preset;
        [SerializeField] private AudioSource _vfxAS, _hostAS;
        [SerializeField] private float _animDuration = 0.35f;
        private List<ChooseAnswerPreset> _answers = new List<ChooseAnswerPreset>();

        //private AudioClip _intro, _correct, _uncorrect, _correctHost, _uncorrectHost;
        public Task PlayFragment(LessonFragmentSO fragment)
        {
            CreateAnswers(fragment);
            _vfxAS.Play();
            return Task.CompletedTask;
        }


        private void CreateAnswers(LessonFragmentSO fragment)
        {
            if (_answers.Count > 0)
            {
                foreach (var item in _answers)
                {
                    Destroy(item.gameObject);
                }
                _answers.Clear();
            }

            var buffer = fragment.Answers.ToList();
            for (int i = 0; i < fragment.Answers.Length; i++)
            {
                int rand = Random.Range(0, buffer.Count);
                var answer = buffer[rand];
                var obj = Instantiate(_preset, transform);
                obj.Image.sprite = answer.Sprite;
                _answers.Add(obj);
                buffer.RemoveAt(rand);
            }
        }
    }
}
