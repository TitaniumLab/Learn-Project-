using System;
using UnityEngine;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "LessonTaskBundleData", menuName = "Scriptable Objects/LessonTaskBundleData")]
    public class LessonTaskBundleData : ScriptableObject
    {
        [field: SerializeField] public AudioClip Clip;
        [field: SerializeField] public AnswerData[] Datas;
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
