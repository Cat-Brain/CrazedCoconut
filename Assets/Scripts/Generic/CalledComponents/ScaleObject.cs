using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ScaleObject : MonoBehaviour
{
    public float from, to, duration;
    public Ease ease;

    public UnityEvent onComplete;

    public void Activate()
    {
        transform.DOScale(to, duration).From(from).SetEase(ease).OnComplete(() => onComplete?.Invoke());
    }
}
