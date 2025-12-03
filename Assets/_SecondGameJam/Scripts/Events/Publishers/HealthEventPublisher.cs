namespace _SecondGameJam.Scripts.Events.Publishers
{
    /** An event for changing health HUD. */
    public static class HealthEventPublisher
    {
        public delegate void HealthAction(float healthPercent);

        public static event HealthAction OnPlayerTakeDamage;

        public static void TakeDamage(float healthPercent)
        {
            OnPlayerTakeDamage?.Invoke(healthPercent);
        }
    }
}