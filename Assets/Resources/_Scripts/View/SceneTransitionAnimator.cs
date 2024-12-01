using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    [RequireComponent(typeof(RectTransform))]
    public class SceneTransitionAnimator : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.35f;
        private RectTransform _rt;
        private static SceneTransitionAnimator _instance;

        private void Awake()
        {
            _instance = this;
            _rt = GetComponent<RectTransform>();
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        public static Task PlayTransitionAsync(float startScale, float finalScale)
        {
            if (_instance != null)
            {
                return AnimationManager.PlayScaleTransition(_instance._rt, startScale, finalScale, _instance._duration);
            }
            Debug.LogWarning($"Instans of {typeof(SceneTransitionAnimator)} is null");
            return Task.CompletedTask;
        }
    }
}
