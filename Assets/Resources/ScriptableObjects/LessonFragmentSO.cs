using System;
using UnityEngine;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "LessonFragmentSO", menuName = "Scriptable Objects/LessonFragmentSO")]
    public class LessonFragmentSO : ScriptableObject, IVideoFragmentData, IChooseAnswerData
    {
        // For some reason custom inspector can't read from [field: SerializeField] ..... ¯\_(ツ)_/¯
        [SerializeField] private LessonFragmentType _type;
        private LessonFragmentType _oldType; // Allows to reset an object when changing its type
        [SerializeField] private PlayVideoData _videoData;
        [SerializeField] private ChooseAnswerData _chooseAnswerData;

        public PlayVideoData PlayVideoData { get { return _videoData; } }
        public ChooseAnswerData ChooseAnswerData { get { return _chooseAnswerData; } }
        public LessonFragmentType Type { get { return _type; } }


        public enum LessonFragmentType
        {
            PlayVideo = 0,
            ChooseCorrectAnswer = 1
        }


        private void OnValidate()
        {
            // Reset object
            if (_type != _oldType)
            {
                _videoData = new PlayVideoData();
                _chooseAnswerData = new ChooseAnswerData();
                _oldType = _type;
            }
        }
    }

    [Serializable]
    public class PlayVideoData
    {
        /// <summary>
        /// Put video in .../Assets/StreamingAssets/Videos/
        /// </summary>
        [Tooltip("Put video in .../Assets/StreamingAssets/Videos/")]
        [field: SerializeField] public string VideoFileName { get; private set; }
    }


    [Serializable]
    public class ChooseAnswerData
    {
        [field: SerializeField] public AudioClip ChooseIntro { get; private set; }
        [field: SerializeField] public AudioClip ChooseCorrect { get; private set; }
        [field: SerializeField] public AudioClip ChooseUncorrect { get; private set; }
        [field: SerializeField] public AudioClip ChooseCorrectHost { get; private set; }
        [field: SerializeField] public AudioClip ChooseUncorrectHost { get; private set; }
        [field: SerializeField] public AnswerData[] Answers { get; private set; }
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
