using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

    }
    public void SetSoundEffects(bool isOn)
    {
        if(isOn)
            audioMixer.SetFloat("SFX", -80);
        else
            audioMixer.SetFloat("SFX", 0);
        
        PlayerPrefs.SetInt("SFX", isOn ? 1 : 0);

    }
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

    }
}
