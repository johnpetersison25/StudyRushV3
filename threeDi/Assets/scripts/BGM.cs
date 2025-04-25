using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance;

    private AudioSource bgmAudio;
    public AudioClip dangerMusic; // Assign your music clip in the inspector
    private bool isPlaying = false;

    void Awake()
    {
        // Make sure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            bgmAudio = gameObject.AddComponent<AudioSource>();
            bgmAudio.loop = true;
            bgmAudio.clip = dangerMusic;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void PlayDangerMusic()
    {
        if (!isPlaying)
        {
            bgmAudio.Play();
            isPlaying = true;
        }
    }

    public void StopMusic()
    {
        if (isPlaying)
        {
            bgmAudio.Stop();
            isPlaying = false;
        }
    }
}
