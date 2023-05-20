using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public static AudioManager instance;

    [SerializeField]
    private Slider masterVolumeSlider;

    [SerializeField]
    private Slider musicVolumeSlider;

    [SerializeField]
    private Slider sfxVolumeSlider;

    private void Awake()
    {
        // Load the volume settings from player prefs
        if (PlayerPrefs.HasKey("Master"))
        {
            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.value = PlayerPrefs.GetFloat("Master", 1f);
            }

            SetMasterVolume(PlayerPrefs.GetFloat("Master", 1f));
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = PlayerPrefs.GetFloat("Music", 1f);
            }

            SetMusicVolume(PlayerPrefs.GetFloat("Music", 1f));
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
            }

            SetSFXVolume(PlayerPrefs.GetFloat("SFX", 1f));
        }
    }

    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);

        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);

        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);

        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
