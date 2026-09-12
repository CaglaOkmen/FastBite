using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;

    public AudioClip[] soundClips;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(soundClips[0]);
    }

    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(soundClips[1]);
    }

    public void PlayDropSound()
    {
        audioSource.PlayOneShot(soundClips[2]);
    }

    public void PlayNewItemSound()
    {
        audioSource.PlayOneShot(soundClips[3]);
    }

    public void PlayWrongSound()
    {
        audioSource.PlayOneShot(soundClips[4]);
    }
}
