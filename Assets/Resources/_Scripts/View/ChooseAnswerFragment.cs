using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    [RequireComponent(typeof(AudioSource))]
    public class ChooseAnswerFragment : MonoBehaviour, ILessonFragment
    {
        private AudioSource _as;
        [SerializeField] private float _animDuration = 0.35f;
        //private AudioClip _introAudio, _correctChoice, _uncorrectChoice;
        public Task PlayFragment(LessonFragmentSO fragment)
        {
            throw new System.NotImplementedException();
        }
    }
}
