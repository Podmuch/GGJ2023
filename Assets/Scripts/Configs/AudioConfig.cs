using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public sealed class AudioConfig : ScriptableObject
    {
        public const string ResourcePath = "Configs/AudioConfig";
        
        [SerializeField]
        private List<AudioData> sounds;

        public AudioClip GetSound(string key)
        {
            var sound = sounds.Find(x => x.key.Equals(key));

            if (sound != null) return sound.audioClip;
            
            Debug.LogError($"There's now sound with {key} key ");
            return null;
        }
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] 
        public string key;
        [SerializeField] 
        public AudioClip audioClip;
    }
}