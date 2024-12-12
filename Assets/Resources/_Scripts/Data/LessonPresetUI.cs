using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace LearnProject
{
    [RequireComponent(typeof(Canvas))]
    public class LessonPresetUI : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        [field: SerializeField] public RectTransform TransitionRT { get; private set; }
        [field: SerializeField] public PlayVideoFragment VideoFragmentManager { get; private set; }
        [field: SerializeField] public ChooseAnswerFragment ChooseAnswerManager { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }
        [field: SerializeField] public ShowAddedScoreData AddedScore { get; private set; }
        [field: SerializeField] public PlayAudioFragment PlayAudioFragment { get; private set; }
    }
}
