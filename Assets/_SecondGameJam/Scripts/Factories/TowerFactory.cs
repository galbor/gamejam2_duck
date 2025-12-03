using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _SecondGameJam.Scripts.Factories
{
    /** Used to create towers.
     * Usage: towerFactory.CreateTower(TowerFactory.TowerType.*);
     */
    public class TowerFactory
    {
        public enum TowerType
        {
            Regular,
            Kamikaze,
            Anchor
            // Other tower types placeholder.
        }

        /** Loads a tower prefab of the format: {type}TowerPrefab
         * Note: The prefab must be in the Resources folder.
         */
        public static GameObject CreateTower(TowerType type)
        {
            // TODO: Change to Addressables.
            var prefab = Resources.Load<GameObject>($"{type}TowerPrefab");
            if (prefab == null)
            {
                throw new ArgumentException($"TowerFactory.CreateTower() could not find a prefab for {type}.");
            }

            return Object.Instantiate(prefab);
        }
    }
}