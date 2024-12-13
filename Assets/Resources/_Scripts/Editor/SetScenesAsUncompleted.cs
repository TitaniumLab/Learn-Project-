using UnityEditor;
using UnityEngine;

namespace LearnProject
{
    /// <summary>
    /// For some reason buttons in custom inspector for Scene Asset does not work
    /// </summary>
    public class SetScenesAsUncompleted : MonoBehaviour
    {
        [SerializeField] private SceneAsset[] _sceneAssets;

        public void ResetScenes()
        {
            foreach (SceneAsset asset in _sceneAssets)
            {
                PlayerPrefs.SetInt(asset.name, 0);
            }
        }
    }
}
