using ClownLib;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Gate : MonoBehaviour
{
    public GameObject postPrefab;

    public int postCount;
    public Vector3 postPosVariance;
    public Vector3 postRotationVariance;

    public float lowHeight, highHeight;
    public float moveTime;
    public Ease moveEase;

    private bool active;

    void Start()
    {
        for (int i = 0; i < postCount; i++)
            Instantiate(postPrefab, transform.TransformPoint(new Vector3(
                Random.Range(-postPosVariance.x, postPosVariance.x),
                Random.Range(-postPosVariance.y, postPosVariance.y),
                Random.Range(-postPosVariance.z, postPosVariance.z))),
                Quaternion.Euler(transform.TransformDirection(
                Random.Range(-postRotationVariance.x, postRotationVariance.x),
                Random.Range(-postRotationVariance.y, postRotationVariance.y),
                Random.Range(-postRotationVariance.z, postRotationVariance.z))), transform);

        active = false;
        transform.position = transform.position.SetY(lowHeight);
    }

    [ProPlayButton]
    public void SetActive(bool active)
    {
        if (this.active == active)
            return;

        this.active = active;
        transform.DOMoveY(active ? highHeight : lowHeight, moveTime).SetEase(moveEase);
    }
}
