using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudioFragment : MonoBehaviour, ILessonFragment
    {
        private AudioSource _aSource;

        private void Awake()
        {
            _aSource = GetComponent<AudioSource>();
        }


        public async Task PlayFragment(LessonFragmentSO fragment)
        {
            await PlayAudioAsync(fragment.PlayAudioData.AudioClip);
        }

        private async Task PlayAudioAsync(AudioClip audioClip)
        {
            _aSource.PlayOneShot(audioClip);
            while (_aSource.isPlaying)
            {
                await Task.Yield();
            }
        }

    }
}