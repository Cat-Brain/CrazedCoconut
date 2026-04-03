using UnityEngine;

public class SoundEffectOnTick : TickComponent
{
    public AudioClip clip;
    public float pitchVariance = 0, basePitch = 1;

    public override void OnTick()
    {
        SoundEffectManager.PlaySound(clip, pitchVariance, basePitch);
    }
}
