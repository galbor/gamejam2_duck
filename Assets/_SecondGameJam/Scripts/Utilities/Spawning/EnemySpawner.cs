using System;
using System.Collections;
using UnityEngine;
using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Events.Publishers;
using LevelConfig = _SecondGameJam.Scripts.Data.ScriptableObjects.LevelConfig;

namespace _SecondGameJam.Scripts.Utilities.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        [SerializeField] private Transform _enemyContainer;

        private void Awake()
        {
         if (Instance == null)
         {
             Instance = this;
         }
         else
         {
             Destroy(gameObject);
         }   
        }

        private void Start()
        {
            // Assumption - First child is the enemy container.
            _enemyContainer = transform.GetChild(0);
        }

        /** Starts a level. Clearing of enemies should be done manually. */
        public IEnumerator StartLevel(LevelConfig level)
        {
            var wavesConfig = level.WavesConfig;
            var waves = wavesConfig.Waves;
            int waveCount = waves.Count;
            SpawnManager.Instance.SetWaves(wavesConfig);
            yield return StartCoroutine(SpawnLevel(waveCount));
        }

        public void ClearEnemies()
        {
            for (int i = _enemyContainer.childCount - 1; i >= 0; i--)
            {
                var child = _enemyContainer.GetChild(i);
                var childObj = child.gameObject;
                Destroy(childObj);
            }
        }

        private IEnumerator SpawnLevel(int waveCount)
        {
            for (int i = 0; i < waveCount; i++)
            {
                yield return StartCoroutine(SpawnManager.Instance.StartNextWave());
            }
            yield return new WaitUntil(() => SpawnManager.Instance.HasLevelFinished);
        }
    }
}
