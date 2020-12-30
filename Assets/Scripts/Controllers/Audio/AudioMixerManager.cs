using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    public float defaultValue = 0.01f;
    public static float musicVolume { get; private set; }
    public static float soundVolume { get; private set; }

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private Text musicTextValue;
    [SerializeField] private Text soundTextValue;

    void Start()
    {
        musicSlider.value = defaultValue;
        soundSlider.value = defaultValue;
        SetMusicLevel(defaultValue);
        SetSoundLevel(defaultValue);
    }

    public void SetMusicLevel (float sliderValue)
    {
        musicVolume = sliderValue;
        musicTextValue.text = (sliderValue * 100).ToString();
        AudioManager.instance.UpdateMixer();
    }

    public void SetSoundLevel(float sliderValue)
    {
        soundVolume = sliderValue;
        soundTextValue.text = (sliderValue * 100).ToString();
        AudioManager.instance.UpdateMixer();
    }
}
