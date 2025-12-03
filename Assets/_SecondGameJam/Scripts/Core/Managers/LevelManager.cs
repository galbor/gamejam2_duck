using System.Collections;
using System.Collections.Generic;
using _SecondGameJam.Scripts.Core.GameStates.Concrete;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /** LevelManager is a Singleton that manages the loading of levels in the game. */
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public int CurrentLevelIndex { get; private set; }

        // TODO: Set levels in the inspector.
        [SerializeField] private List<LevelConfig> _levelConfigs;
        public int LevelCount => _levelConfigs?.Count ?? 0;

        /** Creates a singleton of the LevelManager. */
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

        /** Loads a level if index is valid, else returns null. */
        public LevelConfig LoadLevel(int levelIndex)
        {
            levelIndex = levelIndex % _levelConfigs.Count;
            if (0 <= levelIndex && levelIndex < _levelConfigs.Count)
            {
                CurrentLevelIndex = levelIndex;
                var loadedLevel = _levelConfigs[levelIndex];
                return loadedLevel;
            }

            Debug.LogError($"Level index {levelIndex} is out of range.");
            return null;
        }

        /** Loads next level if exists, else change GameState to win. */
        public LevelConfig LoadNextLevel()
        {
            CurrentLevelIndex++;
            if (CurrentLevelIndex < _levelConfigs.Count)
            {
                var nextLevel = _levelConfigs[CurrentLevelIndex];
                return nextLevel;
            }

            GameManager.Instance.ChangeState(new WinGameState(GameManager.Instance));
            return null;
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private static IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            // Loading screen display placeholder.
            while (!asyncLoad.isDone)
            {
                // Loading screen update progress placeholder.
                yield return null;
            }
            // Post-load operations placeholder.
        }

        public void ReloadScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            LoadScene(currentSceneName);
        }
    }
}