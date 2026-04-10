using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ScaleOnStart : MonoBehaviour
{
    public float from, to, duration;
    public Ease ease;

    public UnityEvent onComplete;

    void Start()
    {
        transform.DOScale(to, duration).From(from).SetEase(ease).OnComplete(() => onComplete?.Invoke());
    }
}
