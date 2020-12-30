using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;
    [SerializeField] private AudioMixerGroup soundMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    
    public Sound[] sounds;

    void Awake()
    {
        instance = this;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            switch (s.audioType)
            {
                case Sound.AudioType.soundEffect:
                    s.source.outputAudioMixerGroup = soundMixerGroup;
                    break;

                case Sound.AudioType.musicEffect:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);
        if (newSound == null) 
        {
            Debug.LogWarning(newSound + " sound/music was not found!");
            return;
        }
        newSound.source.Play();
    }

    public void StopSound(string name)
    {
        Sound newSound = Array.Find(sounds, sound => sound.name == name);
        if (newSound == null)
        {
            Debug.LogWarning(newSound + " sound/music was not found!");
            return;
        }
        newSound.source.Stop();
    }

    public void UpdateMixer()
    {
        soundMixerGroup.audioMixer.SetFloat("SoundVol", Mathf.Log10(AudioMixerManager.soundVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVol", Mathf.Log10(AudioMixerManager.musicVolume) * 20);
    }
}
