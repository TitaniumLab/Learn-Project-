using UnityEngine;
using UnityEngine.UI;

namespace LearnProject
{
    [RequireComponent(typeof(Button))]
    public class ChooseAnswerPreset : MonoBehaviour
    {
        public RectTransform RT { get { return GetComponent<RectTransform>(); } }
        [field: SerializeField] public Image Image { get; private set; }
        public Button Button { get { return GetComponent<Button>(); } }
    }
}
