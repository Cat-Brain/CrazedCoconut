using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource source;
    private bool started;

    public void Init(AudioClip clip, float pitch = 1)
    {
        source.clip = clip;
        source.pitch = pitch;

        started = true;
        source.Play();
    }

    void Update()
    {
        if (!started || source.isPlaying && enabled)
            return;

        Destroy(gameObject);
        enabled = false;
    }
}
