using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _SecondGameJam.Scripts.Core.Managers
{
    public enum SoundType
    {
        Sound,
        Music
    }
    
    /**
     * AudioManager is a Singleton that manages the audio in the game.
     * It has a list of sounds that can be played and a list of music that can be played.
     * It also has a method to lower the volume of the music.
     */
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        [SerializeField] private float _musicVolume = 0.5f;
        [SerializeField] private float _soundVolume = 0.5f;
        private Dictionary<string, AudioSource> _sounds = new();
        private Dictionary<string, AudioSource> _musicTracks = new();
        private Dictionary<string, AudioSource> GetAudioDict(SoundType soundType) =>
            soundType == SoundType.Sound ? _sounds : _musicTracks;
        private float GetVolume(SoundType soundType) => soundType == SoundType.Sound ? _soundVolume : _musicVolume;
        
        /** Create Singleton. */
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Debug.LogWarning("Destroying duplicate AudioManager");
                Destroy(gameObject);
            }
        }
        
      
        /** Play audio only if audio isn't playing. */
        public void PlayAudio(string audioName, SoundType soundType=SoundType.Sound, float volume=-1.0f,
                              float pitch=1.0f, bool loop=false)
        {
            if (volume < 0)
            {
                volume = GetVolume(soundType);
            }
            Dictionary<string, AudioSource> audioDict = GetAudioDict(soundType);
            if (!audioDict.TryGetValue(audioName, out var source))
            {
                LoadAndPlayAudio(audioName, soundType, volume, pitch, loop);
                return;
            }
            
            if (!source.isPlaying)
            {
                source.volume = volume;
                source.pitch = pitch;
                source.loop = loop;
                source.Play();
            }
        }
        
        /** Loads Audio async, then plays it.  */
        private void LoadAndPlayAudio(string audioName, SoundType soundType,
                                      float volume = -1.0f, float pitch = 1.0f, bool loop = false)
        {
            if (volume < 0)
            {
                volume = GetVolume(soundType);
            }
            var audioDict = GetAudioDict(soundType);
            AudioSource source = null;
            Addressables.LoadAssetAsync<AudioClip>(audioName).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    source = Instantiate(new GameObject(), transform).AddComponent<AudioSource>();
                    source.clip = handle.Result;
                    source.volume = volume;
                    source.pitch = pitch;
                    source.loop = loop;
                    audioDict[audioName] = source;
                    source.Play();
                }
                else
                {
                    Debug.LogError($"Failed to load {soundType}: {audioName}");
                }
            };
        }
        

        /** Stop audio only if audio is playing. */
        public void StopAudio(string audioName, SoundType soundType)
        {
            Dictionary<string, AudioSource> audioDict = GetAudioDict(soundType);
            if (!audioDict.TryGetValue(audioName, out var source))
            {
                Debug.LogError($"Failed to stop {soundType}: {audioName} not found!" +
                               $" It might be still loading.");
                return;
            }
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        /** Change volume by soundType {soundType}. */
        public void ChangeVolume(SoundType soundType, float newVolume, float duration)
        {
            StartCoroutine(AdjustVolume(soundType, newVolume, duration));
        }

        /** Change volume of a specific audio. */
        public void ChangeVolume(string audioName, SoundType soundType, float newVolume, float duration)
        {
            StartCoroutine(AdjustVolume(audioName, soundType, newVolume, duration));
        }

        /** Lowers volume for audio {audioName}.  */
        public IEnumerator AdjustVolume(string audioName, SoundType soundType, float newVolume,
            float duration = 1.0f)
        {
            var audioDict = GetAudioDict(soundType);
            if (!audioDict.TryGetValue(audioName, out var source))
            {
                Debug.LogError($"Failed to change volume of {soundType}: {audioName} not found!");
                yield break;
            }
            float startVolume = source.volume;
            for (float currentTime = 0; currentTime < duration; )
            {
                currentTime += Time.deltaTime;
                float newVolumeValue = Mathf.Lerp(startVolume, newVolume, currentTime / duration);
                source.volume = newVolumeValue;
                yield return null;
            }
        }

        /** Lowers volume for all audio of type {soundType}.  */
        private IEnumerator AdjustVolume(SoundType soundType, float targetVolume, float duration)
        {
            var audioDict = GetAudioDict(soundType);
            float startVolume =  GetVolume(soundType);
            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
                foreach (var audioData in audioDict)
                {
                    AudioSource source = audioData.Value;
                    source.volume = newVolume;
                }
                yield return null;
            }
        }

        /** Stop all audio of type {soundType}. */
        public void StopAllAudio(SoundType soundType)
        {
            var audioDict = GetAudioDict(soundType);
            foreach (var audioData in audioDict)
            {
                AudioSource source = audioData.Value;
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }
        }

        /** Unload audio from dictionary. */
        public void UnloadAudio(string audioName, SoundType soundType)
        {
            var audioDict = GetAudioDict(soundType);
            if (!audioDict.TryGetValue(audioName, out var source))
            {
                string soundTypeString = soundType.ToString();
                Debug.LogError($"Failed to unload {soundTypeString}: {audioName} not found!");
                return;
            }
            Destroy(source);
            Addressables.Release(audioName);
            audioDict.Remove(audioName);
        }
    }
}