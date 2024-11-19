using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace LearnProject
{
    public class AnimationManager
    {
        private static float _halfTransitionDuration = 1f;


        public static Task PlayTransitionAsync(RectTransform rectTransform, float startScale, float finalScale)
        {
            rectTransform.transform.localScale = Vector3.one * startScale;
            return rectTransform.DOScale(finalScale, _halfTransitionDuration).AsyncWaitForCompletion();
        }
    }
}
