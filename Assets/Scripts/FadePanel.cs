using UnityEngine;
using DG.Tweening;

public class FadePanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    private Tween fadeTween;
    public void FadeIn(float duraction)
    {
        Fade(1f, duraction, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }
    public void FadeOut(float duraction)
    {
        Fade(0f, duraction, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
            fadeTween.Kill(false);

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
