using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;


    const string MasterVolume = "Master";
    const string SFXVolume = "SFX";
    const string MusicVolume = "Music";


    [EasyButtons.Button]
    public void OnMasterVolumeChange(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        Debug.Log("MasterVolume now: " + dB);
        if (audioMixer != null)
        {
            bool result = audioMixer.SetFloat(MasterVolume, dB);
            if (!result)
            {
                Debug.LogWarning($"Parameter '{MasterVolume}' not found in AudioMixer. Please check the exposed parameter name.");
            }
        }
        if (audioMixer != null)
        {
            bool result = audioMixer.SetFloat(SFXVolume, dB);
            if (!result)
            {
                Debug.LogWarning($"Parameter '{SFXVolume}' not found in AudioMixer. Please check the exposed parameter name.");
            }
        }
        if (audioMixer != null)
        {
            bool result = audioMixer.SetFloat(MusicVolume, dB);
            if (!result)
            {
                Debug.LogWarning($"Parameter '{MusicVolume}' not found in AudioMixer. Please check the exposed parameter name.");
            }
        }
        else
        {
            Debug.LogWarning("AudioMixer reference is not set in the inspector.");
        }
    }

    [EasyButtons.Button]
    public void OnSFXVolumeChange(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        Debug.Log("SFXVolume now: " + dB);
        audioMixer?.SetFloat(SFXVolume, dB);
    }

    [EasyButtons.Button]
    public void OnMusicVolumeChange(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        Debug.Log("MusicVolume now: " + dB);
        audioMixer?.SetFloat(MusicVolume, dB);
    }


}
