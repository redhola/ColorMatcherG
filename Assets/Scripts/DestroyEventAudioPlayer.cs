using UnityEngine;

public class DestroyEventAudioPlayer : MonoBehaviour
{
    public AudioClip destructionClip; 
    public AudioSource sharedAudioSource;

    void OnDestroy()
    {
        if (sharedAudioSource != null && destructionClip != null)
        {
            sharedAudioSource.clip = destructionClip;
            sharedAudioSource.Play();
        }
    }

    void Start()
    {
        if (sharedAudioSource == null)
        {
            sharedAudioSource = gameObject.AddComponent<AudioSource>();
            sharedAudioSource.playOnAwake = false;
        }
    }
}
