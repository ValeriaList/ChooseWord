using UnityEngine;
using DG.Tweening;

public class BounceEffect : MonoBehaviour
{
    public void Bounce(GameObject[] icons)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.DORewind();
            icons[i].transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, vibrato: 8, 3);
        }
    }
    public void ImageBounce(GameObject icon, Vector3 pos)
    {
        var buttonTransform = icon.transform;
        var image = buttonTransform.GetChild(0);
        image.transform.DOShakePosition(2.0f, strength: pos, vibrato: 5, randomness: 50, snapping: false, fadeOut: true);
    }
}