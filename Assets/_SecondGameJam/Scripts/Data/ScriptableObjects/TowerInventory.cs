using System.Collections.Generic;

namespace _SecondGameJam.Scripts.Data.ScriptableObjects
{
    [System.Serializable]
    public class TowerInventory
    {
        public List<TowerItem> AvailableTowers;

        [System.Serializable]
        public class TowerItem
        {
            public TowerConfig Config;
            public int Count;
        }

        public int GetTowerCount(TowerConfig config)
        {
            var tower = AvailableTowers.Find(item => item.Config == config);
            int count = tower?.Count ?? 0;
            return count;
        }
    }
}