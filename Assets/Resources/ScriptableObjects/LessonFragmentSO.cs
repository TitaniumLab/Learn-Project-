using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "LessonFragmentSO", menuName = "Scriptable Objects/LessonFragmentSO")]
    public class LessonFragmentSO : ScriptableObject
    {
        // For some reason custom inspector can't read from [field: SerializeField] ..... ¯\_(ツ)_/¯
        [SerializeField] private LessonFragmentType _type;
        // Play video fragment
        [SerializeField] private VideoClip _vClip;
        // Choose answer fragment
        [SerializeField] private AudioClip _chooseIntro, _chooseCorrect, _chooseUncorrect, _chooseCorrectHost, _chooseUncorrectHost;
        [SerializeField] private AnswerData[] _answers;

        public LessonFragmentType Type { get { return _type; } }
        // Play video
        public VideoClip VClip { get { return _vClip; } }
        // Choose answer
        public AudioClip ChooseIntro { get { return _chooseIntro; } }
        public AudioClip ChooseCorrect { get { return _chooseCorrect; } }
        public AudioClip ChooseUncorrect { get { return _chooseUncorrect; } }
        public AudioClip ChooseCorrectHost { get { return _chooseCorrectHost; } }
        public AudioClip ChooseUncorrectHost { get { return _chooseUncorrectHost; } }
        public AnswerData[] Answers { get { return _answers; } }

        public enum LessonFragmentType
        {
            PlayVideo = 0,
            ChooseCorrectAnswer = 1
        }



        private void OnValidate()
        {
            //EditorUtility.SetDirty(this);
            //AssetDatabase.SaveAssetIfDirty(this);
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
#endif
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
