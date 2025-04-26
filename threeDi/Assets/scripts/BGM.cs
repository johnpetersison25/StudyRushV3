using UnityEngine;
using UnityEngine.Audio; // Add this!

public class BGM : MonoBehaviour
{
    public static BGM Instance;

    private AudioSource bgmAudio;
    public AudioClip dangerMusic;
    public AudioMixer audioMixer; // 👈 Assign this in Inspector
    public string exposedVolumeParam = "MasterVolume"; // 👈 Match this to your exposed parameter name

    private bool isPlaying = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmAudio = gameObject.AddComponent<AudioSource>();
            bgmAudio.loop = true;
            bgmAudio.clip = dangerMusic;

            // 🔊 Connect the mixer to the audio source
            if (audioMixer != null)
                bgmAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0]; // 👈 Group name must match!

        }
        else
        {
            Destroy(gameObject);
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
