using UnityEditor;
using UnityEngine;
using static LearnProject.LessonFragmentSO;

namespace LearnProject
{
    [CustomEditor(typeof(LessonFragmentSO), true)]
    [CanEditMultipleObjects]
    public class LessonFragmentSOEditor : Editor
    {
        private SerializedProperty _type;
        private SerializedProperty _scoreToAdd;
        private SerializedProperty _videoData;
        private SerializedProperty _chooseAnswerData;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _scoreToAdd = serializedObject.FindProperty("_scoreToAdd");
            _videoData = serializedObject.FindProperty("_videoData");
            _chooseAnswerData = serializedObject.FindProperty("_chooseAnswerData");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_type);
            LessonFragmentType type = (LessonFragmentType)_type.enumValueIndex;
            switch (type)
            {
                case LessonFragmentType.PlayVideo:
                    EditorGUILayout.PropertyField(_videoData);
                    break;

                case LessonFragmentType.ChooseCorrectAnswer:
                    EditorGUILayout.PropertyField(_scoreToAdd);
                    EditorGUILayout.PropertyField(_chooseAnswerData);
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
