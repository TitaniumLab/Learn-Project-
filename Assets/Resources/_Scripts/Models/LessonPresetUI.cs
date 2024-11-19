using UnityEngine;

namespace LearnProject
{
    public class LessonPresetUI : MonoBehaviour
    {
        [field: SerializeField] public RectTransform TransitionRT { get; private set; }
        [field: SerializeField] public RectTransform VideoRT { get; private set; }
    }
}
