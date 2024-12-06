using UnityEditor;
using UnityEngine;

namespace LearnProject
{
    [CustomEditor(typeof(SetScenesAsUncompleted), true)]
    [CanEditMultipleObjects]
    public class SetScenesAsUncompletedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Set as uncompleted"))
            {
                (target as SetScenesAsUncompleted).ResetScenes();
            }
        }
    }
}
