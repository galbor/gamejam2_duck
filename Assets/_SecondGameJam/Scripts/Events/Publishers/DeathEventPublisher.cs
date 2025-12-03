namespace _SecondGameJam.Scripts.Events.Publishers
{
    public static class DeathEventPublisher
    {
        public delegate void DeathAction();

        public static event DeathAction OnDeath;

        public static void EnemyDeath()
        {
            OnDeath?.Invoke();
        }
    }
}