using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer; // Assign in Inspector
    public Slider volumeSlider;   // Assign your UI slider

    void Start()
    {
        // Optional: load saved volume from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float sliderValue)
    {
        // Convert linear slider (0.0001 to 1) to decibel scale
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);

        // Save volume preference
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
