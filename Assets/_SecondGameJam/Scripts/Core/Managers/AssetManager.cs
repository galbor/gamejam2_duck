using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace _SecondGameJam.Scripts.Core.Managers
{
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager Instance { get; private set; }
        private static readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();

        /**
         * Create the Singleton instance of the AssetManager.
         */
        private void Awake()
        { 
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void LoadAssetAsync<T>(string address, Action<T> onLoaded) where T : Object
        {
            if (_loadedAssets.TryGetValue(address, out var handle))
            {
                onLoaded?.Invoke((T) handle.Result);
                return;
            }
            
            Addressables.LoadAssetAsync<T>(address).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError($"Failed to load asset of type {typeof(T).Name} at address {address}");
                    return;
                }
                _loadedAssets[address] = handle;
                onLoaded?.Invoke(handle.Result);
            }; 
        }

        public void UnloadAsset<T>(string address) where T : Object
        {
            if (_loadedAssets.TryGetValue(address, out var handle))
            {
                Addressables.Release(handle);
                _loadedAssets.Remove(address);
            }

        }
    }
}