using UnityEditor;
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
        private SerializedProperty _playAudioData;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _scoreToAdd = serializedObject.FindProperty("_scoreToAdd");
            _videoData = serializedObject.FindProperty("_videoData");
            _chooseAnswerData = serializedObject.FindProperty("_chooseAnswerData");
            _playAudioData = serializedObject.FindProperty("_playAudioData");
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
                case LessonFragmentType.PlayAudio:
                    EditorGUILayout.PropertyField(_playAudioData);
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
