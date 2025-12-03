using _SecondGameJam.Scripts.Data.ScriptableObjects;

namespace _SecondGameJam.Scripts.Events.Publishers
{
    public static class LevelEventPublisher
    {
        public delegate void LevelChangedEvent(LevelConfig newLevel);

        public static event LevelChangedEvent OnLevelChanged;

        public static void ChangeLevel(LevelConfig newLevel)
        {
            OnLevelChanged?.Invoke(newLevel);
        }
    }
}