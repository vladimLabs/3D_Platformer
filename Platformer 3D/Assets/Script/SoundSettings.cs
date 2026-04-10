using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider bgVolumeSlider;
    [SerializeField] private AudioMixer mixer;

    private const string master_Volume = "MasterVolume";
    private const string sfx_Volume = "SFXVolume";
    private const string bgr_Volume = "BgVolume";

    private void Awake()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        bgVolumeSlider.onValueChanged.AddListener(SetBgrVolume);
    }

    private void Start()
    {
        TryLoadVolume(master_Volume, masterVolumeSlider, SetMasterVolume);
        TryLoadVolume(sfx_Volume, sfxVolumeSlider, SetSFXVolume);
        TryLoadVolume(bgr_Volume, bgVolumeSlider, SetBgrVolume);
    }

    private void SetMasterVolume(float value)
    {
        mixer.SetFloat(master_Volume, GetMixerVolume(value));
        SaveVolume(master_Volume, value);
    }

    private void SetSFXVolume(float value)
    {
        mixer.SetFloat(sfx_Volume, GetMixerVolume(value));
        SaveVolume(sfx_Volume, value);
    }

    private void SetBgrVolume(float value)
    {
        mixer.SetFloat(bgr_Volume, GetMixerVolume(value));
        SaveVolume(bgr_Volume, value);
    }

    private float GetMixerVolume(float value) =>
        Mathf.Log10(value) * 20;

    private void SaveVolume(string channel, float value)
    {
        PlayerPrefs.SetFloat(channel, value);
    }

    private void TryLoadVolume(string channel, Slider slider,Action<float> loadCallback)
    {
        if (PlayerPrefs.HasKey(channel))
        {
            float value = PlayerPrefs.GetFloat(channel);
            loadCallback.Invoke(value);
            slider.value = value;
        }
    }
}
