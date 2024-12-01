using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    public static class AnimationManager
    {
        public static Task PlayScaleTransition(RectTransform rectTransform, float startScale, float endSclae, float duration)
        {

            rectTransform.localScale = Vector3.one * startScale;
            var tween = rectTransform.DOScale(endSclae, duration);
            return tween.AsyncWaitForCompletion();
        }
    }
}
