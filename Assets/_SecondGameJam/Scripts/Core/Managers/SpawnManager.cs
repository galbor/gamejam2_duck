using System.Collections;
using System.Collections.Generic;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /**
     * Usage:
     * 1)Attach to an empty GameObject in the scene.
     * 2)Use SetWaves() to set the level to spawn, and StartNextWave() to spawn each wave.
     */
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        [SerializeField] private Transform _enemyContainer;

        // TODO: Set to the first level's wavesConfig?
        [SerializeField] private List<WavesConfig.WaveConfig> _waves = new();
        private int _currentWaveIndex = -1;
        private int _activeEnemies;
        public bool HasLevelFinished => _activeEnemies <= 0;

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
            _enemyContainer = transform.GetChild(0);
        }

        private void OnEnable()
        {
            DeathEventPublisher.OnDeath += OnEnemyDefeated;
        }

        private void OnDisable()
        {
            DeathEventPublisher.OnDeath -= OnEnemyDefeated;
        }

        private void OnEnemyDefeated()
        {
            _activeEnemies--;
        }

        public void SetWaves(WavesConfig wavesConfig)
        {
            _waves.Clear();
            _currentWaveIndex = -1;
            _activeEnemies = 0;
            var waves = wavesConfig.Waves;
            foreach (var wave in waves)
            {
                _waves.Add(wave);
                int enemyCount = wave.EnemySpawns.Count;
                _activeEnemies += enemyCount;
            }
        }

        /** Spawns a wave, then returns after all enemies were spawned. */
        public IEnumerator StartNextWave()
        {
            _currentWaveIndex++;
            if (_currentWaveIndex < _waves.Count)
            {
                var wave = _waves[_currentWaveIndex];
                yield return StartCoroutine(SpawnWave(wave));
            }
            else
            {
                Debug.Log("Finished all waves!");
            }
        }

        private IEnumerator SpawnWave(WavesConfig.WaveConfig wave)
        {
            yield return new WaitForSeconds(wave.WaveDelayInSeconds);
            var enemies = wave.EnemySpawns;
            foreach (var enemy in enemies)
            {
                SpawnEnemy(enemy);
            }
        }

        private void SpawnEnemy(WavesConfig.WaveConfig.EnemySpawnConfig enemySpawnConfig)
        {
            GameObject prefab = enemySpawnConfig.Prefab;
            Vector3 location = enemySpawnConfig.Location;
            // Rotate enemy clockwise by 90 degrees:
            Quaternion rotation = Quaternion.Euler(0, 0, -90.0f);
            GameObject enemy = Instantiate(prefab, location, rotation, _enemyContainer);
            // TODO: Set enemy's properties here:
        }
    }
}