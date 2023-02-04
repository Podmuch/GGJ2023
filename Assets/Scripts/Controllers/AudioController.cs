using System.Collections.Generic;
using BoxColliders.Configs;
using BoxColliders.Project;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController Instance { get; set; }
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> busyAudioSources = new List<AudioSource>();

    private AudioConfig audioConfig;
    
    #region Monobehaviour;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void LateUpdate()
    {
        for (var index = 0; index < busyAudioSources.Count; index++)
        {
            var busyAudioSource = busyAudioSources[index];
            if (busyAudioSource.isPlaying) continue;
            
            audioSources.Add(busyAudioSource);
            busyAudioSources.Remove(busyAudioSource);
        }
    }
    
    #endregion

    public AudioSource PlayAudio(string key, float delay = 0f, bool looped = false, float loopTime = 5)
    {
        if(audioConfig == null) audioConfig = ProjectDIContainer.Instance.GetReference<AudioConfig>(null);
        
        if (audioSources.Count <= 0)
        {
            var destAudioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(destAudioSource);
            destAudioSource.playOnAwake = false;

        }
        
        var audioClip = audioConfig.GetSound(key);
        var audioSource = audioSources[0];
        audioSources.RemoveAt(0);
        busyAudioSources.Add(audioSource);
        
        audioSource.clip = audioClip;
        audioSource.loop = looped;
        
        audioSource.PlayDelayed(delay);

        return audioSource;
    }
}
