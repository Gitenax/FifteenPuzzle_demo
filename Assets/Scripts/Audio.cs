using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : Singleton<Audio>
{
    private readonly float MinVolume = -80;
    private readonly float MaxVolume = 0;
    
    
    [Header("Музыка")]
    [SerializeField] private AudioSource _backgroundMusic;
    
    [Header("Микшер"), Space]
    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _UiGroup;
    
    [Header("Громкость"), Space]
    private bool _musicVolumeEnabled = true;
    private bool _UiVolumeEnabled = true;

    
    public void ToggleMusic()
    {
        if (_musicVolumeEnabled)
        {
            _musicGroup.audioMixer.SetFloat("MusicVolume", -20f);
            _backgroundMusic.Stop();
        }
        else
        {
            _musicGroup.audioMixer.SetFloat("MusicVolume", MinVolume);
            _backgroundMusic.Play();
        }
        
        PlayerPrefs.SetInt("MusicVolumeEnabled", _musicVolumeEnabled ? 1 : 0);
    }

    public void ToggleUI()
    {
        if(_UiVolumeEnabled)
            _UiGroup.audioMixer.SetFloat("UiVolume", MaxVolume);
        else
            _UiGroup.audioMixer.SetFloat("UiVolume", MinVolume);
        
        PlayerPrefs.SetInt("UiVolumeEnabled", _UiVolumeEnabled ? 1 : 0);
    }

    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
