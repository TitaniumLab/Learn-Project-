using System;
using UnityEngine;
using UnityEngine.Video;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "LessonFragmentSO", menuName = "Scriptable Objects/LessonFragmentSO")]
    public class LessonFragmentSO : ScriptableObject
    {
        // For some reason custom inspector can't read from [field: SerializeField] ..... ¯\_(ツ)_/¯
        [SerializeField] private LessonFragmentType _type;
        [SerializeField] private VideoClip _vClip;
        [SerializeField] private AudioClip _aClip;
        [SerializeField] private AnswerData[] _answers;

        public LessonFragmentType Type { get { return _type; } }
        public VideoClip VClip { get { return _vClip; } }
        public AudioClip AClip { get { return _aClip; } }
        public AnswerData[] Answers { get { return _answers; } }

        public enum LessonFragmentType
        {
            PlayVideo = 0,
            ChooseCorrectAnswer = 1
        }
    }


    [Serializable]
    public class AnswerData
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [SerializeField] private bool _isCorrect;
        public bool IsCorrect { get { return _isCorrect; } }
        [field: ConditionalHide(nameof(_isCorrect))]
        [field: SerializeField] public int ScoreToAdd { get; private set; }
    }
}
