using UnityEngine;
using UnityEngine.Video;

namespace LearnProject
{
    public class LessonPresetBundle : MonoBehaviour
    {
        [field: SerializeField] public LessonPresetUI UIPreset { get; private set; }
        [field: SerializeField] public VideoPlayer VPlayer { get; private set; }

    }
}
