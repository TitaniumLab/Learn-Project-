using TMPro;
using UnityEngine;

namespace LearnProject
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ShowAddedScoreData : MonoBehaviour
    {
        public CanvasGroup ScoreCanvasGroup { get { return GetComponent<CanvasGroup>(); } }
        [field: SerializeField] public TextMeshProUGUI AddedScoreText { get; private set; }
    }
}
