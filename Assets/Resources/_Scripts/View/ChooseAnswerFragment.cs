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

        public Task PlayFragment(LessonFragmentSO fragment)
        {
            CreateAnswers(fragment.ChooseAnswerData);
            return Task.CompletedTask;
        }


        private void CreateAnswers(ChooseAnswerData data)
        {
            var buffer = data.Answers.ToList();
            for (int i = 0; i < data.Answers.Length; i++)
            {
                int rand = Random.Range(0, buffer.Count);
                var answer = buffer[rand];
                var obj = Instantiate(_preset, transform);
                obj.Image.sprite = answer.Sprite;
                _answers.Add(obj);
                buffer.RemoveAt(rand);
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
