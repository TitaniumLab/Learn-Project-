using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearnProject
{
    public class LessonTile : MonoBehaviour
    {
        [field: SerializeField] public Image TileImage { get; private set; }
        [field: SerializeField] public Button TileButton { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TileText { get; private set; }
    }
}
