using UnityEngine;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    /** The stats of a tower. */
    [CreateAssetMenu(fileName = "NewTowerData", menuName = "Towers/Tower Data")]
    [System.Serializable]
    public class TowerConfig : ScriptableObject
    {
        public string Name;
        public string PrefabAddress;
        public string Description;
        public Sprite Icon;
    }
}