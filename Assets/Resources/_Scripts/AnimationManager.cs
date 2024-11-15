using DG.Tweening;
using UnityEngine;

namespace LearnProject
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiParent;
        [Header("Scene transition")]
        [SerializeField] private RectTransform _sceneTransitionPrefab;
        private RectTransform _sceneTransform;
        [SerializeField] private bool _isLessonScene = true;
        [SerializeField] private float _appearDuration = 0.5f;
        [SerializeField] private float _transitionImageSize = 6;


        private void Awake()
        {
            _sceneTransform = Instantiate(_sceneTransitionPrefab, _uiParent);

            if (_isLessonScene)
            {
                _sceneTransform.localScale = Vector3.one * _transitionImageSize;
                OnSceneEnter();
            }
            else
            {
                _sceneTransform.localScale = Vector3.zero;
            }


            LessonsTileManager.OnSceneChangeWithDelay += OnSceneChange;
        }


        private void OnDestroy()
        {
            LessonsTileManager.OnSceneChangeWithDelay -= OnSceneChange;
        }


        private void OnSceneChange(float duration)
        {
            _sceneTransform.DOScale(_transitionImageSize, duration);
        }


        private void OnSceneEnter()
        {
            _sceneTransform.DOScale(0, _appearDuration);
        }
    }
}
