using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Initialize audio mixers for sound and sound
    [SerializeField] private AudioMixerGroup soundMixerGroup, musicMixerGroup;

    // Default values for music and sound levels
    public float musicLevel { get; set; } = -50;
    public float soundLevel { get; set; } = -50;

    // Array of sounds to be played by this object
    public Sound[] sounds;

    void Awake()
    {
        instance = this;

        // Setup each sound/musuiuc source
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

    private void Start()
    {
        if (SettingsScript.instance != null)
        {
            SettingsScript.instance.SetSoundLevel(soundLevel);
            SettingsScript.instance.SetMusicLevel(musicLevel);
        }
        else
        {
            UpdateMixer();
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
        soundMixerGroup.audioMixer.SetFloat("SoundVol", soundLevel);
        musicMixerGroup.audioMixer.SetFloat("MusicVol", musicLevel);
    }
}
