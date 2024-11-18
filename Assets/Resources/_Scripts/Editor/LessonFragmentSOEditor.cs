using UnityEditor;
using static LearnProject.LessonFragmentSO;

namespace LearnProject
{
    [CustomEditor(typeof(LessonFragmentSO), true)]
    [CanEditMultipleObjects]
    public class LessonFragmentSOEditor : Editor
    {
        private SerializedProperty _type;

        private SerializedProperty _vClip;

        private SerializedProperty _aClip;
        private SerializedProperty _answers;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _vClip = serializedObject.FindProperty("_vClip");
            _aClip = serializedObject.FindProperty("_aClip");
            _answers = serializedObject.FindProperty("_answers");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_type);
            LessonFragmentType type = (LessonFragmentType)_type.enumValueIndex;
            switch (type)
            {
                case LessonFragmentType.PlayVideo:
                    EditorGUILayout.PropertyField(_vClip);
                    break;

                case LessonFragmentType.ChooseCorrectAnswer:
                    EditorGUILayout.PropertyField(_aClip);
                    EditorGUILayout.PropertyField(_answers);
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
