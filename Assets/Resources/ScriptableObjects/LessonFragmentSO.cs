using System;
using UnityEngine;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "LessonFragmentSO", menuName = "Scriptable Objects/LessonFragmentSO")]
    public class LessonFragmentSO : ScriptableObject, IVideoFragmentData, IChooseAnswerData
    {
        // For some reason custom inspector can't read from [field: SerializeField] ..... ¯\_(ツ)_/¯
        [SerializeField] private LessonFragmentType _type;
        private bool _hasBeenSeted = false;
        private LessonFragmentType _oldType; // Allows to reset an object when changing its type
        [SerializeField] private PlayVideoData _videoData;
        [SerializeField] private ChooseAnswerData _chooseAnswerData;
        [SerializeField] private int _scoreToAdd;

        public PlayVideoData PlayVideoData { get { return _videoData; } }
        public ChooseAnswerData ChooseAnswerData { get { return _chooseAnswerData; } }
        public int ScoreToAdd { get { return _scoreToAdd; } }
        public LessonFragmentType Type { get { return _type; } }

        private void Awake()
        {
            _oldType = _type;
            _hasBeenSeted = true;
        }

        private void OnValidate()
        {
            // Reset all data when switching fragment type to avoid creating unused data
            if (_oldType != _type && _hasBeenSeted)
            {
                _videoData = new PlayVideoData();
                _chooseAnswerData = new ChooseAnswerData();
                _scoreToAdd = 0;
                _oldType = _type;
            }
        }



        public enum LessonFragmentType
        {
            PlayVideo = 0,
            ChooseCorrectAnswer = 1
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
        [field: SerializeField] public AudioClip IntroHost { get; private set; }
        [field: SerializeField] public AudioClip CorrectVFX { get; private set; }
        [field: SerializeField] public AudioClip IncorrectVFX { get; private set; }
        [field: SerializeField] public AudioClip CorrectHost { get; private set; }
        [field: SerializeField] public AudioClip IncorrectHost { get; private set; }
        [field: SerializeField] public AnswerData[] Answers { get; private set; }
    }


    [Serializable]
    public class AnswerData
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool IsCorrect { get; private set; }
    }
}
