using System.Collections.Generic;
using _SecondGameJam.Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace _SecondGameJam.Scripts.Utilities.Spawning.WaveConfigurations
{
    /** Used to store a sequence of Waves. */
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Levels/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        public string Name = "";
        public List<WaveConfig> Waves = new();
        public TowerInventory TowerInventory;
        // TODO: Remove list of TowerSpawnConfig and replace with tower inventory.
        public List<TowerSpawnConfig> Towers = new();

        /** A single Wave's data */
        [System.Serializable]
        public class WaveConfig
        {
            public float WaveDelayInSeconds = 5.0f;
            public List<EnemySpawnConfig> EnemySpawns = new();

            [System.Serializable]
            public class EnemySpawnConfig
            {
                public GameObject Prefab;
                public Vector3 Location;
            }
        }

        // TODO: Remove this class and replace with tower inventory.
        [System.Serializable]
        public class TowerSpawnConfig
        {
            public TowerConfig Tower;
            public int Amount;
        }
    }
}