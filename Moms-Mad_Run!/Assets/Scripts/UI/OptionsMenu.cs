using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    //public TMP_Dropdown dropdownGraphics; //Graphics Dropdown

    //Volume Sliders
    public Slider masterVol, musicVol, soundVol;
    public float masterVolTemp, musicVolTemp, soundVolTemp;
    public AudioMixer mainAudioMixer;

    /*
    //Graphics Quality

    public void ChangeGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(dropdownGraphics.value);
        Debug.Log("Graphics Change");
    }
    */

    //Audio Sliders
    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterParam", masterVol.value);
        masterVolTemp = masterVol.value;
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicParam", musicVol.value);
        musicVolTemp = musicVol.value;
    }

    public void ChangeSoundVolume()
    {
        mainAudioMixer.SetFloat("SoundParam", soundVol.value);
        soundVolTemp = soundVol.value;
    }

    void Start()
    {
        //Match Audio Sliders to Audio Mixer
        mainAudioMixer.GetFloat("MasterParam", out float master);
        masterVol.value = master;
        mainAudioMixer.GetFloat("MusicParam", out float music);
        musicVol.value = music;
        mainAudioMixer.GetFloat("SoundParam", out float sound);
        soundVol.value = sound;
    }
}
