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
        Debug.Log("MasterVolume now: " + volume); 
        audioMixer?.SetFloat(MasterVolume, volume); 
    }

    [EasyButtons.Button]
    public void OnSFXVolumeChange(float volume)
    {
        Debug.Log("SFXVolume now: " + volume); 
        audioMixer?.SetFloat(SFXVolume, volume); 
        
    }

    [EasyButtons.Button]
    public void OnMusicVolumeChange(float volume)
    {
        Debug.Log("MusicVolume now: " + volume); 
        audioMixer?.SetFloat(MusicVolume, volume); 
    }


}
