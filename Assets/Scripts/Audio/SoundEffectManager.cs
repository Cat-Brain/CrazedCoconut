using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    public GameObject soundEffectPrefab;

    public static SoundEffectPlayer PlaySound(
        AudioClip clip, float pitchVariance = 0, float basePitch = 1)
    {
        if (instance == null)
            instance = FindAnyObjectByType<SoundEffectManager>();

        SoundEffectPlayer newSoundEffect = Instantiate(instance.soundEffectPrefab, instance.transform)
            .GetComponent<SoundEffectPlayer>();
        newSoundEffect.Init(clip, Random.Range(basePitch - pitchVariance, basePitch + pitchVariance));
        return newSoundEffect;
    }
}
