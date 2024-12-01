using UnityEngine;

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
    }
}
