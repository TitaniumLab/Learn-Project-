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

        private SerializedProperty _vClip;

        private SerializedProperty _chooseIntro, _chooseCorrect, _chooseUncorrect, _chooseCorrectHost, _chooseUncorrectHost;
        private SerializedProperty _answers;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _vClip = serializedObject.FindProperty("_vClip");
            _chooseIntro = serializedObject.FindProperty("_chooseIntro");
            _chooseCorrect = serializedObject.FindProperty("_chooseCorrect");
            _chooseUncorrect = serializedObject.FindProperty("_chooseUncorrect");
            _chooseCorrectHost = serializedObject.FindProperty("_chooseCorrectHost");
            _chooseUncorrectHost = serializedObject.FindProperty("_chooseUncorrectHost");
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
                    EditorGUILayout.PropertyField(_chooseIntro);
                    EditorGUILayout.PropertyField(_chooseCorrect);
                    EditorGUILayout.PropertyField(_chooseUncorrect);
                    EditorGUILayout.PropertyField(_chooseCorrectHost);
                    EditorGUILayout.PropertyField(_chooseUncorrectHost);
                    EditorGUILayout.PropertyField(_answers);
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void OnValidate()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
    }
}
