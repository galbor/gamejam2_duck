using System.Collections.Generic;
using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** A collection of waves for a single level. */
    [System.Serializable]
    public class WavesConfig
    {
        public List<WaveConfig> Waves = new();

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
    }
}