using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
