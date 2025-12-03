using System.Collections;
using _SecondGameJam.Scripts.Core.GameStates.Concrete;
using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using _SecondGameJam.Scripts.Utilities.Spawning;
using UnityEngine;

namespace _SecondGameJam.Scripts.Utilities.Levels
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private float _delayBeforeLevelSwitch = 2f;
        [SerializeField] private KeyCode _previousLevelKey = KeyCode.B;
        [SerializeField] private KeyCode _nextLevelKey = KeyCode.N;

        private void Start()
        {
            int startLevelIndex = LevelManager.Instance.CurrentLevelIndex;
            StartLevels(startLevelIndex, 0.0f);
        }
        
        private void StartLevels(int startLevelIndex, float firstLevelDelay = -1)
        {
            StopAllCoroutines();
            if (firstLevelDelay < 0)
            {
                firstLevelDelay = _delayBeforeLevelSwitch;
            }
            StartCoroutine(SwitchLevels(startLevelIndex, firstLevelDelay));
        }

        private IEnumerator SwitchLevels(int currentLevelIndex, float firstLevelDelay)
        {
            LevelConfig currentLevel = LevelManager.Instance.LoadLevel(currentLevelIndex);
            int levelCount = LevelManager.Instance.LevelCount;
            for (int i = currentLevelIndex; i < levelCount; i++)
            {
                Debug.Log($"Started level {i + 1}.");
                if (i == currentLevelIndex)
                {
                    yield return StartCoroutine(SwitchLevel(currentLevel, firstLevelDelay));
                }
                else
                {
                    yield return StartCoroutine(SwitchLevel(currentLevel, _delayBeforeLevelSwitch));
                }
                Debug.Log($"Finished level {i + 1}.");
                currentLevel = LevelManager.Instance.LoadNextLevel();
            }
        }

        private IEnumerator SwitchLevel(LevelConfig loadedLevel, float delayBeforeSwitch)
        {
            yield return new WaitForSeconds(delayBeforeSwitch);
            LevelEventPublisher.ChangeLevel(loadedLevel);
            EnemySpawner.Instance.ClearEnemies();
            if (loadedLevel)
            {
                yield return StartCoroutine(EnemySpawner.Instance.StartLevel(loadedLevel));
            }
        }
        
        /** Change levels on input: */
        private void Update()
        {
            int currentLevelIndex = LevelManager.Instance.CurrentLevelIndex;
            int levelCount = LevelManager.Instance.LevelCount;
            if (Input.GetKeyDown(_nextLevelKey))
            {
                currentLevelIndex = (currentLevelIndex + 1) % levelCount;
            }
            if (Input.GetKeyDown(_previousLevelKey))
            {
                currentLevelIndex--;
                if (currentLevelIndex < 0)
                {
                    currentLevelIndex += levelCount;
                }
            }
            if (currentLevelIndex != LevelManager.Instance.CurrentLevelIndex)
            {
                GameManager.Instance.ChangeState(new PlayState(GameManager.Instance, isRestart: true));
                StartLevels(currentLevelIndex, 0.0f);
            }
        }
    }
}