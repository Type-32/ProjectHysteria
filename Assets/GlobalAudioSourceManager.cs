using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hysteria.Utility;
using UnityEngine;

public class GlobalAudioSourceManager : SerializedSingleton<GlobalAudioSourceManager>
{
    [SerializeField] private int maxAudioSources = 10;
    private List<AudioSource> _audioSources = new List<AudioSource>();
    [SerializeField] private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(source);
        }
    }

    public void PlaySound(string clipName, float volume = 1f, bool loop = false)
    {
        if (!_audioClips.ContainsKey(clipName))
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found.");
            return;
        }

        AudioSource availableSource = GetAvailableAudioSource();
        if (availableSource != null)
        {
            availableSource.clip = _audioClips[clipName];
            availableSource.volume = volume;
            availableSource.loop = loop;
            availableSource.Play();
        }
        else
        {
            Debug.LogWarning("No available audio sources. Consider increasing maxAudioSources.");
        }
    }

    public void StopSound(string clipName)
    {
        foreach (AudioSource source in _audioSources)
        {
            if (source.isPlaying && source.clip.name == clipName)
            {
                source.Stop();
                return;
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (AudioSource source in _audioSources)
        {
            source.Stop();
        }
    }

    public void LoadAudioClip(string clipName, AudioClip clip)
    {
        if (!_audioClips.ContainsKey(clipName))
        {
            _audioClips.Add(clipName, clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' already exists. Overwriting.");
            _audioClips[clipName] = clip;
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in _audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    public List<string> GetAudioClipKeys()
    {
        return _audioClips.Keys.ToList();
    }

    public void SetGlobalVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
    }

    public float GetGlobalVolume()
    {
        return AudioListener.volume;
    }
}