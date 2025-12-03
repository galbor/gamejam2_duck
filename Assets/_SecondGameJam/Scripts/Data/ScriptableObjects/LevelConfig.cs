using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** Used to store a sequence of Waves. */
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Levels/Level Config V2.0")]
    public class LevelConfig : ScriptableObject
    {
        public string Name;
        public TowerInventory TowerInventory;
        public WavesConfig WavesConfig;
    }
}