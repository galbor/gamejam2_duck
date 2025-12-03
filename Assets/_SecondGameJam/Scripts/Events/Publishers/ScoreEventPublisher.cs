namespace _SecondGameJam.Scripts.Events.Publishers
{
    /** An event for updating the score UI. */
    public static class ScoreEventPublisher
    {
        public delegate void ScoreAction(int score);

        public static event ScoreAction OnScoreChanged;

        public static void ChangeScore(int score)
        {
            OnScoreChanged?.Invoke(score);
        }
    }
}