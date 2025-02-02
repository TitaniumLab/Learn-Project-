using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LearnProject
{
    [CreateAssetMenu(fileName = "DataBundleForLessonTileSO", menuName = "Scriptable Objects/DataBundleForLessonTileSO")]
    public class DataBundleForLessonTileSO : ScriptableObject
    {
        [field: SerializeField] public AssetReference Scene { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string LessonName { get; private set; }
    }
}
